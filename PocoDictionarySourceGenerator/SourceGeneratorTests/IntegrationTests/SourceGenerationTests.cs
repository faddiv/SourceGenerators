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
    [Fact]
    public async Task Always_Generate_PocoDictionaryAttribute()
    {
        var code = new CSharpFile("Empty.cs", "");

        await VerifyCS.VerifyGeneratorAsync(code,
            testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task Generate_PocoInNamespace()
    {
        var code = testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            testEnvironment.GetOutputs());
    }
    
    [Fact]
    public async Task Generate_PocoInGlobalNamespace()
    {
        var code = testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            testEnvironment.GetOutputs());
    }
    
    [Fact]
    public async Task Generate_PocoForRecord()
    {
        var code = testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            testEnvironment.GetOutputs());
    }

    [Fact]
    public async Task Generate_PocoForStruct()
    {
        var code = testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            testEnvironment.GetOutputs());
    }
    
    [Fact]
    public async Task Generate_PocoForRecordStruct()
    {
        var code = testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            testEnvironment.GetOutputs());
    }

    [Fact]
    public async Task Generate_PocoInInnerClass()
    {
        var code = testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            testEnvironment.GetOutputs());
    }
}
