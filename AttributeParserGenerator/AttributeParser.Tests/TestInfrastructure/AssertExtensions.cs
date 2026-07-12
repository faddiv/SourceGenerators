using Microsoft.CodeAnalysis.CSharp;
using TUnit.Assertions.Conditions;
using TUnit.Assertions.Sources;

namespace AttributeParserGenerator.Tests.TestInfrastructure;

public static class AssertExtensions
{
    extension(ValueAssertion<CSharpCompilation> source)
    {
        public SatisfiesAssertion<CSharpCompilation> HasNoDiagnostics()
        {
            source.Context.ExpressionBuilder.Append($".HasNoDiagnostics()");
            return source.Satisfies(static compilation =>
            {
                var diagnostics = compilation?.GetDiagnostics() ?? [];

                return diagnostics.Length == 0;
            });
        }
    }
}
