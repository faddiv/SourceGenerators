using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Core;

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

        public string GetName()
        {
            if (_index < _attributeData.ConstructorArguments.Length)
            {
                return _attributeData.AttributeConstructor?.Parameters[_index].Name ?? "";
            }

            return _parser.ToCamelCase(_attributeData.NamedArguments[_index - _attributeData.ConstructorArguments.Length]
                .Key);
        }

        public object? GetValue()
        {
            var constructorArguments = _attributeData.ConstructorArguments.Length;
            if (_index < constructorArguments)
            {
                var argument = _attributeData.ConstructorArguments[_index];
                return argument.Value;
            }

            var namedArgument =
                _attributeData.NamedArguments[_index - constructorArguments];
            return namedArgument.Value.Value;
        }
    }
}
