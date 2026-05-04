dotnet test --coverage
dotnet reportgenerator -reports:SourceGeneratorTools.Tests\bin\Debug\net10.0\TestResults\cobertura.xml -targetdir:coveragereport