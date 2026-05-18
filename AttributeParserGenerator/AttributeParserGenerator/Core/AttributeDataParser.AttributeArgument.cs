using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Core;

public partial class AttributeDataParser
{
    public readonly struct AttributeArgument(
        string name,
        int index,
        AttributeData attributeData,
        AttributeDataParser parser)
    {
        private readonly AttributeDataParser _parser = parser;
        private readonly AttributeData _attributeData = attributeData;
        private readonly int _index = index;
        public string Name { get; } = name;

        public object? GetValue()
        {
            return _parser.GetValue(_index, _attributeData);
        }
    }
}
