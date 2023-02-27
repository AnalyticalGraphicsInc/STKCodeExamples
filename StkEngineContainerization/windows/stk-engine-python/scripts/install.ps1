Write-Host "Installing Python"

$installer = Get-ChildItem -Path c:/dist -Recurse '*.exe' | Select-Object -ExpandProperty FullName
Start-Process $installer -Wait -NoNewWindow -ArgumentList @('/Quiet', 'InstallAllUsers=1', 'PrependPath=1')

Remove-Item -Path C:\dist\ -Force -Recurse
