"""
MonteCarlo without STK

Position is randomly distributed within this area (Lat, Lon):
        36.976436, -121.441276
        36.877462, -121.373787
        36.963635, -121.498569
    36.866974, -121.436421

Roll
        80% of time -10 deg ~ 10 deg
        15%: +-10deg ~ +-30 deg
        5%: +- 30deg ~ +-60deg
        or a Gaussian distribution -60deg to 60deg

Pitch
        90%: -5deg ~ + 5deg
        5%: +-5deg ~ +-10deg
        5%: +-19deg ~ +-30deg
        or a Gaussian distribution -30deg to 30deg

Heading
        Random

Altitude
    100ft ~ 4000ft

Outputs: CSV with monte carlo states
"""

import csv
import numpy as np
import matplotlib.pyplot as plt

NUM_TRIALS = 10

LAT_MIN = 36.866974  # deg
LAT_MAX = 36.976436
LON_MIN = -121.498569
LON_MAX = -121.373787

ALT_MIN = 100  # ft
ALT_MAX = 40000  # ft

PRINT_TO_CSV = True
OUTPUT_CSV = "output.csv"


def sample_roll():
    # choose probability bucket
    r = np.random.rand()

    if r < 0.80:
        # 80% chance: -10 to 10 deg
        return np.random.uniform(-10, 10)

    elif r < 0.95:
        # 15% chance: +/- 10 to +/- 30 deg
        sign = np.random.choice([-1, 1])
        return sign * np.random.uniform(10, 30)

    else:
        # 5% chance: +/- 30 to +/- 60 deg
        sign = np.random.choice([-1, 1])
        return sign * np.random.uniform(30, 60)


def sample_pitch():
    # choose probability bucket
    r = np.random.rand()

    if r < 0.90:
        # 90% chance: -5 to 5 deg
        return np.random.uniform(-5, 5)

    elif r < 0.95:
        # 5% chance: +/- 5 to +/- 10 deg
        sign = np.random.choice([-1, 1])
        return sign * np.random.uniform(5, 10)

    else:
        # 5% chance: +/- 19 to +/- 30 deg
        sign = np.random.choice([-1, 1])
        return sign * np.random.uniform(19, 30)


def sample_heading():
    return np.random.uniform(0, 360)


def sample_altitude():
    return np.random.uniform(ALT_MIN, ALT_MAX)


def sample_lat():
    return np.random.uniform(LAT_MIN, LAT_MAX)


def sample_lon():
    return np.random.uniform(LON_MIN, LON_MAX)


def sample_time():
    return int(np.random.uniform(0, 172800))  # range of 0 to 48 hours in seconds


def run_monte_carlo(num_trials):
    time = []
    latitudes = []
    longitudes = []
    rolls = []
    pitches = []
    headings = []
    altitudes = []

    for i in range(num_trials):
        time.append(sample_time())
        latitudes.append(sample_lat())
        longitudes.append(sample_lon())
        rolls.append(sample_roll())
        pitches.append(sample_pitch())
        headings.append(sample_heading())
        altitudes.append(sample_altitude())

    return {
        "time": np.array(time),
        "lat": np.array(latitudes),
        "lon": np.array(longitudes),
        "roll": np.array(rolls),  # rotation about body X
        "pitch": np.array(pitches),  # rotation about body Y
        "heading": np.array(headings),  # rotation about body Z
        "altitude": np.array(altitudes),
    }


def main():
    # RUN MONTE CARLO AND GENERATE PLOTS

    if PRINT_TO_CSV:
        with open(OUTPUT_CSV, "w", newline="") as f:
            writer = csv.writer(f)
            print("writing to file...")

            # Header row
            writer.writerow(["time", "lat", "lon", "pitch", "heading", "altitude"])
            for i in range(NUM_TRIALS):
                writer.writerow(
                    [
                        states["time"][i],
                        states["lat"][i],
                        states["lon"][i],
                        states["roll"][i],
                        states["pitch"][i],
                        states["heading"][i],
                        states["altitude"][i],
                    ]
                )

    print(f"Saved states to {OUTPUT_CSV}")

    # Lat/Lon scatter
    plt.scatter(states["lat"], states["lon"], s=3, alpha=0.5)
    plt.title("Random Latitude/Longitude Samples")
    plt.xlabel("Latitude")
    plt.ylabel("Longitude")
    plt.grid(True)
    plt.show()
    return


if __name__ == "__main__":
    main()
