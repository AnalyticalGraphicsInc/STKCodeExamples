ARG baseImage=centos:7
FROM ${baseImage} AS builder

# Add the engine packages to the builder stage
COPY distributions/stk*.tgz /tmp/

# Unpack engine
RUN set -e; \
    mkdir -p /stk; \
    find /tmp -name stk*.tgz -exec tar -zxf {} --strip-components=1 -C /stk \;

# Reduce image size by removing unneccessary components
RUN set -e; \
    rm -rf /stk/Data/ExampleScenarios/; \
    rm -rf /stk/Data/HtmlInterface/; \
    rm -rf /stk/Data/HtmlUtilities/; \
    rm -rf /stk/Data/LicAndReg/; \
    rm -rf /stk/Data/Resources/; \
    rm -rf /stk/Data/Viewer/; \
    rm -rf /stk/STKData/VO; \
    rm -rf /stk/licensingclient/linx64/ClientSettings

# Set the base image for the next stage
FROM ${baseImage}

LABEL ANSYSLMD_LICENSE_FILE='Specifies the location of the Ansys Licensing Server. The format should be PORT@FQDN. If using a triad of license server, list all servers in the same format separated by semicolons.  Required.'

# Define STK user home directory
ENV STK_USER_HOME=/home/stk

# Setup non-root STK user
RUN useradd -ms /bin/bash stk

# Switch to STK user
WORKDIR "${STK_USER_HOME}"
USER stk

# Copy files from the builder stage
COPY --from=builder --chown=stk /stk/ "${STK_USER_HOME}"/

# Configure environment
ENV LD_LIBRARY_PATH="${STK_USER_HOME}/bin" \
    PATH="${STK_USER_HOME}/bin:${PATH}" \
    STK_CONFIG_DIR="${STK_USER_HOME}/config" \
    STK_INSTALL_DIR="${STK_USER_HOME}"

# Run new user install
RUN stkxnewuser /force /allowOnline=yes

CMD echo 'STK Engine requires a host application to run.  Shutting down this container.'
