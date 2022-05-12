# By default, start from the base STK Engine image
ARG baseImage=ansys/stk/stk-engine-baseline:12.4.0-centos7
FROM ${baseImage}

# Install Python
USER root
RUN set -e; \
    yum -y install python3; \
    yum clean all; \
    rm -rf /var/cache/yum

# Switch back to non-root user
USER stk

# Update the path to include Python executables
ENV PATH="${STK_USER_HOME}/.local/bin:${PATH}" \
    PIP_DEFAULT_TIMEOUT=600 \
    PIP_RETRY=50

# Install the STK Engine Python API module
RUN set -e; \
    python3 -m pip install --no-cache-dir --upgrade --user pip; \
    pip install --no-cache-dir --user wheel; \
    pip install --no-cache-dir --user "${STK_USER_HOME}/bin/AgPythonAPI/"agi.stk*.whl

ENTRYPOINT [ "python3" ]