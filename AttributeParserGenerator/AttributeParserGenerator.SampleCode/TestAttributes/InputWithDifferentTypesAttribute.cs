namespace AttributeParserGenerator.SampleCode.TestAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class InputWithDifferentTypesAttribute(
    string? stringValue,
    int intValue,
    bool boolValue,
    EnumValue enumValue,
    double doubleValue,
    Type typeValue
) : Attribute
{
    public string? StringValue { get; } = stringValue;
    public int IntValue { get; } = intValue;
    public bool BoolValue { get; } = boolValue;
    public EnumValue EnumValue { get; } = enumValue;
    public double DoubleValue { get; } = doubleValue;
    public Type TypeValue { get; } = typeValue;
}
