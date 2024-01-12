# STK Engine Flask Webservice Example Docker Image

## Purpose

This Docker image code sample demonstrates how to run STK Engine for Linux through a web service in a containerized environment.  It uses a default Place object and a default Satellite object to calculate access intervals for given start and stop times. **Note: This example uses the Flask development server, so it is not intended for a production deployment.**

## Prerequisites

* Docker must be running on your system.
* Access to an Ansys Licensing Server with a valid STK license.  Edit the [`licensing.env`](../configuration/licensing.env) file to ensure the `ANSYSLMD_LICENSE_FILE` environment variable has your Ansys License Server information.
* You have already built the `stk-engine-python` image.

## Method 1 - Docker CLI

### Build the Image

1. Run `docker build -t ansys/stk/stk-engine-webservice:{version}-ubi8 .` on the command line in this directory after replacing `{version}` with the version number. i.e `12.8.0`

### Run the Container

The entrypoint of this container starts the Flask development server in the foreground, listening on the container's port `5000`. To start the container and verify its functionality:

1. Run the following command from this directory after replacing `{version}` with the version number. i.e `12.8.0`: `docker run -d -p 5000:5000 --env-file ../configuration/licensing.env --name stk-engine-webservice --rm ansys/stk/stk-engine-webservice:{version}-ubi8`
    * If port `5000` is already in use on your machine, map a different port (e.g. `1234:5000`).
2. After the container has started, use a web browser to navigate to `http://localhost:5000/access?startTime=2022-01-01T12:00:00.000-05:00&stopTime=2022-01-02T12:00:00.000-05:00`
    * If you changed the host port mapping above, use that port here instead of `5000`.
    * The `startTime` and `stopTime` in the URL can be set to any ISO-8601 formatted date/time string, as long as the stop time is after the start time.
    * It may take a few seconds for the web service application to be ready to accept requests after the container starts.  If this url does not work initially, wait a few seconds for the app to be ready and refresh the page.
3. Verify the request returns some access intervals in JSON format.  Example:

    ```JSON
        {
            "accessIntervals":[
                {"start":"...","stop":"..."},
                {"start":"...","stop":"..."},
                {"start":"...","stop":"..."},
                {"start":"...","stop":"..."}
            ]
        }
    ```

4. On the command line, run `docker stop stk-engine-webservice`

## Method 2 - Docker Compose

### Build the Image

1. On the command line, run `docker compose build`.

### Run the Container

The entrypoint of this container starts the Flask development server in the foreground, listening on the container's port `5000`. To start the container and verify its functionality:

1. On the command line, run `docker compose up -d`.
    * If port `5000` is already in use on your machine, map a different port (e.g. `1234:5000`) in     the [`docker-compose.yml`](./docker-compose.yml) file before executing step 1.
2. After the container has started, use a web browser to navigate to `http://localhost:5000/access?startTime=2022-01-01T12:00:00.000-05:00&stopTime=2022-01-02T12:00:00.000-05:00`
    * If you changed the host port mapping in the `docker-compose.yml`, use that port here instead of `5000`.
    * The `startTime` and `stopTime` in the URL can be set to any ISO-8601 formatted date/time string, as long as the stop time is after the start time.
    * It may take a few seconds for the web service application to be ready to accept requests after the container starts.  If this url does not work initially, wait a few seconds for the app to be ready and refresh the page.
3. Verify the request returns some access intervals in JSON format.  Example:

    ```JSON
        {
            "accessIntervals":[
                {"start":"...","stop":"..."},
                {"start":"...","stop":"..."},
                {"start":"...","stop":"..."},
                {"start":"...","stop":"..."}
            ]
        }
    ```

4. On the command line, run `docker compose down`
