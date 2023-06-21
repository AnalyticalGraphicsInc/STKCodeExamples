ARG baseImage=ansys/stk/stk-engine-python:12.6.0-ubi8
FROM ${baseImage}

# Copy Parallel Computing Server Python API
COPY distributions/*.whl "${STK_USER_HOME}"/

# Install STK Parallel Computing Server Python API
RUN pip3 install --no-cache-dir --user "${STK_USER_HOME}"/agiparallel*.whl
