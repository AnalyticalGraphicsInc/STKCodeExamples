# Uncomment the line below if you want to use the UBI image from ironbank
# ARG baseImage=registry1.dso.mil/ironbank/redhat/ubi/ubi8:latest

# Uncomment the line below if you built the custom/redhat/ubi8 image
# ARG baseImage=custom/redhat/ubi8:latest

ARG baseImage=redhat/ubi8:latest
FROM ${baseImage} AS builder

# Add the STK Parallel Computing Server Coordinator packages to the builder stage
COPY distributions/STK_Parallel_Computing_Coordinator*.tgz /tmp/

# Unpack the STK Parallel Computing Server Coordinator
RUN set -e; \
    mkdir -p /tmp/coordinator; \
    find /tmp -name STK_Parallel_Computing_Coordinator*.tgz -exec tar -zxf {} --strip-components=1 -C /tmp/coordinator \;


FROM ${baseImage}

USER root
# Install .NET Core dependencies
RUN set -e; \
    yum -y install aspnetcore-runtime-6.0

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
