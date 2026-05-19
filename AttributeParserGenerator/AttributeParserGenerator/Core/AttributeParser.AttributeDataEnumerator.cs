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
        private readonly AttributeTypeInfo _info = parser.GetAttributeTypeInfo(attributeData);

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
            var index = _index + 1;
            if (index >= _info.CountArguments)
            {
                return false;
            }

            _index = index;
            _currentName = _info.ArgumentNames[_index];
            if (TryGetNamedArgument(out var value))
            {
                _currentValue = value;
                return true;
            }

            if (TryGetConstructorParameter(out value))
            {
                _currentValue = value;
                return true;
            }

            _currentValue = null;
            return true;
        }

        private bool TryGetNamedArgument(out object? o)
        {
            foreach (var keyValuePair in _attributeData.NamedArguments)
            {
                if (!string.Equals(keyValuePair.Key, _currentName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                o = keyValuePair.Value.Value;
                return true;
            }

            o = null;
            return false;
        }

        private bool TryGetConstructorParameter(out object? value)
        {
            if (_attributeData.AttributeConstructor == null)
            {
                value = null;
                return false;
            }

            var parameters = _attributeData.AttributeConstructor.Parameters;

            for (var i = 0; i < parameters.Length; i++)
            {
                var item = parameters.ItemRef(i);
                if (!string.Equals(item.Name, _currentName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                value = _attributeData.ConstructorArguments[i].Value;
                return true;
            }

            value = null;
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
