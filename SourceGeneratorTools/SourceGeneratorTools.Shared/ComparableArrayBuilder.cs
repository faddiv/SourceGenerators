using System.Runtime.CompilerServices;

namespace SourceGeneratorTools;

public static class ComparableArrayBuilder
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComparableArray<T> Create<T>(ReadOnlySpan<T> array)
    {
        return new ComparableArray<T>(array.ToArray());
    }
}
