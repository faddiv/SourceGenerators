using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AttributeParser.Tests;

public static class CSharpCompilationExtension
{
    /// <summary>
    /// Retrieves the attribute data applied to a class with the specified fully qualified name. Throws an exception if the attribute is not found.
    /// </summary>
    /// <param name="compilation">The current C# compilation instance.</param>
    /// <param name="fullName">The fully qualified name of the class to inspect for attributes.</param>
    /// <returns>The attribute data applied to the specified class.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no attributes are found on the specified class.</exception>
    public static AttributeData GetAttributeDataOnClass(this CSharpCompilation compilation, string fullName)
    {
        return compilation.GetTypeByMetadataName(fullName)?.GetAttributes().Single() ??
               throw new InvalidOperationException($"Attribute not found on class {fullName}");
    }
}
