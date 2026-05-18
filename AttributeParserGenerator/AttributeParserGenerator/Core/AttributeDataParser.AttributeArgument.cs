namespace AttributeParserGenerator.Core;

public partial class AttributeDataParser
{
    public readonly struct AttributeArgument(
        string name,
        object? value)
    {
        private readonly string _name = name;
        private readonly object? _value = value;
        public string GetName() => _name;

        public object? GetValue() => _value;
    }
}
