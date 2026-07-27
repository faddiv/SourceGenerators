using System.Diagnostics.CodeAnalysis;
using AttributeParser.Core;
using Microsoft.CodeAnalysis;
using TestInfrastructure;

namespace AttributeParser.Tests.TestInfrstructure;

public class AttributeDataParserHarness : CompilationHarness
{
    public AttributeDataParserHarness()
    {
        AddGlobalUsings("System");
        AddMetadataReference(typeof(AttributeParserAttribute));
    }

    public AttributeDataParser Parser => new();

    public void AddAttributeOnClass([StringSyntax("csharp")]string attribute, CancellationToken token)
    {
        AddSource(
            $"""
            {attribute}
            public class ExampleClass;
            """,
            token);
    }

    public async Task<AttributeDataParser.AttributeArgument> ParseAndGetSingleArgument(
        CancellationToken token,
        string fullName = "ExampleClass")
    {
        var attributeData = GetAttributeDataOnClass(fullName, token);
        var arguments = Parser.Parse(attributeData).ToArray();

        await Assert.That(arguments).Count().IsEqualTo(1);
        return arguments[0];
    }

    public async Task<AttributeDataParser.AttributeArgument[]> ParseAndGetArguments(
        CancellationToken token,
        string fullName = "ExampleClass")
    {
        var attributeData = GetAttributeDataOnClass(fullName, token);
        return Parser.Parse(attributeData).ToArray();

    }

    /// <summary>
    /// Retrieves the attribute data associated with a specified class.
    /// </summary>
    /// <param name="fullName">The fully qualified name of the class for which the attribute data is to be retrieved.</param>
    /// <returns>The <see cref="Microsoft.CodeAnalysis.AttributeData"/> instance representing the attribute data on the specified class.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no attributes are found on the specified class or the class does not exist.</exception>
    public AttributeData GetAttributeDataOnClass(string fullName, CancellationToken token)
    {
        var compilation = CompileNoDiagnostics(token);

        return compilation.GetTypeByMetadataName(fullName)?.GetAttributes().Single() ??
               throw new InvalidOperationException($"Attribute not found on class {fullName}");
    }
}
