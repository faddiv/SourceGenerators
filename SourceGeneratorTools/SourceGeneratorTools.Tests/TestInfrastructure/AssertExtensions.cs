using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TUnit.Assertions.Conditions;
using TUnit.Assertions.Sources;

namespace SourceGeneratorTools.Tests.TestInfrastructure;

public static class AssertExtensions
{
    extension(ValueAssertion<SourceBuilder> source)
    {
        public StringEqualsAssertion HasContent(
            string expected,
            [CallerArgumentExpression(nameof(expected))]
            string? expression = null)
        {
            if (!string.IsNullOrEmpty(expected) && !expected.EndsWith(Environment.NewLine))
            {
                expected += Environment.NewLine;
            }

            source.Context.ExpressionBuilder.Append($".HasContent({expression ?? nameof(expected)})");
            return new StringEqualsAssertion(
                source.Context.Map(static sb => sb?.ToString()),
                expected,
                StringComparison.Ordinal);
        }

        public StringEqualsAssertion HasRawContent(string expected)
        {
            source.Context.ExpressionBuilder.Append($".HasRawContent({nameof(expected)})");
            return new StringEqualsAssertion(
                source.Context.Map(static sb => sb?.ToString()),
                expected,
                StringComparison.Ordinal);
        }
    }
}
