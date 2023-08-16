if (Test-Path "$env:JUPYTER_LAB_TOKEN") {
    Write-Error 'Error: JUPYTER_LAB_TOKEN is a required environment variable.'
    exit 1
}

jupyter lab `
    --ip=0.0.0.0 `
    --no-browser `
    --notebook-dir="C:/notebooks" `
    --NotebookApp.token="${env:JUPYTER_LAB_TOKEN}" `
    --port=8888
