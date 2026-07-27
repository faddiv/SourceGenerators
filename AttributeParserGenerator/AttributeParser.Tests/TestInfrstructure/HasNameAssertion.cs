using System.Runtime.CompilerServices;
using AttributeParser.Core;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Conditions;
using TUnit.Assertions.Core;

namespace AttributeParser.Tests.TestInfrstructure;

public class HasNameAssertion(
    AssertionContext<AttributeDataParser.AttributeArgument> context,
    string name)
    : Assertion<AttributeDataParser.AttributeArgument>(context)
{
    private readonly string _name = name;

    protected override Task<AssertionResult> CheckAsync(
        EvaluationMetadata<AttributeDataParser.AttributeArgument> metadata)
    {
        var value = metadata.Value.GetName();
        var exception = metadata.Exception;

        if (exception != null)
            return Task.FromResult(
                AssertionResult.Failed($"expected .GetName() to return with '{_name}' but exception threw", exception));

        return value == _name
            ? Task.FromResult(AssertionResult.Passed)
            : Task.FromResult(
                AssertionResult.Failed($"expected .GetName() to return with '{_name}' but returned '{value}'"));
    }

    public EqualsAssertion<T> WithValue<T>(
        T expectedValue,
        [CallerArgumentExpression(nameof(expectedValue))]
        string? expression = null)
    {
        Context.ExpressionBuilder.Append($".WithValue({expectedValue})");
        return new EqualsAssertion<T>(Context.Map(arg => arg.GetValue<T>()), expectedValue);
    }

    public StringEqualsAssertion WithSymbol(
        string expectedValue,
        [CallerArgumentExpression(nameof(expectedValue))]
        string? expression = null)
    {
        Context.ExpressionBuilder.Append($".WithSymbol({expression})");
        return new StringEqualsAssertion(Context.Map(arg => arg.GetValue<INamedTypeSymbol>()?.Name), expectedValue);
    }

    public StructuralEquivalencyAssertion<IEnumerable<T>> WithValues<T>(
        T[] expectedValues,
        [CallerArgumentExpression(nameof(expectedValues))]
        string? expression = null)
    {
        Context.ExpressionBuilder.Append($".WithValue({expression})");
        return new StructuralEquivalencyAssertion<IEnumerable<T>>(
            Context.Map(arg => (IEnumerable<T>)arg.GetValues<T>()),
            expectedValues);
    }

    public StructuralEquivalencyAssertion<IEnumerable<string>> WithSymbols(
        string[] expectedValues,
        [CallerArgumentExpression(nameof(expectedValues))]
        string? expression = null)
    {
        Context.ExpressionBuilder.Append($".WithSymbol({expression})");
        return new StructuralEquivalencyAssertion<IEnumerable<string>>(
            Context.Map(arg => arg.GetValues<INamedTypeSymbol>().Select(s => s.Name)),
            expectedValues);
    }

    protected override string GetExpectation()
    {
        return $"to be name {_name}";
    }
}
