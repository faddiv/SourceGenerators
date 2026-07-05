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

        private int MaxIndex => _attributeData.ConstructorArguments.Length + _attributeData.NamedArguments.Length;

        public AttributeArgument Current => _index < MaxIndex
            ? new AttributeArgument(_index, _attributeData, _parser)
            : throw new InvalidOperationException();

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index++;
            return _index < MaxIndex;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}
