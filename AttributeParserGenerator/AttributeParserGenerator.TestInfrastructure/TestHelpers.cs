using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace AttributeParserGenerator.TestInfrastructure;

public class TestHelpers
{
    public static List<SyntaxTree> GetSyntaxes()
    {
        var directory = GetFromSolutionDirectory("AttributeParserGenerator.SampleCode");
        var syntaxTrees = new List<SyntaxTree>();
        foreach (var file in GetCsFilesRecursively(directory))
        {
            using var fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            var sourceText = SourceText.From(fileStream, Encoding.UTF8,
                SourceHashAlgorithm.Sha256);
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText, CSharpParseOptions.Default, path: file.Name);
            syntaxTrees.Add((CSharpSyntaxTree)syntaxTree);
        }

        return syntaxTrees;
    }

    private static DirectoryInfo GetFromSolutionDirectory(string path)
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (currentDirectory != null)
        {
            if (currentDirectory.GetFiles("*.slnx").Length > 0)
            {
                return new DirectoryInfo(Path.Combine(currentDirectory.FullName, path));
            }

            currentDirectory = currentDirectory.Parent;
        }

        throw new InvalidOperationException(
            $"Could not find solution directory from current directory {Directory.GetCurrentDirectory()}");
    }


    private static IEnumerable<FileInfo> GetCsFilesRecursively(DirectoryInfo directory)
    {
        // List all files, except in obj and bin folders

        foreach (var file in directory.GetFiles("*.cs"))
        {
            yield return file;
        }

        foreach (var directoryInfo in directory.GetDirectories())
        {
            if (directoryInfo.Name is "obj" or "bin")
            {
                continue;
            }

            foreach (var file in directoryInfo.EnumerateFiles("*.cs"))
            {
                yield return file;
            }
        }
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
