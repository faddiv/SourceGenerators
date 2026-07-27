// Accept all pending Verify snapshots in this directory.
// Usage (From solution directory): dotnet run accept.cs
#:property TargetFramework=net10.0
#:property ImplicitUsings=enable

const string receivedMarker = ".received.";
const string verifiedMarker = ".verified.";

var directory = Path.Combine(Environment.CurrentDirectory, "AttributeParser.SourceGenerator.Tests");
var receivedFiles = Directory
    .EnumerateFiles(directory, $"*{receivedMarker}*", SearchOption.AllDirectories)
    .Where(path => !IsBuildOutput(path))
    .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
    .ToArray();

if (receivedFiles.Length == 0)
{
    Console.WriteLine("No pending Verify snapshots found.");
    return;
}

foreach (var receivedPath in receivedFiles)
{
    var verifiedPath = receivedPath.Replace(receivedMarker, verifiedMarker, StringComparison.Ordinal);
    File.Move(receivedPath, verifiedPath, overwrite: true);
    Console.WriteLine($"Accepted: {Path.GetRelativePath(directory, verifiedPath)}");
}

Console.WriteLine($"Accepted {receivedFiles.Length} snapshot(s).");

static bool IsBuildOutput(string path) =>
    path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)
    || path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase);
