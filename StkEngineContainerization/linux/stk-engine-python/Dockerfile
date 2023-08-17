# By default, start from the base STK Engine image
# Uncomment the first line below if you built the stk-engine-custom-baseline image
# ARG baseImage=ansys/stk/stk-engine-custom-baseline:12.7.1-ubi8
ARG baseImage=ansys/stk/stk-engine-custom-baseline:12.7.1-ubi8
FROM ${baseImage}

# Install Python
USER root
RUN set -e; \
    yum install python39 -y

# Switch back to non-root user
USER stk

# Update the path to include Python executables
ENV PATH="${STK_USER_HOME}/.local/bin:${PATH}" \
    PIP_DEFAULT_TIMEOUT=600 \
    PIP_RETRY=50

# Install the STK Engine Python API module
RUN set -e; \
    pip3 install --no-cache-dir --upgrade --user pip; \
    pip3 install --no-cache-dir --user wheel; \
    pip3 install --no-cache-dir --user "${STK_USER_HOME}/bin/AgPythonAPI/"agi.stk*.whl

# Use entrypoint script to run Python commands
COPY --chown=stk docker-entrypoint.sh "${STK_USER_HOME}/bin/docker-entrypoint.sh"
RUN chmod +x "${STK_USER_HOME}/bin/docker-entrypoint.sh"
ENTRYPOINT [ "docker-entrypoint.sh" ]
