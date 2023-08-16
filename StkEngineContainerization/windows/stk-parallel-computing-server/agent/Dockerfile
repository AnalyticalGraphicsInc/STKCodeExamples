ARG agentBaseImage=ansys/stk/stk-parallel-computing-server-python:2.6-windowsservercore-ltsc2019
FROM ${agentBaseImage}
ARG agreeToLicense=no

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Add the STK Parallel Computing Server Agent packages to the image
COPY distributions scripts C:/TEMP/

LABEL NUMBER_OF_WORKER_PROCESSES="The number of worker processes to use. Must be compatible with STK Engine License. Default: 4" \
    COORDINATOR="The FQDN of the STK Parallel Computing Server Coordinator that controls this agent. Required."

USER ContainerAdministrator
# Install STK Parallel Computing Server Agent
RUN C:/temp/install.ps1 $Env:AgreeToLicense
USER ContainerUser

# Configure Environment
ENV NUMBER_OF_WORKER_PROCESSES=4

WORKDIR "C:/Program Files/AGI/STK Parallel Computing Server/Agent/bin"
# Start the agent
ENTRYPOINT ./AGI.Parallel.AgentService.exe --nostdin --coordinator="$Env:COORDINATOR"
