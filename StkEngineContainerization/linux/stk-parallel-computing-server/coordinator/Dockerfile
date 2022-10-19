ARG baseImage=centos:7
FROM ${baseImage} AS builder

# Add the STK Parallel Computing Server Coordinator packages to the builder stage
COPY distributions/STK_Parallel_Computing_Coordinator*.tgz /tmp/

# Unpack the STK Parallel Computing Server Coordinator
RUN set -e; \
    mkdir -p /tmp/coordinator; \
    find /tmp -name STK_Parallel_Computing_Coordinator*.tgz -exec tar -zxf {} --strip-components=1 -C /tmp/coordinator \;


FROM ${baseImage}

# Install .NET Core dependencies (see https://docs.microsoft.com/en-us/dotnet/core/install/linux-centos)
RUN set -e; \
    yum -y install krb5-libs; \
    yum -y install libicu; \
    yum -y install openssl-libs; \
    yum -y install zlib; \
    yum clean all; \
    rm -rf /var/cache/yum

# Define STK user home directory
ENV STK_USER_HOME=/home/stk

# Setup non-root STK user
RUN useradd -ms /bin/bash stk

# Switch to STK user
WORKDIR "${STK_USER_HOME}"
USER stk

# Copy parallel computing coordinator from the builder
COPY --from=builder --chown=stk /tmp/coordinator/ "${STK_USER_HOME}"/Coordinator/

#Configure Environment
ENV PATH="${STK_USER_HOME}"/Coordinator/bin:"${PATH}"

# Expose the Coordinator port
EXPOSE 9090

# Start the coordinator
ENTRYPOINT AGI.Parallel.CoordinatorService --nostdin
