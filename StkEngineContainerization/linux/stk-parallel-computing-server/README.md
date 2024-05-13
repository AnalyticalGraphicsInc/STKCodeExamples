# STK Parallel Computing Server

## Purpose

This code sample demonstrates how to run the STK Parallel Computing Server in a containerized environment.

## Prerequisites

* Docker must be running on your system.
* Access to an Ansys Licensing Server with a valid STK license.  Edit the [`licensing.env`](../configuration/licensing.env) file to ensure the `ANSYSLMD_LICENSE_FILE` environment variable has your Ansys License Server information.
* You have already built the `stk-engine-python` image.

## Special Configuration

The STK Parallel Computing Server Coordinator image is built using the `yum` and `pip` tools to install package dependencies. Please see the [`custom-environment`](../custom-environment/README.md) Docker image code sample project and build the `custom/redhat/ubi8:latest` image, if needed, before proceeding with this image.

## Method 1 - Docker CLI

Note: Since STK Parallel Computing Server consists of multiple containers running simultaneously and communicating over Docker networking, [Method 2 - Docker Compose](#method-2---docker-compose) is highly recommended for this code sample.

### Build the Images

1. Download and unzip the latest version of STK Parallel Computing Server from [https://support.agi.com/downloads](https://support.agi.com/downloads).
2. From the extracted STK Parallel Computing Server archive:
    * Copy the `agiparallel-${version}-py3-none-any.whl` file (this contains the `agiparallel` Python module):
        1. Unzip `Server/SDKs/STKParallelComputingServerApiPython.zip`.
        2. From the folder extracted in step 1, copy the `dist/agiparallel-${version}-py3-none-any.whl` to the [`python/distributions`](./python/distributions) folder.
    * Copy the `STK_Parallel_Computing_Coordinator${version}.tgz` from the `Linux` folder to the [`coordinator/distributions`](./coordinator/distributions/) folder.
    * Copy the `STK_Parallel_Computing_Agent${version}.tgz` from the `Linux` folder to the [`agent/distributions`](./agent/distributions/) folder.
3. Build the coordinator image:
    * If you did not build the `custom-environment` image described in the [Special Configuration](#special-configuration) section, run `docker build -t ansys/stk/stk-parallel-computing-server-coordinator:{version}-ubi-8 coordinator` on the command line in this directory after replacing `{version}` with the version number. i.e `2.8.0`
    * If you did build the `custom-environment` image described in the [Special Configuration](#special-configuration) section, run `docker build -t ansys/stk/stk-parallel-computing-server-coordinator:{version}-ubi-8 --build-arg baseImage=custom/redhat/ubi8:latest coordinator` on the command line in this directory after replacing `{version}` with the version number. i.e `2.8.0`
4. Run `docker build -t ansys/stk/stk-parallel-computing-server-python:{version}-ubi-8 python` on the command line in this directory after replacing `{version}` with the version number. i.e `2.8.0`.  This produces an image that includes the STK Parallel Computing Server Python API.  The Agent container extends from this, but it can also be used to execute a client script that submits jobs to the Coordinator service.
5. Run `docker build -t ansys/stk/stk-parallel-computing-server-agent:{version}-ubi-8 agent` on the command line in this directory after replacing `{version}` with the version number. i.e `2.8.0` to build the agent image.

### Run the Containers

#### Start the STK Parallel Computing Services

The entrypoint of these containers start the Coordinator and Agent servers, with the Coordinator listening on the container's port `9090`. To start the containers:

1. Run `docker network create stk-parallel-computing-server` on the command line in this directory.
2. Run `docker run -d --rm --name stk-parallel-coordinator --network stk-parallel-computing-server --network-alias coordinator -p 9090:9090 ansys/stk/stk-parallel-computing-server-coordinator:{version}-ubi-8` on the command line in this directory after replacing `{version}` with the version number. i.e `2.8.0`
    * If port `9090` is already in use on your machine, map a different port (e.g. `1234:9090`).
3. Run `docker run -d --rm --name stk-parallel-agent --network stk-parallel-computing-server --network-alias agent --env-file ../configuration/licensing.env -e COORDINATOR=coordinator ansys/stk/stk-parallel-computing-server-agent:{version}-ubi-8`
on the command line in this directory after replacing `{version}` with the version number. i.e `2.8.0`

#### Execute Parallel Computing Tasks

This section will show how to execute the [`client_example.py`](./client_example.py) script using a Docker container with Python 3 and the STK Parallel Computing Server Python API installed.

In this example, each task will compute access intervals between a default satellite object and a default place object given the start and stop times of the analysis interval.  Each task's interval is defined as an entry in the `timeIntervals` list.  You can edit this list directly in the script file to add more calculation intervals or modify those already there. The date-times must be in valid ISO-8601 format.

1. Run `docker run --rm -v <ABSOLUTE PATH TO THIS DIRECTORY>/client_example.py:/tmp/client_example.py -e COORDINATOR_HOSTNAME=coordinator -e COORDINATOR_PORT=9090 -w /tmp --network stk-parallel-computing-server ansys/stk/stk-parallel-computing-server-python:{version}-ubi-8 client_example.py` on the command line in this directory after replacing `{version}` with the version number. i.e `2.8.0`
    * If you changed the host port mapping for the coordinator above, use that port for the value of `COORDINATOR_PORT`
    instead of `9090`.

#### Cleanup

1. Run `docker stop stk-parallel-agent` on the command line in this directory.
2. Run `docker stop stk-parallel-coordinator` on the command line in this directory.
3. Run `docker network rm stk-parallel-computing-server` on the command line in this directory.

## Method 2 - Docker Compose

### Build the Images

1. Download and unzip the latest version of STK Parallel Computing Server from [https://support.agi.com/downloads](https://support.agi.com/downloads).
2. From the extracted STK Parallel Computing Server archive:
    * Copy the `agiparallel-${version}-py3-none-any.whl` file (this contains the `agiparallel` Python module) from the `Server/SDKs/STKParallelComputingServerApiPython.zip/dist` folder to the [`python/distributions`](./python/distributions) folder.
    * Copy the `STK_Parallel_Computing_Coordinator${version}.tgz` from the `Linux` folder to the [`coordinator/distributions`](./coordinator/distributions/) folder.
    * Copy the `STK_Parallel_Computing_Agent${version}.tgz` from the `Linux` folder to the [`agent/distributions`](./agent/distributions/) folder.
3. If you built the `custom-environment` image described in the [Special Configuration](#special-configuration) section, edit the [`docker-compose.yml`](./docker-compose.yml) file to set the value of the `baseImage` build argument to `custom/redhat/ubi8:latest` for the `coordinator` service.
4. Run `docker compose -f docker-compose.python.yml build` on the command line in this directory. This produces an image that includes the STK Parallel Computing Server Python API.  The Agent container extends from this, but it can also be used to execute a client script that submits jobs to the Coordinator service.
5. Run `docker compose build` on the command line in this directory. This builds the STK Parallel Computing Server Agent and Coordinator images.

### Run the Containers

#### Start the STK Parallel Computing Services

The entrypoint of these containers start the Coordinator and Agent servers, with the Coordinator listening on the container's port `9090`. To start the containers:

1. Run `docker compose up -d --scale agent=2` on the command line in this directory.
    * If port `9090` is already in use on your machine, map a different port (e.g. `1234:9090`) in     the `coordinator` service of the [`docker-compose.yml`](./docker-compose.yml) file before executing step 1.
    * Note: The provided `client_example.py` script executes two tasks by default, so, for demonstration purposes, this command starts two agent containers.  If you are unable to start multiple agents because of licensing restrictions, remove the `--scale agent=2` part of the command.

#### Execute Parallel Computing Tasks

This section will show how to execute the [`client_example.py`](./client_example.py) script using a Docker container with Python 3 and the STK Parallel Computing Server Python API installed.

In this example, each task will compute access intervals between a default satellite object and a default place object
given the start and stop times of the analysis interval.  Each task's interval is defined as an entry in the `timeIntervals` list.  You can edit this list directly in the script file to add more calculation intervals or modify those already there.  The date-times must be in valid ISO-8601 format.

1. Run `docker compose -f docker-compose.python.yml run --rm python client_example.py` on the command line in this directory.
    * If you changed the host port mapping for the coordinator above, use that port for the value of `COORDINATOR_PORT`
    in the [`docker-compose.python.yml`](./docker-compose.python.yml) instead of `9090`.

#### Cleanup

1. Run `docker compose down` on the command line in this directory.
