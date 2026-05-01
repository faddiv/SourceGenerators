using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Foxy.PocoDictionary.SourceGenerator.Helpers;

[CollectionBuilder(typeof(ComparableArrayBuilder), nameof(ComparableArrayBuilder.Create))]
public readonly struct ComparableArray<T>(T[] array)
    : IEquatable<ComparableArray<T>>,
        IReadOnlyList<T>
    where T : class?, IEquatable<T>?
{
    private readonly T[]? _array = array;

    public static implicit operator ComparableArray<T>(T[] array)
    {
        return new ComparableArray<T>(array);
    }

    public bool Equals(ComparableArray<T> other)
    {
        var thisArray = _array;
        var otherArray = other._array;
        if (thisArray is null && otherArray is null) return true;
        if (thisArray is null || otherArray is null) return false;
        if (ReferenceEquals(thisArray, otherArray)) return true;
        if (thisArray.Length != otherArray.Length) return false;
        for (var i = 0; i < thisArray.Length; i++)
        {
            var thisItem = thisArray[i];
            var otherItem = otherArray[i];
            if (thisItem is null && otherItem is null) continue;
            if (thisItem is null || otherItem is null) return false;
            if (thisItem.Equals(otherItem)) continue;
            return false;
        }

        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (_array is IEnumerable<T> enumerable)
            return enumerable.GetEnumerator();
        return Enumerable.Empty<T>().GetEnumerator();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return obj.GetType() == GetType()
               && Equals((ComparableArray<T>)obj);
    }

    public override int GetHashCode()
    {
        if (_array is null) return 0;

        var hash = 17;
        foreach (var item in _array)
        {
            var itemHash = item?.GetHashCode() ?? 0;
            hash = hash * 23 + itemHash;
        }

        return hash;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static bool operator ==(ComparableArray<T>? left, ComparableArray<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ComparableArray<T>? left, ComparableArray<T>? right)
    {
        return !Equals(left, right);
    }

    public int Count => _array?.Length ?? 0;

    public T this[int index]
    {
        get
        {
            if (_array is null)
                throw new InvalidOperationException("The ComparableArray is not initialized.");
            return _array[index];
        }
    }
}
