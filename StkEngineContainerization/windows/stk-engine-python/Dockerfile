# By default, start from the base STK Engine image
ARG baseImage=ansys/stk/stk-engine-baseline:12.4.0-windowsservercore-ltsc2019
FROM ${baseImage}

USER ContainerAdministrator

# Install Python
COPY distributions scripts C:/dist/
RUN C:\dist\install.ps1

USER ContainerUser

# Update the pip environment variables
ENV PIP_DEFAULT_TIMEOUT=600 \
    PIP_RETRY=50

# Install the STK Engine Python API module
RUN setx path "C:/Users/ContainerUser/AppData/Roaming/Python/Python38/Scripts;$env:path"; \
    pip install --no-cache-dir --upgrade --user pip; \
    pip install --no-cache-dir --user wheel; \
    $stkwhl = Get-ChildItem -Path 'C:/Program Files/AGI/STK 12/bin/AgPythonAPI' -Recurse 'agi.stk*.whl' | Select-Object -ExpandProperty FullName; \
    pip install --no-cache-dir --user $stkwhl

ENTRYPOINT [ "python" ]
