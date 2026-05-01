using System;
using System.Runtime.CompilerServices;

namespace Foxy.PocoDictionary.SourceGenerator.Helpers;

public static class ComparableArrayBuilder
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComparableArray<T> Create<T>(ReadOnlySpan<T> array)
        where T : class, IEquatable<T?>
    {
        return new ComparableArray<T>(array.ToArray());
    }
}
