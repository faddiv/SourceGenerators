using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Core;

public partial class AttributeDataParser
{
    public struct AttributeDataEnumerator(AttributeData attributeData, AttributeDataParser parser)
        : IEnumerator<AttributeArgument>
    {
        private readonly AttributeData _attributeData = attributeData;
        private readonly AttributeDataParser _parser = parser;

        private AttributeArgumentType _currentType = InitialAttributeArgumentType(attributeData);
        private int _index = -1;

        private string _currentName = "";
        private object? _currentValue = null;

        public AttributeArgument Current => new(_currentName, _currentValue);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_currentType == AttributeArgumentType.ConstructorArgument)
            {
                _index++;
                var constructorArguments = _attributeData.ConstructorArguments.Length;
                if (_index < constructorArguments)
                {
                    _currentName = _attributeData.AttributeConstructor?.Parameters[_index].Name ?? "";
                    var argument = _attributeData.ConstructorArguments[_index];
                    _currentValue = argument.Value;
                    return true;
                }

                _index = -1;
                _currentType = AttributeArgumentType.NamedArgument;
            }

            if (_currentType == AttributeArgumentType.NamedArgument)
            {
                _index++;
                var namedArguments = _attributeData.NamedArguments.Length;
                if (_index < namedArguments)
                {
                    var namedArgument = _attributeData.NamedArguments[_index];
                    _currentName = namedArgument.Key;
                    _currentValue = namedArgument.Value.Value;
                    return true;
                }

                _index = -1;
                _currentType = AttributeArgumentType.Finished;
            }

            return _currentType != AttributeArgumentType.Finished;
        }

        public void Reset()
        {
            _currentType = InitialAttributeArgumentType(_attributeData);
            _index = -1;
        }

        private static AttributeArgumentType InitialAttributeArgumentType(AttributeData attributeData)
        {
            if (attributeData.ConstructorArguments.Length > 0)
            {
                return AttributeArgumentType.ConstructorArgument;
            }

            if (attributeData.NamedArguments.Length > 0)
            {
                return AttributeArgumentType.NamedArgument;
            }

            return AttributeArgumentType.Finished;
        }
    }

    private enum AttributeArgumentType
    {
        Finished,
        ConstructorArgument,
        NamedArgument
    }
}
