using System.Threading.Tasks;
using Foxy.PocoDictionary.SourceGenerator;
using Foxy.PocoDictionary.SourceGenerator.Data;
using Xunit;
using SourceGeneratorTests.TestInfrastructure;
using SourceGeneratorTests.TestInfrastructure.Verifiers;

namespace SourceGeneratorTests.IntegrationTests;

using VerifyCS = CSharpSourceGeneratorVerifier<PocoDictionaryIncrementalGenerator>;

public class ErrorReportingTests(TestEnvironment testEnvironment)
{
    [Fact]
    public async Task Reports_On_ClassIsNotPartial()
    {
        var code = testEnvironment.GetInvalidSource();

        var expected = VerifyCS
            .Diagnostic(DiagnosticReports.ClassMustBePartial)
            .WithLocation(0)
            .WithArguments("Foo");

        await VerifyCS.VerifyGeneratorAsync(code, expected, testEnvironment.DefaultOutput);
    }
}
