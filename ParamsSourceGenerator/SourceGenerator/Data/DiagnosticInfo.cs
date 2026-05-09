using System.Linq;
using Microsoft.CodeAnalysis;
using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

public sealed record DiagnosticInfo(
    DiagnosticDescriptor Descriptor,
    Location Location,
    params ComparableArray<object> Args)
{
    public static DiagnosticInfo Create(DiagnosticDescriptor descriptor, Location location, params object[] args)
    {
        return new DiagnosticInfo(descriptor, location, args);
    }

    public Diagnostic ToDiagnostics()
    {
        return Diagnostic.Create(Descriptor, Location, Args.ToArray());
    }
}
