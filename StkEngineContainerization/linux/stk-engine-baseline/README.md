# STK Engine Baseline Docker Image

## Purpose

This Docker image code sample demonstrates how to install STK Engine for Linux in a containerized environment.

## Prerequisites

* Docker must be running on your system.
* By default, this sample uses the `redhat/ubi8:latest` Docker image as its baseline. If you are not able to pull images directly from Docker Hub on your system, you must load the baseline OS image on your system before building this image.
* If you would like to use the base ubi image from IronBank you can uncomment the baseImage lines in the `Dockerfile` or `docker-compose.yml` files.
* Access to an Ansys Licensing Server with a valid STK license.  Edit the [`licensing.env`](../configuration/licensing.env) file to ensure the `ANSYSLMD_LICENSE_FILE` environment variable has your Ansys License Server information.

## Method 1 - Docker CLI

### Build the Image

1. Download version 12.4.0 or later of STK Engine for UNIX and STK Engine Data for UNIX from [https://support.agi.com/downloads](https://support.agi.com/downloads).
2. Unzip these files and copy the `stk_binaries_v${version}.tgz` and `stk_data_v${version}.tgz` into the [`distributions`](./distributions) folder at the same level as this file.
3. Run `docker build -t ansys/stk/stk-engine-baseline:{version}-ubi8 .` on the command line in this directory after replacing `{version}` with the version number. i.e `12.8.0`

### Run the Container

STK Engine requires a host application to run, so this baseline image does not specify an `ENTRYPOINT`. If you were to run a container from this image, it would exit immediately. However, you can verify that STK Engine is working inside the `stk-engine-baseline` container with the following steps:

1. Run the following command from this directory after replacing `{version}` with the version number. i.e `12.8.0`: `docker run -it --env-file ../configuration/licensing.env --name stk-baseline --rm ansys/stk/stk-engine-baseline:{version}-ubi8 connectconsole --interactive --nographics`
2. Execute a Connect command such as `GetSTKVersion /` and verify you receive a correct response.
3. Exit the Connect console by executing the command `exit`.

## Method 2 - Docker Compose

### Build the Image

1. Download version 12.4.0 or later of STK Engine for UNIX and STK Engine Data for UNIX from [https://support.agi.com/downloads](https://support.agi.com/downloads).
2. Unzip these files and copy the `stk_binaries_v${version}.tgz` and `stk_data_v${version}.tgz` into the [`distributions`](./distributions) folder at the same level as this file.
3. On the command line, run `docker compose build`.

### Run the Container

STK Engine requires a host application to run, so this baseline image does not specify an `ENTRYPOINT`. If you were to run a container from this image, it would exit immediately. However, you can verify that STK Engine is working inside the `stk-engine-baseline` container with the following steps:

1. On the command line, run `docker compose run --rm stk-engine-baseline connectconsole --interactive --nographics`.
2. Execute a Connect command such as `GetSTKVersion /` and verify you receive a correct response.
3. Exit the Connect console by typing the command `exit`.
