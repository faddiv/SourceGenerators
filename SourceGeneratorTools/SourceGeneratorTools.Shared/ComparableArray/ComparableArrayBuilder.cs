using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SourceGeneratorTools;

public static class ComparableArray
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComparableArray<T> Create<T>(ReadOnlySpan<T> array)
    {
        return new ComparableArray<T>(array.ToArray());
    }

    public static ComparableArray<T> Create<T>(IEnumerable<T> enumeration)
    {
        var array = enumeration.ToArray();
        return new ComparableArray<T>(array);
    }
}
