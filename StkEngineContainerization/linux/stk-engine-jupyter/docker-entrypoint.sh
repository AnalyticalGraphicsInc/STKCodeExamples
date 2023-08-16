#!/bin/bash
set -e

if [[ -z "${JUPYTER_LAB_TOKEN}" ]]; then
    echo 'Error: JUPYTER_LAB_TOKEN is a required environment variable.'
    exit 1
fi

jupyter lab \
    --ip=0.0.0.0 \
    --no-browser \
    --notebook-dir="${STK_USER_HOME}/notebooks" \
    --NotebookApp.token="${JUPYTER_LAB_TOKEN}" \
    --port=8888
