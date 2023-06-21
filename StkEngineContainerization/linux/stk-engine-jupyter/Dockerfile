# By default, start from the STK Engine Python API image
ARG baseImage=ansys/stk/stk-engine-python:12.6.0-ubi8
FROM ${baseImage}

LABEL JUPYTER_LAB_TOKEN='The token used to initially authenticate with the jupyter lab application. Required.'

# Install jupyterlab
RUN set -e; \
    pip3 install --no-cache-dir --user jupyterlab; \
    mkdir -p "${STK_USER_HOME}"/notebooks

# Mount notebooks from the local file system
VOLUME [ "${STK_USER_HOME}/notebooks" ]

# Expose port for jupyterlab
EXPOSE 8888

# Runs the Jupyter Lab server in the foreground.
COPY --chown=stk docker-entrypoint.sh "${STK_USER_HOME}"/bin
RUN chmod +x "${STK_USER_HOME}"/bin/docker-entrypoint.sh
ENTRYPOINT docker-entrypoint.sh

# Jupyter Lab recommonds using the tini init tool when running in a container.
# If running this container with a "docker run" command instead of using the provided
# docker-compose.yml file, be sure to pass the "--init" flag to the "docker run" command.
