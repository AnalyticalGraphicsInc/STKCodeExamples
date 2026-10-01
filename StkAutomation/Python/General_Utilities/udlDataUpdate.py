import os
import sys
import json
import zipfile
import shutil
import getpass
import ssl
from requests.adapters import HTTPAdapter

# GLOBALS
VERSION = {"major": 1, "minor": 0, "patch": 0}

LIST_ENDPOINT = "https://unifieddatalibrary.com/scs/v2/list?order=asc&path=/CommunityData/AGI/&sort=id"
GET_ENDPOINT = "https://unifieddatalibrary.com/scs/download?"

# This can be any writable folder
# INSTALL_DIR = "C:\\Users\\<?>\\DataUpdate"
INSTALL_DIR = "C:\\ProgramData\\AGI\\STK_ODTK 13"

TMP_NAME = "tmp"
CLEANUP_TMP = True

FILE_MAPPINGS = {
    "EOP-All-v1.1.txt": "DynamicEarthData\\",
    "GPSAlmanac.al3": "GPSAlmanacs\\",
    "GPSData.txt": "GPSAlmanacs\\",
    "LeapSecond.dat": "DynamicEarthData\\",
    "SEMFileListing.txt": "GPSAlmanacs\\",
    "SpaceWeather-All-v1.2.txt": "DynamicEarthData\\",
    "stkAllTLE.om": "Databases\\Satellite\\",
    "stkAllTLE.sd": "Databases\\Satellite\\",
    "stkAllTLE.tce": "Databases\\Satellite\\"}


class OSContextAdapter(HTTPAdapter):
    def init_poolmanager(self, connections, maxsize, block=..., **pool_kwargs):
        # force certs to use the OS Trust Store
        pool_kwargs["ssl_context"] = ssl.create_default_context()
        return super().init_poolmanager(connections, maxsize, block, **pool_kwargs)


def create_session(username, password):
    session = requests.Session()
    session.auth = HTTPBasicAuth(username, password)
    session.mount("https://", OSContextAdapter())
    return session


def fetch_zip_list(session, list_endpoint):
    resp = session.get(list_endpoint, headers={"Accept" : "application/json"})

    resp.raise_for_status()
    data = resp.json()
    zip_files = sorted(
        [item["id"] for item in data],
        reverse=True
    )

    return zip_files


def download_zip(session, zip_name, dest_path, get_endpoint):
    url = get_endpoint + f"id=/{zip_name}"

    resp = session.get(url, headers={"Accept" : "application/octet-stream"}, stream=True)
    resp.raise_for_status()

    with open(dest_path, "wb") as f:
        for chunk in resp.iter_content(chunk_size=8192):
            if chunk:
                f.write(chunk)

    resp.close()


def extract_zip(zip_path):
    extracted_path = os.path.splitext(zip_path)[0]
    with zipfile.ZipFile(zip_path, "r") as z:
        z.extractall(extracted_path)

    # rename contents according to manifest
    manifest = read_manifest(os.path.join(extracted_path, "manifest.txt"))
    for item in manifest["entries"]:
        src_path = os.path.join(extracted_path, item["id"])
        dst_path = os.path.join(extracted_path, item["fileName"])
        shutil.move(src_path, dst_path)

    return extracted_path


def read_manifest(path):
    with open(path, encoding="utf-8") as f:
        return json.load(f)


def move_to_install(extracted_dir, install_dir, file_mappings: dict):
    # make sure the install location exists
    try:
        os.makedirs(install_dir, exist_ok=True)
    except PermissionError:
        print(f" Error: You don't have permission to write to '{install_dir}'")
        return False
    except OSError as e:
        print(f" Error: Unable to create or access '{install_dir}'. Details {e}")
        return False

    # move each file
    with os.scandir(extracted_dir) as entries:
        for entry in entries:
            if entry.is_file():
                if entry.name == "manifest.txt" or file_mappings.get(entry.name) is None:
                    continue

                source_path = entry.path
                target_dir = os.path.join(install_dir, file_mappings[entry.name])
                target_path = os.path.join(target_dir, entry.name)

                print(f" Moving '{entry.name}' to '{target_path}'")
                os.makedirs(target_dir, exist_ok=True)
                shutil.copy(source_path, target_path)

    return True


def display_header():
    print("========================================================")
    print(" UDL DATA UPDATE UTILITY")
    print(f" version {VERSION['major']}.{VERSION['minor']}.{VERSION['patch']}")
    print("--------------------------------------------------------")
    print(" This command-line utility retrieves the daily data ")
    print(" package published to the UDL SCS by AGI, which contains")
    print(" the essential subset of the standard DataUpdate needed ")
    print(" for closed-network environments.")
    print("--------------------------------------------------------")


def cleanup_temp(tmp_folder):
    if CLEANUP_TMP:
        print(os.linesep + f" Deleting temporary folder: {tmp_folder} and it's contents.")
        shutil.rmtree(tmp_folder, ignore_errors=True)


def main():
    display_header()

    # create UDL session
    username = input(" UDL Username: ").strip()
    password = getpass.getpass(" UDL Password: ")
    session = create_session(username, password)

    # find the latest file
    print(os.linesep + " Retrieving list of available .zips ...")
    zip_files = fetch_zip_list(session, LIST_ENDPOINT)
    latest_zip = sorted(zip_files)[-1]

    # download the zip to a tmp folder
    tmp_dir = os.path.join(os.getcwd(), TMP_NAME)
    os.makedirs(tmp_dir, exist_ok=True)
    zip_local_path = os.path.join(tmp_dir, os.path.basename(latest_zip))

    print(os.linesep + f" Downloading {latest_zip} to {tmp_dir} ...")
    download_zip(session, latest_zip, zip_local_path, GET_ENDPOINT)

    # extract/rename the files and validate the manifest
    print(os.linesep + " Extracting contents ...")
    extracted_dir = extract_zip(zip_local_path)

    print(os.linesep + f" Moving to {INSTALL_DIR} ...")
    valid_move = move_to_install(extracted_dir, INSTALL_DIR, FILE_MAPPINGS)

    # cleanup and close
    cleanup_temp(tmp_dir)
    if not valid_move:
        print(os.linesep + f" Failed to move extracted content into {INSTALL_DIR}")
    else:
        print(os.linesep + f" Succesully extracted and installed data in: {INSTALL_DIR}")


if __name__ == "__main__":
    try:
        import requests
        from requests.auth import HTTPBasicAuth
    except ImportError:
        print(" This tool requires the 'requests' package. Please install with:")
        print(" pip install requests")
        sys.exit(1)

    main()
