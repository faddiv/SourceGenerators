using System.Collections.Immutable;

namespace SourceGeneratorTools.Tests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_Cast
{
    [Test]
    public async Task ImplicitCastFromArray_CreatesComparableArray()
    {
        int[] ints = [1, 2, 3];
        ComparableArray<int> array = ints;

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task ImplicitCastFromImmutableArray_CreatesComparableArray()
    {
        ImmutableArray<int> ints = [1, 2, 3];
        ComparableArray<int> array = ints;

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }
    [Test]
    public async Task ImplicitCastFromList_CreatesComparableArray()
    {
        var list = new List<int> { 1, 2, 3 };
        ComparableArray<int> array = list;

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }
}
