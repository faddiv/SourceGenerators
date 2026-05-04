using System.Threading.Tasks;
using Foxy.Params.SourceGenerator;
using Xunit;
using SourceGeneratorTests.TestInfrastructure;
using Foxy.Params.SourceGenerator.Data;
using Microsoft.CodeAnalysis;
using SourceGeneratorTests.TestInfrastructure.Verifiers;

namespace SourceGeneratorTests.IntegrationTests;

using VerifyCS = CSharpSourceGeneratorVerifier<ParamsIncrementalGenerator>;

public class ErrorReportingTests(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    [Fact]
    public async Task Reports_NoPartialKeyword()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic(DiagnosticReports.PartialIsMissingDescriptor)
            .WithLocation(0)
            .WithArguments("Foo", "Format");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task Reports_NoPartialKeywordOnParentClass()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic(DiagnosticReports.PartialIsMissingDescriptor)
            .WithLocation(0)
            .WithArguments("ParentClass", "Format");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task Reports_NoParameter()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic(DiagnosticReports.ParameterMissingDescriptor)
            .WithLocation(0)
            .WithArguments("Format");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task Reports_NonReadOnlySpanParameter()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic(DiagnosticReports.ParameterMismatchDescriptor)
            .WithLocation(0)
            .WithArguments("Format", "object args");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task DoesntGenerateOnFaultyMethodParameter()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic("CS0246", DiagnosticSeverity.Error)
            .WithLocation(0)
            .WithArguments("ThisIsWrong");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task DoesntGenerateOnFaultyReturnType()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic("CS0246", DiagnosticSeverity.Error)
            .WithLocation(0)
            .WithArguments("ThisIsWrong");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task Reports_OutReadOnlySpanParameter()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic(DiagnosticReports.OutModifierNotAllowedDescriptor)
            .WithLocation(0)
            .WithArguments("Format", "out System.ReadOnlySpan<object> args");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task DoesntGenerateOnNameDuplication()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic("CS0100", DiagnosticSeverity.Error)
            .WithLocation(0)
            .WithArguments("values");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }

    [Fact]
    public async Task Reports_OnNameCollision()
    {
        var code = _testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic(DiagnosticReports.ParameterCollisionDescriptor)
            .WithLocation(0)
            .WithArguments("Format", "valuesSpan, values0, values1, values2");

        await VerifyCS.VerifyGeneratorAsync(code, expected, _testEnvironment.DefaultOutput);
    }
}
