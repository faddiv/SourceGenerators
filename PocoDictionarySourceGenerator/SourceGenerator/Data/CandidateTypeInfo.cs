using Foxy.PocoDictionary.SourceGenerator.Helpers;

namespace Foxy.PocoDictionary.SourceGenerator.Data;

public record CandidateTypeInfo(
    string TypeName,
    string? Namespace,
    ComparableArray<string> TypeHierarchy,
    ComparableArray<CandidatePropertyInfo> Properties,
    string TypeKind = "class")
{
    public bool InGlobalNamespace => Namespace is null;
    
    public string FullName => Namespace is null ? TypeName : $"{Namespace}.{TypeName}";
}
