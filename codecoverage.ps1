# Clean UnitTests and IntegrationTests TestResults folder before running tests. Might have old data, and we want to remove that
Write-Host "Cleaning UnitTests TestResults..."
Remove-Item -Recurse -Force UnitTests\TestResults\* 2>$null
Write-Host "Cleaning IntegrationTests TestResults..."
Remove-Item -Recurse -Force IntegrationsTests\TestResults\* 2>$null

# Run tests for UnitTests project
Write-Host "Running UnitTests..."
$resultUnitTests = dotnet test UnitTests/UnitTests.csproj --collect:"XPlat Code Coverage"

# Run tests for IntegrationTests project
Write-Host "Running IntegrationTests..."
$resultIntegrationTests = dotnet test IntegrationsTests/IntegrationsTests.csproj --collect:"XPlat Code Coverage"

# Generate the coverage report for both projects
Write-Host "Generating coverage report..."
reportgenerator -reports:"UnitTests\TestResults\*\coverage.cobertura.xml;IntegrationsTests\TestResults\*\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html

# Open the generated coverage report
Write-Host "Opening coverage report..."
Start-Process "coveragereport\index.html"

Write-Host "Script execution completed."
