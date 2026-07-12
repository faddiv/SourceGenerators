namespace AttributeParserGenerator.SampleCode.TestAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class FirstInputAttribute(string? value, int intValue, bool boolValue = false) : Attribute
{
    public FirstInputAttribute() : this(null, 0)
    {
    }

    public FirstInputAttribute(string? value) : this(value, 0)
    {
    }

    public FirstInputAttribute(string? value, int? intValue) : this(value, intValue ?? 0)
    {
    }

    public string? Value { get; set; } = value;
    public int IntValue { get; set; } = intValue;
    public bool BoolValue { get; set; } = boolValue;
}
