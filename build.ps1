param(
    [Parameter()]
    [string]$VersionPrefix = "1.1.0",

    [Parameter()]
    [int]$BuildNumber = 0,

    [Parameter()]
    [string]$OutputDir
)

if ($OutputDir -eq [string]::Empty) {
    $OutputDir = Join-Path $PSScriptRoot "\dist"
}

$needTempFile = $BuildNumber -eq 0
$csproj = Join-Path $PSScriptRoot "\src\TakNotify\TakNotify.csproj"
$tempFile = Join-Path $OutputDir "\temp.txt"

Write-Host "1. Preparing resources" -ForegroundColor Yellow

if ($needTempFile) {
    if (-Not (Test-Path $OutputDir -PathType Container)) {
        New-Item -ItemType Directory -Path $OutputDir

        Write-Host
        Write-Host "Output directory has been created"
    }

    if (-Not (Test-Path $tempFile)) {
        "0" | Out-File -FilePath $tempFile

        Write-Host
        Write-Host "Temp file has been initialized"
    }

    $tempContent = Get-Content $tempFile
    $BuildNumber = [convert]::ToInt32($tempContent[0], 10)
}

$version = "$VersionPrefix.$BuildNumber"
$buildOutput = Join-Path $OutputDir "\$version"

Write-Host
Write-Host "All good to go. Proceeding to build v$version" -ForegroundColor Green

Write-Host
Write-Host "2. Restoring packages" -ForegroundColor Yellow

dotnet restore $csproj --verbosity normal

Write-Host
Write-Host "3. Building the code" -ForegroundColor Yellow

dotnet build $csproj --configuration Release --verbosity normal --no-restore -p:Version=$version

Write-Host
Write-Host "4. Publishing an artifact" -ForegroundColor Yellow

dotnet publish $csproj --configuration Release --output $buildOutput --no-build

Write-Host
Write-Host "5. Packing into NuGet package" -ForegroundColor Yellow

dotnet pack $csproj --configuration Release --output $buildOutput --no-build -p:PackageVersion=$VersionPrefix

Write-Host
Write-Host "6. Finishing build" -ForegroundColor Yellow

if ($needTempFile) {
    $BuildNumber + 1 | Out-File -FilePath $tempFile -Force

    Write-Host
    Write-Host "Temp file has been updated"
}

Write-Host
Write-Host "Finish! Please check the build output at $buildOutput" -ForegroundColor Green
Write-Host