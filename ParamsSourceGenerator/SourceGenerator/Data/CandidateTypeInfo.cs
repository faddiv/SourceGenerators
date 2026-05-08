using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data
{
    public sealed record CandidateTypeInfo
    {
        public required string TypeName { get; init; }

        public required bool InGlobalNamespace { get; init; }

        public required ComparableArray<string> TypeHierarchy { get; init; }

        public required string Namespace { get; init; }
    }
}
