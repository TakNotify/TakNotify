$versionPrefix = "1.0.0"

$projectRoot = Join-Path $PSScriptRoot "\src\TakNotify"
$csproj = Join-Path $projectRoot "\TakNotify.csproj"
$outputDir = Join-Path $projectRoot "\dist"
$tempFile = Join-Path $outputDir "\temp.txt"

Write-Host "Preparing resources" -ForegroundColor Yellow

if (-Not (Test-Path $outputDir -PathType Container)) {
    New-Item -ItemType Directory -Path $outputDir

    Write-Host
    Write-Host "Output directory has been created"
}

if (-Not (Test-Path $tempFile)) {
    "0" | Out-File -FilePath $tempFile

    Write-Host
    Write-Host "Temp file has been initialized"
}

$tempContent = Get-Content $tempFile
$buildNumber = [convert]::ToInt32($tempContent[0], 10)
$version = "$versionPrefix.$buildNumber"
$buildOutput = Join-Path $outputDir "\$version"

Write-Host
Write-Host "All good to go. Proceeding to build v$version" -ForegroundColor Green

Write-Host
Write-Host "Restoring packages" -ForegroundColor Yellow

dotnet restore $csproj

Write-Host
Write-Host "Building the code" -ForegroundColor Yellow

dotnet build $csproj --configuration Release --no-restore -p:Version=$version

Write-Host
Write-Host "Publishing an artifact" -ForegroundColor Yellow

dotnet publish $csproj --configuration Release --output $buildOutput --no-build

Write-Host
Write-Host "Finishing build" -ForegroundColor Yellow

$buildNumber + 1 | Out-File -FilePath $tempFile -Force

Write-Host
Write-Host "Temp file has been updated"

Write-Host
Write-Host "Finish! Please check the build output at $buildOutput" -ForegroundColor Green
Write-Host