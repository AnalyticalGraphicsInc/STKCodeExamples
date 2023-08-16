ARG coordinatorBaseImage=mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2019
FROM ${coordinatorBaseImage}
ARG agreeToLicense=no

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Add the STK Parallel Computing Server Coordinator packages to the image
COPY distributions scripts C:/temp/

USER ContainerAdministrator

# Install STK Parallel Computing Server Coordinator
RUN C:\temp\install.ps1 $Env:agreeToLicense

USER ContainerUser

# Expose the Coordinator port
EXPOSE 9090

WORKDIR "C:/Program Files/AGI/STK Parallel Computing Server/Coordinator/bin"
# Start the coordinator
ENTRYPOINT ./AGI.Parallel.CoordinatorService.exe --nostdin
