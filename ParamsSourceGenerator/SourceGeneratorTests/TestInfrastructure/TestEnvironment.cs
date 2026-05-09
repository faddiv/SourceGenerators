using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Test.Infrastructure;
using TUnit.Core.Interfaces;

namespace SourceGeneratorTests.TestInfrastructure;

public class TestEnvironment : IAsyncInitializer
{
    private readonly EnvironmentProvider _environment = new();

    private readonly string _validTests = "IntegrationTests\\SourceGenerationTestCases";
    private readonly string _invalidTests = "IntegrationTests\\ErrorReportingTestCases";
    private readonly string _cachingTests = "IntegrationTests\\CachingTestCases";

    public readonly CSharpFile DefaultOutput;

    public CompilerRunner Compiler { get; }

    public TestEnvironment()
    {
        DefaultOutput = _environment.GetFile(_validTests, "ParamsAttribute.g.cs");
        Compiler = new CompilerRunner();
    }


    public async Task InitializeAsync()
    {
        await Compiler.LoadCSharpAssemblies(TestContext.Current?.Execution.CancellationToken ?? CancellationToken.None);
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public CSharpFile GetValidSource([CallerMemberName] string caller = null!)
    {
        return _environment.GetFile(_validTests, caller, "_source.cs");
    }

    public CSharpFile GetInvalidSource([CallerMemberName] string caller = null!)
    {
        return _environment.GetFile(_invalidTests, caller, "_source.cs");
    }

    public CSharpFile GetCachingSource([CallerMemberName] string caller = null!)
    {
        return _environment.GetFile(_cachingTests, caller, "_source.cs");
    }

    public CSharpFile[] GetCachingSources([CallerMemberName] string caller = null!)
    {
        return _environment.GetFiles(_cachingTests, caller, "_source*");
    }

    public CSharpFile[] GetOutputs([CallerMemberName] string caller = null!)
    {
        return GetOutputsFor(_environment.GetBasePath(_validTests), caller);
    }

    public CSharpFile[] GetCachingOutputs([CallerMemberName] string caller = null!)
    {
        return GetOutputsFor(_environment.GetBasePath(_cachingTests), caller);
    }

    public CSharpFile[] GetOutputsFor(string baseDirectory, [CallerMemberName] string caller = null!)
    {
        var basePath = Path.Combine(baseDirectory, caller);
        var sources = new List<CSharpFile>
        {
            DefaultOutput
        };

        foreach (var filePath in Directory.GetFiles(basePath, "*.cs", SearchOption.TopDirectoryOnly))
        {
            var fileName = Path.GetFileName(filePath);
            if (fileName.StartsWith("_source"))
                continue;

            var content = File.ReadAllText(filePath);
            sources.Add(new CSharpFile(fileName, content));
        }

        return [.. sources];
    }
}
