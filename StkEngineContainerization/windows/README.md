# STK Engine Containerization

This collection of code samples demonstrate containerization of STK Engine on Windows.

Each example project provides:
* A `README.md` file with instructions and explanations on what the example does and how to build and run the container
either using directly the Docker command line or using Docker Compose (i.e., using the docker-compose.yml file in the
same folder)
* A `Dockerfile` containing all the commands to assemble the image
* A `docker-compose.yml` file, enabling the use of Docker Compose to build and run the images

The images in these examples are built upon each other, and you need to build them in the correct order.
Since almost all examples eventually derive from `stk-engine-baseline`, we recommend starting there.
After that, if you have a specific example you'd like to experiment with, navigate to that folder's `README.md` file and
follow the prerequisites section to determine what images need to be built for that example to work.

> Note: These examples have been designed to be easily understood and broadly compatible. We encourage you to explore the Docker documentation and optimize the images for your requirements.

Further information can be found in the AGI Programming Help.

The following is a list of the examples in this collection along with a description of what they demonstrate.

## [Base STK Engine Docker image](stk-engine-baseline)
Provides the STK Engine installation with the runtime configuration (environment variables) to run STK Engine applications.

## [Custom Environment](custom-environment)
Provides the environment required to communicate with Pip package manager in your organization.
This is optional if you are directly connected to the internet. It is required if you are using a proxy/firewall or
isolated network requiring different certificates, settings, or both.

## [Connect STK Engine Docker image](stk-engine-connect)
Derives from the base STK Engine image and runs the connectconsole executable to expose Connect through a socket from
the container.

## [Python STK Engine Docker image](stk-engine-python)
Derives from the base STK Engine image and adds Python to the image.

## [Jupyter STK Engine Docker image](stk-engine-jupyter)
Derives from the Python STK Engine image and exposes JupyterLab from the container. Go to JupyterLab in your web browser
to exercise Python notebooks using STK Engine for computations. AGI also provides an example notebook.

## [Custom STK Engine Service Docker image](stk-engine-webservice)
Derives from the Python STK Engine image and shows how to develop and include your own web service(s) in the Docker
image. You can write those web services in Java, Python, and also with .NET if using a Windows container.
This is just a bare-bones minimal web service example using the Flask development server and is not production ready.
Please refer to the Flask documentation for best practices on how to create and deploy a production-ready web service.

## [STK Parallel Computing Server example](stk-parallel-computing-server)
Provides three example images and composes two of them together to run a full STK Parallel Computing Server cluster.
The first image, in the coordinator subdirectory, contains the STK Parallel Computing Server Coordinator.
The second image, in the Python subdirectory, derives from the Python STK Engine image and includes the
STK Parallel Computing Python API. The third image, in the agent subdirectory, derives from the Python image in this
example and contains the STK Parallel Computing Server Agent. This project includes an example client script that
demonstrates how to exercise the STK Parallel Computing Java/.NET/Python APIs to drive batch STK Engine computations
and tasks.
