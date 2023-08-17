# STK Engine Python API Docker Image

## Purpose
This Docker image code sample demonstrates how to install the STK Engine for Windows Python API in a containerized
environment.

## Prerequisites
* Docker must be running on your system.
* Access to an Ansys Licensing Server with a valid STK license.  Edit the
[`licensing.env`](../configuration/licensing.env) file to ensure the `ANSYSLMD_LICENSE_FILE` environment variable
has your Ansys License Server information.
* You have already built the `stk-engine-baseline` image.
* Download the Python installer and place the `python-3.8.10-amd64.exe` file in the [`distributions`](./distributions) folder.

## Special Configuration
This image is built using the `pip` tool to install Python package dependencies. Please see the
[`custom-environment`](../custom-environment/README.md) Docker image code sample project and build the
`ansys/stk/stk-engine-custom-baseline:{version}-windowsservercore-ltsc2019` image, if needed, before proceeding with this image.

## Method 1 - Docker CLI

### Build the Image
If you did not build the `custom-environment` image described above, run
`docker build -t ansys/stk/stk-engine-python:{version}-windowsservercore-ltsc2019 .` on the command line in this directory after replacing `{version}` with the version number. i.e `12.7.1`

If you did build the `custom-environment` image described above, run
`docker build -t ansys/stk/stk-engine-python:{version}-windowsservercore-ltsc2019 --build-arg baseImage=ansys/stk/stk-engine-custom-baseline:{version}-windowsservercore-ltsc2019 .`
on the command line in this directory after replacing `{version}` with the version number. i.e `12.7.1`

### Run the Container
This image starts the `python` interpreter when starting the container.  You can verify that
STK Engine is working inside the `stk-engine-python` container with the following steps:
1. Run the following command from this directory after replacing `{version}` with the version number. i.e `12.7.1`:
`docker run -it --env-file ../configuration/licensing.env --name stk-python --rm ansys/stk/stk-engine-python:{version}-windowsservercore-ltsc2019`
2. Execute the following Python commands and verify it returns a valid response:
    ```python
    from agi.stk12.stkengine import STKEngine
    stk = STKEngine.StartApplication(noGraphics=True)
    print(stk.Version)
    ```
3. Quit the Python interpreter and the container by executing the command `quit()`

## Method 2 - Docker Compose

### Build the Image
1. If you built the `custom-environment` images described in the [Special Configuration](#special-configuration) section,
edit the [`docker-compose.yml`](./docker-compose.yml) file to set the value of the `baseImage` build argument to
`ansys/stk/stk-engine-custom-baseline:{version}-windowsservercore-ltsc2019` after replacing `{version}` with the version number. i.e `12.7.1`
2. Run `docker compose build` on the command line in this directory.

### Run the Container
This image starts the `python` interpreter when starting the container.  You can verify that
STK Engine is working inside the `stk-engine-python` container with the following steps:
1. On the command line, run `docker compose run --rm stk-engine-python`.
2. Execute the following Python commands and verify it returns a valid response:
    ```python
    from agi.stk12.stkengine import STKEngine
    stk = STKEngine.StartApplication(noGraphics=True)
    print(stk.Version)
    ```
3. Quit the Python interpreter and the container by executing the command `quit()`
