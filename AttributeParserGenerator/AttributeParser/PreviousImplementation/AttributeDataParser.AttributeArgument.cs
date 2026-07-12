using Microsoft.CodeAnalysis;

namespace AttributeParser.PreviousImplementation;

public partial class AttributeDataParser
{
    public readonly struct AttributeArgument(
        int index,
        AttributeData attributeData,
        AttributeDataParser parser)
    {
        private readonly AttributeDataParser _parser = parser;
        private readonly AttributeData _attributeData = attributeData;
        private readonly int _index = index;

        public string GetName() => _parser.GetName(_index, _attributeData);

        public object? GetValue() => _parser.GetValue(_index, _attributeData);
    }
}
