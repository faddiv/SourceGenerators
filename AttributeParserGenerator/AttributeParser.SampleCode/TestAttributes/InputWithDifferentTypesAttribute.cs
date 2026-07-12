namespace AttributeParserGenerator.SampleCode.TestAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class InputWithDifferentTypesAttribute(
    string? stringValue,
    int intValue,
    bool boolValue,
    EnumValue enumValue,
    double doubleValue,
    Type typeValue,
    string[] stringArray,
    int[] intArray,
    bool[] boolArray,
    EnumValue[] enumArray,
    double[] doubleArray,
    Type[] typeArray
) : Attribute
{
    public string? StringValue { get; } = stringValue;
    public int IntValue { get; } = intValue;
    public bool BoolValue { get; } = boolValue;
    public EnumValue EnumValue { get; } = enumValue;
    public double DoubleValue { get; } = doubleValue;
    public Type TypeValue { get; } = typeValue;
    public string[] StringArray { get; } = stringArray;
    public int[] IntArray { get; } = intArray;
    public bool[] BoolArray { get; } = boolArray;
    public EnumValue[] EnumArray { get; } = enumArray;
    public double[] DoubleArray { get; } = doubleArray;
    public Type[] TypeArray { get; } = typeArray;
}
