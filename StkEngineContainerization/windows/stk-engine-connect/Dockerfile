# By default, start from the base STK Engine image
ARG baseImage=ansys/stk/stk-engine-baseline:12.5.0-windowsservercore-ltsc2019
FROM ${baseImage}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
USER ContainerAdministrator

# Enable Connect to listen for external connections
RUN $defaultFile = 'C:\Program Files\AGI\STK 12\STKData\Defaults\_Default.ap'; \
    (Get-Content -Path \"$defaultFile\") -Replace 'AllowExtConnect(\s*)Off', 'AllowExtConnect$1On' | Set-Content -Path \"$defaultFile\" -Force

# Expose the container's Connect socket port
EXPOSE 5001/tcp

USER ContainerUser

# Runs the STK Engine connect console in the foreground.
ENTRYPOINT "'Running ConnectConsole on ' + (Get-NetIpAddress -AddressFamily IPv4 -InterfaceAlias Ethernet).IPAddress; .\ConnectConsole.exe /noGraphics"
