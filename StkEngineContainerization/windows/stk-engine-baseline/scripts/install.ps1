Param([Parameter(Mandatory=$false)] [String] $agreeToLicense)

if ($agreeToLicense -ne 'yes') {
    Write-Error "You must agree to accept the STK User License by setting the 'agreeToLicense' build-arg."
    exit 1
}

Write-Host "Installing STK Engine"

# Unpack engine
$zipFile = Get-ChildItem -Path c:/dist -Recurse '*.zip' | Select-Object -ExpandProperty FullName
Expand-Archive -Path $zipFIle -DestinationPath c:/dist
Remove-Item $zipFile;

$setup = Get-ChildItem -Path c:/dist -Recurse 'setup.exe' | Select-Object -ExpandProperty FullName
Start-Process $setup -Wait -ArgumentList @('/S', '/V"/qn /L*v C:\dist\install.log AgreeToLicense=Yes"')

Remove-Item -Path C:\dist\ -Force -Recurse
