using System;
using System.Collections;
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
                    return null;

                case TypedConstantKind.Primitive:
                    return typedConstant.Value;

                case TypedConstantKind.Enum:
                    return typedConstant.Value;

                case TypedConstantKind.Type:
                    return typedConstant.Value;

                case TypedConstantKind.Array:
                    return SelectArray(typedConstant);

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

        private IList SelectArray(TypedConstant typedConstant)
        {
            if (typedConstant.Type is not IArrayTypeSymbol arrayType)
            {
                return Array.Empty<object>();
            }

            return arrayType.ElementType.SpecialType switch
            {
                SpecialType.System_Boolean => typedConstant.Values.Select(x => (bool)x.Value!).ToImmutableArray(),
                SpecialType.System_Char => typedConstant.Values.Select(x => (char)x.Value!).ToImmutableArray(),
                SpecialType.System_SByte => typedConstant.Values.Select(x => (sbyte)x.Value!).ToImmutableArray(),
                SpecialType.System_Byte => typedConstant.Values.Select(x => (byte)x.Value!).ToImmutableArray(),
                SpecialType.System_Int16 => typedConstant.Values.Select(x => (short)x.Value!).ToImmutableArray(),
                SpecialType.System_UInt16 => typedConstant.Values.Select(x => (ushort)x.Value!).ToImmutableArray(),
                SpecialType.System_Int32 => typedConstant.Values.Select(x => (int)x.Value!).ToImmutableArray(),
                SpecialType.System_UInt32 => typedConstant.Values.Select(x => (uint)x.Value!).ToImmutableArray(),
                SpecialType.System_Int64 => typedConstant.Values.Select(x => (long)x.Value!).ToImmutableArray(),
                SpecialType.System_UInt64 => typedConstant.Values.Select(x => (ulong)x.Value!).ToImmutableArray(),
                SpecialType.System_Single => typedConstant.Values.Select(x => (float)x.Value!).ToImmutableArray(),
                SpecialType.System_Double => typedConstant.Values.Select(x => (double)x.Value!).ToImmutableArray(),
                SpecialType.System_String => typedConstant.Values.Select(x => (string?)x.Value).ToImmutableArray(),
                _ when arrayType.ElementType.TypeKind == TypeKind.Enum =>
                    typedConstant.Values.Select(x => (int)x.Value!).ToImmutableArray(),

                _ when arrayType.ElementType is { TypeKind: TypeKind.Class, Name: "Type" } =>
                    typedConstant.Values.Select(x => (INamedTypeSymbol?)x.Value).ToImmutableArray(),

                _ => throw new NotImplementedException()
            };
        }
    }
}
