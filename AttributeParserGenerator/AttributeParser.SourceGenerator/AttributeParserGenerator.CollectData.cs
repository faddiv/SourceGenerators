using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AttributeParser.SourceGenerator.Data;
using Microsoft.CodeAnalysis;

namespace AttributeParser.SourceGenerator;

public partial class AttributeParserGenerator
{
    private static Result CollectData(
        GeneratorAttributeSyntaxContext syntaxContext,
        CancellationToken cancellationToken)
    {
        try
        {
            var methodSymbol = syntaxContext.TargetSymbol as IMethodSymbol;
            if (methodSymbol is null)
            {
                return new Error(syntaxContext.TargetNode.GetLocation(), "Target symbol is not a method symbol.");
            }

            var parentClass = methodSymbol.ContainingType;

            var containingType = ExtractContainingTypeInfo(parentClass);
            var methodName = methodSymbol.Name;
            var output = ExtractOutputInfo(methodSymbol);

            ParserMethodParameter? attributeDataParser = null;
            ParserMethodParameter? attributeData = null;
            var invalidParameters = new List<ParserMethodParameter>();
            for (var index = 0; index < methodSymbol.Parameters.Length; index++)
            {
                var parameter = methodSymbol.Parameters[index];
                var parameterName = parameter.Name;
                var parameterType = parameter.Type.ToDisplayString();
                if (parameterType == _attributeDataParserTypeName)
                {
                    attributeDataParser = new ParserMethodParameter(parameterName, parameterType, index);
                }
                else if (parameterType == _attributeDataTypeName)
                {
                    attributeData = new ParserMethodParameter(parameterName, parameterType, index);
                }
                else
                {
                    invalidParameters.Add(new ParserMethodParameter(parameterName, parameterType, index));
                }
            }

            if (attributeDataParser is null)
            {
                return new Error(
                    syntaxContext.TargetNode.GetLocation(),
                    $"Method '{methodName}' is missing a parameter of type '{_attributeDataParserTypeName}'.");
            }

            if (attributeData is null)
            {
                return new Error(
                    syntaxContext.TargetNode.GetLocation(),
                    $"Method '{methodName}' is missing a parameter of type '{_attributeParserAttributeName}'.");
            }

            if (invalidParameters.Count > 0)
            {
                return new Error(
                    syntaxContext.TargetNode.GetLocation(),
                    $"Method '{methodName}' has invalid parameters: {string.Join(", ", invalidParameters.Select(e => e.Name))}.");
            }

            return new ParserMethodData(
                containingType,
                methodName,
                output,
                attributeDataParser,
                attributeData);
        }
        catch (Exception e)
        {
            return new Error(
                syntaxContext.TargetNode.GetLocation(),
                $"An error occurred while collecting data: {e.Message}");
        }
    }

    private static ContainingType ExtractContainingTypeInfo(INamedTypeSymbol parentClass)
    {
        var className = parentClass.Name;
        var classNamespace = parentClass.ContainingNamespace.IsGlobalNamespace
            ? ""
            : parentClass.ContainingNamespace.ToDisplayString();
        var containingType = new ContainingType(classNamespace, className);
        return containingType;
    }

    private static ParserMethodOutput ExtractOutputInfo(IMethodSymbol methodSymbol)
    {
        var returnType = methodSymbol.ReturnType;
        var fullName = returnType.ToDisplayString();
        var parameters = new List<ParserMethodOutputField>();
        foreach (var fields in returnType.GetMembers().OfType<IPropertySymbol>())
        {
            parameters.Add(new ParserMethodOutputField(fields.Name, fields.Type.ToDisplayString(),
                TryGetEnumerableElementType(fields.Type as INamedTypeSymbol)));
        }

        return new ParserMethodOutput(fullName, parameters);
    }

    private static string? TryGetEnumerableElementType(INamedTypeSymbol? fieldsType)
    {
        if(fieldsType?.Name is "ImmutableArray" &&
           fieldsType.ContainingNamespace.ToDisplayString() is "System.Collections.Immutable")
        {
            return fieldsType.TypeArguments[0].ToDisplayString();
        }

        return null;
    }
}
