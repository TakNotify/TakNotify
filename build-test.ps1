$testCsproj = Join-Path $PSScriptRoot "\test\TakNotify.Test\TakNotify.Test.csproj"

Write-Host
Write-Host "1. Restoring packages" -ForegroundColor Yellow

dotnet restore $testCsproj

Write-Host
Write-Host "2. Building the code" -ForegroundColor Yellow

dotnet build $testCsproj --configuration Release --no-restore

Write-Host
Write-Host "3. Running test" -ForegroundColor Yellow

dotnet test $testCsproj --no-build

Write-Host
Write-Host "Finish!" -ForegroundColor Green
Write-Host