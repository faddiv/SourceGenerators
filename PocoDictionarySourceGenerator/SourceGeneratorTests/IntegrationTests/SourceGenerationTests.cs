using System.Threading.Tasks;
using Foxy.PocoDictionary.SourceGenerator;
using Xunit;
using SourceGeneratorTests.TestInfrastructure;
using SourceGeneratorTests.TestInfrastructure.Verifiers;
using Test.Infrastructure;

namespace SourceGeneratorTests.IntegrationTests;

using VerifyCS = CSharpSourceGeneratorVerifier<PocoDictionaryIncrementalGenerator>;

public class SourceGenerationTests(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    [Fact]
    public async Task Always_Generate_PocoDictionaryAttribute()
    {
        var code = new CSharpFile("Empty.cs", "");

        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task Generate_PocoInNamespace()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Fact]
    public async Task Generate_PocoInGlobalNamespace()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Fact]
    public async Task Generate_PocoForRecord()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Fact]
    public async Task Generate_PocoForStruct()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Fact]
    public async Task Generate_PocoForRecordStruct()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Fact]
    public async Task Generate_PocoInInnerClass()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }
}
