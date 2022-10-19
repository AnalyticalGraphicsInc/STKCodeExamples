Param([Parameter(Mandatory=$false)] [String] $agreeToLicense)

if ($agreeToLicense -NE 'yes') {
    Write-Error 'You must agree to accept the STK User License by setting the "agreeToLicense" build-arg.'
    exit 1
}

Set-TimeZone -id 'UTC'
$dotnetInstall = Get-ChildItem -Recurse -Path C:/TEMP/ -Filter 'dotnet*.exe'
Start-Process -FilePath $dotnetInstall.FullName -Wait -ArgumentList @('/quiet', '/norestart')
$coordinatorInstall = Get-ChildItem -Recurse -Path C:/TEMP/ -Filter 'STK Parallel Computing Coordinator*.msi'
Start-Process -FilePath $coordinatorInstall.FullName -Wait -ArgumentList @('/quiet /qn AgreeToLicense=Yes')
$coordinatorInstallDir = Get-ChildItem -Recurse -Path "C:/Program Files/AGI" -Filter 'STK Parallel Computing Server *'
Stop-Service "STK Parallel Computing Coordinator"
Set-Service "STK Parallel Computing Coordinator" -StartupType Disabled
Move-Item $coordinatorInstallDir.FullName "C:\Program Files\AGI\STK Parallel Computing Server" 

Remove-Item -Path C:\Temp\ -Force -Recurse
