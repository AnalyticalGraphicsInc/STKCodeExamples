# STK Engine Baseline Docker Image

## Purpose
This Docker image code sample demonstrates how to install STK Engine for Windows in a containerized environment.

## Prerequisites
* Docker must be running on your system.
* By default, this sample uses the `mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2019` Docker image 
as its baseline. If you are not able to pull images directly from Docker Hub on your system, you must load the baseline 
OS image on your system before building this image. If you need a different base image due to OS compatibility or 
additional software requirements, we recommend that the image have the .NET 4.8 Framework installed. Note: More information on
Windows Container compatibility can be found at https://docs.microsoft.com/en-us/virtualization/windowscontainers/deploy-containers/version-compatibility.
* Access to an Ansys Licensing Server with a valid STK license. Edit the
[`licensing.env`](../configuration/licensing.env) file to ensure the `ANSYSLMD_LICENSE_FILE` environment variable
has your Ansys License Server information.

## Method 1 - Docker CLI

### Build the Image
1. Download version 12.5.0 or later of STK Engine for Windows from https://support.agi.com/downloads. 
This example will assume version 12.5.0.
2. Place the `STK_Engine_v12.5.0.zip` file in the
[`distributions`](./distributions) folder at the same level as this file.
3. Run `docker build -t ansys/stk/stk-engine-baseline:12.5.0-windowsservercore-ltsc2019 --build-arg agreeToLicense=yes .` 
on the commandline in this directory.

### Run the Container
STK Engine requires a host application to run, so this baseline image does not specify an `ENTRYPOINT`.
If you were to run a container from this image, it would exit immediately.
However, you can verify that STK Engine is working inside the `stk-engine-baseline` container with the following steps:
1. Run the following command from this directory:
`docker run -it --name stk-baseline --env-file ..\configuration\licensing.env --rm ansys/stk/stk-engine-baseline:12.5.0-windowsservercore-ltsc2019 ConnectConsole.exe /interactive /noGraphics`
2. Execute a Connect command such as `GetSTKVersion /` and verify you receive a correct response.
3. Exit the Connect console by executing the command `exit`.

## Method 2 - Docker Compose

### Build the Image
1. Download version 12.5.0 or later of STK Engine for Windows from
https://support.agi.com/downloads. This example will assume version 12.5.0.
2. Place the `STK_Engine_v12.5.0.zip` file in the
[`distributions`](./distributions) folder at the same level as this file.
3. On the command line, run `docker compose build --build-arg agreeToLicense=yes`.

### Run the Container
STK Engine requires a host application to run, so this baseline image does not specify an `ENTRYPOINT`.
If you were to run a container from this image, it would exit immediately.
However, you can verify that STK Engine is working inside the `stk-engine-baseline` container with the following steps:
1. On the command line, run `docker compose run --rm stk-engine-baseline connectconsole.exe /interactive /noGraphics`.
2. Execute a Connect command such as `GetSTKVersion /` and verify you receive a correct response.
3. Exit the Connect console by typing the command `exit`.
