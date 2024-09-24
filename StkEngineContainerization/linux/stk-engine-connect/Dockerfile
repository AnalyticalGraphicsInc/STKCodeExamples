# By default, start from the base STK Engine image
# Uncomment the first line below if you built the stk-engine-custom-baseline image
# ARG baseImage=ansys/stk/stk-engine-custom-baseline:12.9.0-ubi8
ARG baseImage=ansys/stk/stk-engine-baseline:12.9.0-ubi8
FROM ${baseImage}

# Enable Connect to listen for external connections
RUN sed -i -E 's/AllowExtConnect(\s*)Off/AllowExtConnect\1On/' "${STK_USER_HOME}/STKData/Defaults/_Default.ap"

# Expose the container's Connect socket port
EXPOSE 5001/tcp

# Runs the STK Engine connect console in the foreground.
ENTRYPOINT [ "connectconsole", "--nographics" ]
