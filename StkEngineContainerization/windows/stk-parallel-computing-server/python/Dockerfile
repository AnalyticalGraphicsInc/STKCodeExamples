ARG baseImage=ansys/stk/stk-engine-python:12.5.0-windowsservercore-ltsc2019
FROM ${baseImage}

# Copy Parallel Computing Server Python API
COPY distributions/*.whl C:/TEMP/

# Install STK Parallel Computing Server Python API
RUN $wheel = Get-ChildItem -Path C:/TEMP -Filter *.whl;pip install --no-cache-dir --user $wheel.FullName
