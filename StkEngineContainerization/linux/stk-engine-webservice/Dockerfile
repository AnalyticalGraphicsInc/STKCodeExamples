# By default, start from the STK Engine Python API image
ARG baseImage=ansys/stk/stk-engine-python:12.8.0-ubi8
FROM ${baseImage}

# Install Flask
RUN pip3 install --user --no-cache-dir flask

# Copy example web service application
COPY --chown=stk access-webservice.py "${STK_USER_HOME}"/access-webservice.py

# Configure environment
ENV FLASK_APP=access-webservice.py \
    LANG=en_US.utf8 \
    LC_ALL=en_US.utf8

# Expose the default flask development server port
EXPOSE 5000/tcp

# Run flask application
ENTRYPOINT flask run --host=0.0.0.0
