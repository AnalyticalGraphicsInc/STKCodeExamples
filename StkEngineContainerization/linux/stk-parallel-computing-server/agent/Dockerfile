ARG baseImage=ansys/stk/stk-parallel-computing-server-python:2.5-centos7
FROM ${baseImage} AS builder

# Add the STK Parallel Computing Server Agent packages to the builder stage
COPY distributions/STK_Parallel_Computing_Agent*.tgz /tmp/

# Unpack the STK Parallel Computing Server Agent
RUN set -e; \
    mkdir -p /tmp/agent; \
    find /tmp -name STK_Parallel_Computing_Agent*.tgz -exec tar -xf {} --strip-components=1 -C /tmp/agent \;


FROM ${baseImage}

LABEL NUMBER_OF_WORKER_PROCESSES="The number of worker processes to use.  Must be compatible with STK Engine License. Default: 4" \
    COORDINATOR="The FQDN of the STK Parallel Computing Server Coordinator that controls this agent. Required."

# Install .NET Core dependencies (see https://docs.microsoft.com/en-us/dotnet/core/install/linux-centos)
USER root
RUN set -e; \
    yum -y install krb5-libs; \
    yum -y install libicu; \
    yum -y install openssl-libs; \
    yum -y install zlib; \
    yum clean all; \
    rm -rf /var/cache/yum
USER stk

# Copy parallel computing agent from the builder
COPY --from=builder --chown=stk /tmp/agent/ "${STK_USER_HOME}"/Agent

#Configure Environment
ENV PATH="${STK_USER_HOME}"/Agent/bin:"${PATH}" \
    NUMBER_OF_WORKER_PROCESSES=4

# Start the agent
ENTRYPOINT AGI.Parallel.AgentService --nostdin --coordinator="${COORDINATOR}"
