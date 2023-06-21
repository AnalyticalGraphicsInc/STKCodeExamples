# Custom Environment Base Image

## Purpose

Many of the images in this collection of code samples are built using the `yum` and `pip` tools to install package dependencies.  The images built by this project act as a layer on top of the baseline images which configure these tools to work in your environment. Like the baseline images themselves, the images produced by this project are not meant to be run on their own. Instead, they will be used as a baseline for other images in this code sample collection.

### Certificate Authority (CA) Certificates

If your network requires special CA certificates to download packages, copy your CA certificate (`*.crt`) files to the [`trusted-certificates`](./trusted-certificates/) folder at the same level as this file.

If your network doesn't require special CA certificates to download packages, delete the lines for `CA Certificates Configuration` as described in [`Dockerfile`](./Dockerfile).

### Yellowdog Updater Modifier (YUM)

If you need to override the default `yum` repository configuration to download RPM packages, copy your repository configuration (`*.repo`) files to the [`yum-repositories`](./yum-repositories/) folder at the same level as this file.

If you don't need to override the default `yum` repository configuration to download RPM packages, delete the lines for `YUM configuration` as described in [`Dockerfile`](./Dockerfile).

### Package Installer for Python (PIP)

If you need to override the default `pip` repository URL to download Python packages, set the `pipIndexUrl` build argument while building the image as described in the two "Build the Images" sections below.

If you don't need to override the default `pip` repository URL to download Python packages, delete the lines that set the `PIP_INDEX_URL` environment variable in the image as described in `PIP Configuration` in [`Dockerfile`](./Dockerfile).

## Method 1 - Docker CLI

### Build the Images

If you need to override the default `pip` repository URL to download Python packages, include the following command line option in both commands below: `--build-arg pipIndexUrl=<Your custom pip index url>`

> If you would like to use the base ubi image from IronBank you can change the `baseImage` in step 2 to `registry1.dso.mil/ironbank/redhat/ubi/ubi8:latest`.

1. Run `docker build -t ansys/stk/stk-engine-custom-baseline:{version}-ubi8 .` on the command line in this directory after replacing `{version}` with the version number. i.e `12.6.0` This will produce an image on top of the `stk-engine-baseline` image that is needed to build the code samples using
STK Engine, `yum`, and `pip` in your environment.
2. Run `docker build -t custom/redhat/ubi8:latest --build-arg baseImage=redhat/ubi8:latest .` on the command line in this directory. This will produce an image on top of the `redhat/ubi8:latest` baseline image that is needed to build the code samples using `yum` or `pip` without STK Engine in your environment.

## Method 2 - Docker Compose

### Build the Images

If you need to override the default `pip` repository URL to download Python packages, edit the `docker-compose.yml` file in this directory to set the `pipIndexUrl` for both images.

> If you would like to use the base ubi image from IronBank you can change which `baseImage` is commented in the `docker-compose.yml` file under the `custom-ubi8` service.

1. On the command line, run `docker compose build`. This will produce two images:
    * an image on top of the `stk-engine-baseline` image that is needed to build the code samples using STK Engine, `yum`, and `pip` in your environment.
    * an image on top of the `redhat/ubi8:latest` baseline image that is needed to build the code samples using `yum` or `pip` without STK Engine in your environment.
