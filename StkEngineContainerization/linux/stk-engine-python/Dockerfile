# By default, start from the base STK Engine image
ARG baseImage=ansys/stk/stk-engine-baseline:12.6.0-centos7
FROM ${baseImage}

# Install Python
USER root
ENV PYTHON_PACKAGE=rh-python38
RUN set -e; \
    if ! yum list | grep "${PYTHON_PACKAGE}">/dev/null; then \
        yum -y install centos-release-scl; \
    fi; \
    yum -y install "${PYTHON_PACKAGE}"; \
    echo "source scl_source enable ${PYTHON_PACKAGE}" > /etc/profile.d/enable-python.sh; \
    yum clean all; \
    rm -rf /var/cache/yum
SHELL [ "/bin/sh", "--login", "-c" ]

# Switch back to non-root user
USER stk

# Update the path to include Python executables
ENV PATH="${STK_USER_HOME}/.local/bin:${PATH}" \
    PIP_DEFAULT_TIMEOUT=600 \
    PIP_RETRY=50

# Install the STK Engine Python API module
RUN set -e; \
    pip install --no-cache-dir --upgrade --user pip; \
    pip install --no-cache-dir --user wheel; \
    pip install --no-cache-dir --user "${STK_USER_HOME}/bin/AgPythonAPI/"agi.stk*.whl

# Use entrypoint script to run Python commands
COPY --chown=stk docker-entrypoint.sh "${STK_USER_HOME}/bin/docker-entrypoint.sh"
RUN chmod +x "${STK_USER_HOME}/bin/docker-entrypoint.sh"
ENTRYPOINT [ "docker-entrypoint.sh" ]
