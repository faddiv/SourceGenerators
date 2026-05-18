namespace AttributeParserGenerator.SampleCode.TestAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class AttributeWithLotsOfArguments(
    string? value1,
    string? value2,
    string? value3,
    string? value4,
    string? value5,
    string? value6,
    string? value7,
    string? value8,
    string? value9,
    string? value10
) : Attribute
{
    public string? Value1 { get; set; } = value1;
    public string? Value2 { get; set; } = value2;
    public string? Value3 { get; set; } = value3;
    public string? Value4 { get; set; } = value4;
    public string? Value5 { get; set; } = value5;
    public string? Value6 { get; set; } = value6;
    public string? Value7 { get; set; } = value7;
    public string? Value8 { get; set; } = value8;
    public string? Value9 { get; set; } = value9;
    public string? Value10 { get; set; } = value10;

    public int IntValue1 { get; set; }
    public int IntValue2 { get; set; }
    public int IntValue3 { get; set; }
    public int IntValue4 { get; set; }
    public int IntValue5 { get; set; }
    public int IntValue6 { get; set; }
    public int IntValue7 { get; set; }
    public int IntValue8 { get; set; }
    public int IntValue9 { get; set; }
    public int IntValue10 { get; set; }
}
