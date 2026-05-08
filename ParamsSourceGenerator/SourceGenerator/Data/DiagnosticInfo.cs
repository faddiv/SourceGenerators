using Microsoft.CodeAnalysis;
using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

internal sealed record DiagnosticInfo(
    DiagnosticDescriptor Descriptor,
    Location Location,
    params ComparableArray<object> Args)
{
    internal static DiagnosticInfo Create(DiagnosticDescriptor descriptor, Location location, params object[] args)
    {
        return new DiagnosticInfo(descriptor, location, args);
    }

    internal Diagnostic ToDiagnostics()
    {
        return Diagnostic.Create(Descriptor, Location, Args.ToArray());
    }
}
