using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.TestInfrastructure;

public class InputWithDifferentTypes
{
    public string? StringValue { get; set; }

    public int IntValue { get; set; }

    public bool BoolValue { get; set; }

    public double DoubleValue { get; set; }

    public EnumValue EnumValue { get; set; }

    public INamedTypeSymbol? TypeValue { get; set; }

    public ImmutableArray<string> StringArray { get; set; }

    public ImmutableArray<int> IntArray { get; set; }

    public ImmutableArray<bool> BoolArray { get; set; }

    public ImmutableArray<EnumValue> EnumArray { get; set; }

    public ImmutableArray<double> DoubleArray { get; set; }

    public ImmutableArray<INamedTypeSymbol> TypeArray { get; set; }
}
