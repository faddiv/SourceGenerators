namespace AttributeParserGenerator.Sample;

[AttributeUsage(AttributeTargets.Class)]
public class StringInputAttribute(string? value, int intValue, bool boolValue = false) : Attribute
{
    public StringInputAttribute() : this(null, 0)
    {
    }

    public StringInputAttribute(string? value) : this(value, 0)
    {
    }

    public StringInputAttribute(string? value, int? intValue) : this(value, intValue ?? 0)
    {
    }

    public string? Value { get; set; } = value;
    public int IntValue { get; set; } = intValue;
    public bool BoolValue { get; set; } = boolValue;
}
