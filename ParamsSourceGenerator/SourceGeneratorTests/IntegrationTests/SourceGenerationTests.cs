using System.Threading.Tasks;
using Foxy.Params.SourceGenerator;
using SourceGeneratorTests.TestInfrastructure;
using SourceGeneratorTests.TestInfrastructure.Verifiers;
using Test.Infrastructure;

namespace SourceGeneratorTests.IntegrationTests;

using VerifyCS = CSharpSourceGeneratorVerifier<ParamsIncrementalGenerator>;

[ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
public class SourceGenerationTests(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    [Test]
    public async Task Always_Generate_ParamsAttribute()
    {
        var code = new CSharpFile("Empty.cs", "");

        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.DefaultOutput);
    }

    [Test]
    public async Task Generate_OverridesFor_ReadOnlySpan_WithDefaultParameters()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }


    [Test]
    public async Task Generate_OverridesFor_CountedCase_WithMaxOverrides()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_OverridesFor_MultipleFixedParameters()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task DoesNotGenerateParams_WhenHasParamsIsFalse()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForInstanceLevelMethod()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForNonObjectReadOnlySpan()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForFunctions_WithKeywordReturnType()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForFunctions_WithNonKeywordReturnType()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForGenericMethods_WithMultipleGenericFixedParameters()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForGenericMethods_WithGenericParamsParameter()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForGenericFunctions_WithGenericReturnType()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForGenericMethods_WithRestrictedGenericParameters()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForGenericMethods_WithRestrictedGenericParamsParameters()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForGenericMethods_WithRestrictedGenericReturnType()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForCustomTypeParametersAndReturnType()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForRefReadOnlySpan()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForRefReadonlyReadOnlySpan()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForInReadOnlySpan()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForSpecialFixedParams()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForRefReturn()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForRefReadonlyReturn()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForNullableParametersAndReturnType()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForDifferentReadOnlySpanName()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_WhenParametersDontCollide()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_Overrides_WhenClassIsEmbedded()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_Overrides_WhenNamespaceIsEmbedded()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_Overrides_WhenInGlobalNamespace()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task OnGenericType_Generates_Overrides()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_ForEmbeddedGenericArguments()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }

    [Test]
    public async Task Generate_OverridesFor_AliasedParam()
    {
        var code = _testEnvironment.GetValidSource();
        await VerifyCS.VerifyGeneratorAsync(code,
            _testEnvironment.GetOutputs());
    }
}
