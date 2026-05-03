rd /s /q "SourceGeneratorTools.Tests\bin\Debug\net10.0\TestResults"
dotnet test --coverlet
dotnet reportgenerator -reports:SourceGeneratorTools.Tests\bin\Debug\net10.0\TestResults\coverage.cobertura.*.xml -targetdir:coveragereport