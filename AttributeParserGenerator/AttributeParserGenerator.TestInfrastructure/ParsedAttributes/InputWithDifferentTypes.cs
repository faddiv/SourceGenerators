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
}
