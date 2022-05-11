ARG baseImage=ansys/stk/stk-engine-python:12.4.0-centos7
FROM ${baseImage}

# Copy Parallel Computing Server Python API 
COPY distributions/*.whl "${STK_USER_HOME}"/

# Install STK Parallel Computing Server Python API
RUN pip install --no-cache-dir --user "${STK_USER_HOME}"/agiparallel*.whl
