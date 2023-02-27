# By default, start from the STK Engine Python API image
ARG baseImage=ansys/stk/stk-engine-python:12.6.0-windowsservercore-ltsc2019
FROM ${baseImage}

# Install Flask
RUN pip install --user --no-cache-dir flask

# Copy example web service application
COPY access-webservice.py C:/app/access-webservice.py

# Configure environment
ENV FLASK_APP=C:/app/access-webservice.py \
    LANG=en_US.utf8 \
    LC_ALL=en_US.utf8

# Expose the default flask development server port
EXPOSE 5000/tcp

RUN Set-TimeZone -id "UTC"

# Run flask application
ENTRYPOINT flask run --host=0.0.0.0
