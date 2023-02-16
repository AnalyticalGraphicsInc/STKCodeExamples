ARG baseImage=mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2019
FROM $baseImage
ARG agreeToLicense=no

COPY distributions scripts C:/dist/

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
RUN C:\dist\install.ps1 $Env:agreeToLicense

USER ContainerUser
WORKDIR "C:/Program Files/AGI/STK 12/bin"
RUN [ "C:/Program Files/AGI/STK 12/bin/AgNewUserSetup.exe", "force", "showDlg=Off", "allowOnline" ]

CMD echo 'STK Engine requires a host application to run.  Shutting down this container.'
