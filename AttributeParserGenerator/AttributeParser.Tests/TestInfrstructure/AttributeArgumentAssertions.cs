using System.ComponentModel;
using AttributeParser.Core;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Attributes;

namespace AttributeParser.Tests;

public static class AttributeArgumentAssertions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [GenerateAssertion(ExpectationMessage = "to be name {name} and value {value}")]
    public static bool IsNameAndValue<T>(this AttributeDataParser.AttributeArgument argument, string name, T value)
    {
        return argument.GetName() == name &&
               EqualityComparer<T>.Default.Equals(argument.GetValue<T>(), value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [GenerateAssertion(ExpectationMessage = "to be name {name} and symbolName {symbolName}")]
    public static bool IsNameAndSymbolName(
        this AttributeDataParser.AttributeArgument argument,
        string name,
        string symbolName)
    {
        return argument.GetName() == name &&
               argument.GetValue<INamedTypeSymbol>()?.Name == symbolName;
    }
}
