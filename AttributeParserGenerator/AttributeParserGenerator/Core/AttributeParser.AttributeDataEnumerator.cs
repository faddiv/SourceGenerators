using System;
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
        private int _index = -1;
        private string _currentName = "";

        private int MaxIndex => _attributeData.ConstructorArguments.Length + _attributeData.NamedArguments.Length;

        public AttributeArgument Current => _index < MaxIndex
            ? new AttributeArgument(_currentName, _index, _attributeData, _parser)
            : throw new InvalidOperationException();

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index++;
            if (_index < _attributeData.ConstructorArguments.Length)
            {
                _currentName = _attributeData.AttributeConstructor?.Parameters[_index].Name ?? "";
                return true;
            }

            if (_index < MaxIndex)
            {
                _currentName = _attributeData.NamedArguments[_index - _attributeData.ConstructorArguments.Length]
                    .Key;
                return true;
            }

            _currentName = "";
            return false;
        }

        public void Reset()
        {
            _index = -1;
            _currentName = "";
        }
    }
}
