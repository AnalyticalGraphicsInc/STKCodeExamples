ARG baseImage=ansys/stk/stk-engine-baseline:12.6.0-ubi8
FROM ${baseImage}

USER root

# CA Certificates Configuration
# Adds additional trusted CAs to the system certificate truststore.
# Delete the following two lines if you do not need any additional trusted CAs.
COPY trusted-certificates/* /etc/pki/ca-trust/source/anchors/
RUN update-ca-trust extract

# YUM Configuration
# Overrides the default configuration for yum repositories built into the base image.
# Delete the following two lines to use the default configuration for yum repositories.
RUN rm /etc/yum.repos.d/*.repo
COPY yum-repositories/* /etc/yum.repos.d/

# PIP Configuration
ENV PIP_CERT=/etc/pki/ca-trust/extracted/openssl/ca-bundle.trust.crt
# Directs python pip installs to pull packages from a specified repository.
# Delete the following two lines if you do not need to override the default pip repository URL.
ARG pipIndexUrl=''
ENV PIP_INDEX_URL=${pipIndexUrl}
