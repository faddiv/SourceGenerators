using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using TUnit.Assertions.Conditions;
using TUnit.Assertions.Sources;

namespace AttributeParserGenerator.Sample;

public class TestHelpers
{
    public static List<SyntaxTree> GetSyntaxes()
    {
        var directory = new DirectoryInfo("../../../SampleCode");
        var syntaxTrees = new List<SyntaxTree>();
        foreach (var file in directory.GetFiles("*.cs"))
        {
            using var fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            var sourceText = SourceText.From(fileStream, Encoding.UTF8,
                SourceHashAlgorithm.Sha256);
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText, CSharpParseOptions.Default, path: file.Name);
            syntaxTrees.Add((CSharpSyntaxTree)syntaxTree);
        }

        return syntaxTrees;
    }

    public static MetadataReference[] GetFrameworkReferences()
    {
        string runtimeDir = RuntimeEnvironment.GetRuntimeDirectory();
        return AppDomain
            .CurrentDomain.GetAssemblies()
            .Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location) &&
                        x.Location.StartsWith(runtimeDir, StringComparison.OrdinalIgnoreCase))
            .Select(x => MetadataReference.CreateFromFile(x.Location))
            .Cast<MetadataReference>()
            .ToArray();

    }
}
public static class AssertExtensions
{
    extension(ValueAssertion<CSharpCompilation> source)
    {
        public SatisfiesAssertion<CSharpCompilation> HasNoDiagnostics()
        {
            source.Context.ExpressionBuilder.Append($".HasNoDiagnostics()");
            return source.Satisfies(static compilation =>
            {
                var diagnostics = compilation?.GetDiagnostics() ?? [];

                return diagnostics.Length == 0;
            });
        }
    }
}

