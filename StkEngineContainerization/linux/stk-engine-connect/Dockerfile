# By default, start from the base STK Engine image
ARG baseImage=ansys/stk/stk-engine-baseline:12.6.0-centos7
FROM ${baseImage}

# Enable Connect to listen for external connections
RUN sed -i -E 's/AllowExtConnect(\s*)Off/AllowExtConnect\1On/' "${STK_USER_HOME}/STKData/Defaults/_Default.ap"

# Expose the container's Connect socket port
EXPOSE 5001/tcp

# Runs the STK Engine connect console in the foreground.
ENTRYPOINT [ "connectconsole", "--nographics" ]
