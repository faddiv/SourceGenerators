using Microsoft.CodeAnalysis;

namespace Foxy.Params.SourceGenerator.Helpers;

public static class FoxyEnumerableExtensions
{
    public static IncrementalValuesProvider<TSource> NotNull<TSource>(
        this IncrementalValuesProvider<TSource?> enumerable)
        where TSource : class
    {
        return enumerable.Where(x => x is not null)!;
    }
}
