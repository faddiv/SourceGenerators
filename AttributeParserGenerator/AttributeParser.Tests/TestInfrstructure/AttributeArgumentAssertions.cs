using System.Runtime.CompilerServices;
using AttributeParser.Core;
using TUnit.Assertions.Core;

namespace AttributeParser.Tests.TestInfrstructure;

public static class AttributeArgumentAssertions
{
    public static HasNameAssertion HasName(
        this IAssertionSource<AttributeDataParser.AttributeArgument> source,
        string expected,
        [CallerArgumentExpression(nameof(expected))]
        string? expression = null)
    {
        source.Context.ExpressionBuilder.Append($".HasName({expression})");
        return new HasNameAssertion(source.Context, expected);
    }
}
