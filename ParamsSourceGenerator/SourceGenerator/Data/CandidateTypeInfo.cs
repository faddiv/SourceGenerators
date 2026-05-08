using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data
{
    public sealed record CandidateTypeInfo(
        string TypeName,
        ComparableArray<string> TypeHierarchy,
        bool InGlobalNamespace,
        string Namespace);
}
