namespace AttributeParserGenerator.SampleCode.TestAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class InputWithOptionalAttribute(string? value = "Default Value", int intValue = 42) : Attribute
{
    public string? Value { get; set; } = value;
    public int IntValue { get; set; } = intValue;
}
