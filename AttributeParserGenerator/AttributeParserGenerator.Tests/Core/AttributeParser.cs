using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AttributeParserGenerator.Tests.Core;

public class AttributeParser
{
    public Parser Parse(AttributeData attributeData)
    {
        return new Parser(attributeData, this);
    }

    public readonly struct Parser(AttributeData attributeData, AttributeParser parser) : IEnumerable<AttributeArgument>
    {
        private readonly AttributeData _attributeData = attributeData;
        private readonly AttributeParser _parser = parser;

        public ParserEnumerator GetEnumerator()
        {
            return new ParserEnumerator(_attributeData);
        }

        IEnumerator<AttributeArgument> IEnumerable<AttributeArgument>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object? GetValue(in AttributeArgument result)
        {
            if (result.Index < _attributeData.ConstructorArguments.Length)
            {
                var argument = _attributeData.ConstructorArguments[result.Index];
                return argument.Value;
            }

            var namedArgument =
                _attributeData.NamedArguments[result.Index - _attributeData.ConstructorArguments.Length];
            return namedArgument.Value.Value;
        }
    }

    public struct ParserEnumerator(AttributeData attributeData) : IEnumerator<AttributeArgument>
    {
        private readonly AttributeData _attributeData = attributeData;
        private int _index = -1;
        private string _currentName = "";

        private int MaxIndex => _attributeData.ConstructorArguments.Length + _attributeData.NamedArguments.Length;

        public AttributeArgument Current => _index < MaxIndex
            ? new AttributeArgument(_currentName, _index)
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
                _currentName = "";
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

    public readonly struct AttributeArgument(string name, int index)
    {
        public string Name { get; } = name;
        public int Index { get; } = index;
    }
}
