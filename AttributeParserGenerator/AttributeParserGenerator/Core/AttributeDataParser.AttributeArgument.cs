using System;
using System.Collections.Immutable;
using System.Linq;
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

            return _parser.ToCamelCase(_attributeData
                .NamedArguments[_index - _attributeData.ConstructorArguments.Length]
                .Key);
        }

        public object? GetValue()
        {
            var typedConstant = GetTypedConstant();
            switch (typedConstant.Kind)
            {
                case TypedConstantKind.Error:
                    throw new InvalidOperationException();

                case TypedConstantKind.Primitive:
                    return typedConstant.Value;

                case TypedConstantKind.Enum:
                    return typedConstant.Value;

                case TypedConstantKind.Type:
                    return typedConstant.Value;

                case TypedConstantKind.Array:
                    return typedConstant.Values.Select(x => x.Value).ToArray();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private TypedConstant GetTypedConstant()
        {
            var constructorArguments = _attributeData.ConstructorArguments.Length;
            if (_index < constructorArguments)
            {
                var argument = _attributeData.ConstructorArguments[_index];
                return argument;
            }

            var namedArgument =
                _attributeData.NamedArguments[_index - constructorArguments];
            return namedArgument.Value;
        }

        public T? GetValue<T>()
        {
            var typedConstant = GetTypedConstant();
            switch (typedConstant.Kind)
            {
                case TypedConstantKind.Error:
                    return default;

                case TypedConstantKind.Primitive:
                    return (T?)typedConstant.Value;

                case TypedConstantKind.Enum:
                    return (T?)typedConstant.Value;

                case TypedConstantKind.Type:
                    return (T?)typedConstant.Value;

                case TypedConstantKind.Array:
                    throw new InvalidOperationException("Expected a primitive typed constant.");

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ImmutableArray<T> GetValues<T>()
        {
            var typedConstant = GetTypedConstant();
            switch (typedConstant.Kind)
            {
                case TypedConstantKind.Error:
                    return ImmutableArray<T>.Empty;

                case TypedConstantKind.Primitive:
                case TypedConstantKind.Enum:
                case TypedConstantKind.Type:
                    throw new InvalidOperationException("Expected an array typed constant.");

                case TypedConstantKind.Array:
                    return [..typedConstant.Values.Select(x => (T)x.Value!)];

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
