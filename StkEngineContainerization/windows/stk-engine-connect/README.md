# STK Engine Connect Docker Image

## Purpose

This Docker image code sample demonstrates how to run the STK Engine for Windows Connect console in a containerized environment.

## Prerequisites

* Docker must be running on your system.
* Access to an Ansys Licensing Server with a valid STK license. Edit the [`licensing.env`](../configuration/licensing.env) file to ensure the `ANSYSLMD_LICENSE_FILE` environment variable has your Ansys License Server information.
* You have already built the `stk-engine-baseline` image.

## Method 1 - Docker CLI

### Build the Image

1. Run `docker build -t ansys/stk/stk-engine-connect:{version}-windowsservercore-ltsc2019 .` on the command line in this directory after replacing `{version}` with the version number. i.e `12.9.0`

### Run the Container

The entrypoint of this container starts the Connect console in the foreground listening on the container's port `5001`. To start the container and verify its functionality:

1. Run the following command from this directory after replacing `{version}` with the version number. i.e `12.9.0`: `docker run -d -p 5001:5001 --env-file ../configuration/licensing.env --name stk-connect --rm ansys/stk/stk-engine-connect:{version}-windowsservercore-ltsc2019`
    * If port `5001` is already in use on your machine, map a different port (e.g. `1234:5001`).
2. After the container has started, use a TCP client to connect to the container at `localhost:5001`. If you changed the host port mapping in the step above, use that port here instead of `5001`.
3. Execute a Connect command in the TCP client, such as `GetSTKVersion /` and verify you receive a correct response.
4. Shut down the container by typing the command `docker stop stk-connect`.

## Method 2 - Docker Compose

### Build the Image

1. Run `docker compose build` on the command line in this directory.

### Run the Container

The entrypoint of this container starts the Connect console in the foreground listening on the container's port `5001`. To start the container and verify its functionality:

1. Run the following command from this directory: `docker compose up -d`.
    * If port `5001` is already in use on your machine, map a different port (e.g. `1234:5001`) in the `docker-compose.yml` file before executing step 1.
2. After the container has started, use a TCP client to connect to `localhost:5001`. If you changed the host port mapping above, use that port here instead of `5001`.
3. Execute a Connect command in the TCP client, such as `GetSTKVersion /` and verify you receive a correct response.
4. Shut down the container by typing the command `docker compose down`.
