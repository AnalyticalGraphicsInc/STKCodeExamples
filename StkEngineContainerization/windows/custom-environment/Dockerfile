ARG baseImage=ansys/stk/stk-engine-baseline:12.9.0-windowsservercore-ltsc2019
FROM ${baseImage}

USER ContainerAdministrator

# CA Certificates Configuration
# Adds additional trusted CAs to the system certificate truststore.
# Delete the following two lines if you do not need any additional trusted CAs.
COPY trusted-certificates/* c:/Temp/certificates/
RUN Get-ChildItem -Path c:/Temp/certificates | Foreach {Import-Certificate -FilePath $_.fullname -CertStoreLocation cert:/LocalMachine/Root}

# PIP Configuration
RUN Get-Content c:/Temp/certificates/*.crt | Set-Content c:/Temp/certificates/ca-bundle.crt
ENV PIP_CERT=c:/Temp/certificates/ca-bundle.crt
# Directs python pip installs to pull packages from a specified repository.
# Delete the following two lines if you do not need to override the default pip repository URL.
ARG pipIndexUrl=''
ENV PIP_INDEX_URL=${pipIndexUrl}

USER ContainerUser
