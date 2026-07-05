using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public AttributeArgument Current
        {
            get
            {
                if (_index < 0)
                {
                    throw new InvalidOperationException("Enumeration has not started. Call MoveNext().");
                }

                switch (_currentType)
                {
                    case AttributeArgumentType.ConstructorArgument:
                        var constName = _attributeData.AttributeConstructor?.Parameters[_index].Name ?? string.Empty;
                        var constValue = _attributeData.ConstructorArguments[_index].Value;
                        return new AttributeArgument(constName, constValue);

                    case AttributeArgumentType.NamedArgument:
                        var namedName = _parser.ToCamelCase(_attributeData.NamedArguments[_index].Key);
                        var namedValue = _attributeData.NamedArguments[_index].Value.Value;
                        return new AttributeArgument(namedName, namedValue);

                    case AttributeArgumentType.Finished:
                        throw new InvalidOperationException("Enumeration already finished.");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index++;
            if (_currentType == AttributeArgumentType.ConstructorArgument)
            {
                if (_index >= _attributeData.ConstructorArguments.Length)
                {
                    _currentType = AttributeArgumentType.NamedArgument;
                    _index = 0;
                }
            }

            if (_currentType != AttributeArgumentType.NamedArgument)
            {
                return true;
            }

            if (_index < _attributeData.NamedArguments.Length)
            {
                return true;
            }

            _currentType = AttributeArgumentType.Finished;
            return false;
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
