# By default, start from the STK Engine Python API image
ARG baseImage=ansys/stk/stk-engine-python:12.5.0-windowsservercore-ltsc2019
FROM ${baseImage}

LABEL JUPYTER_LAB_TOKEN='The token used to initially authenticate with the jupyter lab application. Required.'

# Install jupyterlab
RUN pip install --no-cache-dir --user jupyterlab; \
    New-Item -Path 'C:/notebooks' | Out-Null

# Mount notebooks from the local file system
VOLUME [ "C:/notebooks" ]

# Expose port for jupyterlab
EXPOSE 8888

# Runs the Jupyter Lab server in the foreground.
COPY ["docker-entrypoint.ps1", "C:/Program Files/AGI/STK 12/bin/"]
ENTRYPOINT ["powershell", "-File", "C:/Program Files/AGI/STK 12/bin/docker-entrypoint.ps1"]

# Jupyter Lab recommonds using the tini init tool when running in a container.
# If running this container with a "docker run" command instead of using the provided
# docker-compose.yml file, be sure to pass the "--init" flag to the "docker run" command.
