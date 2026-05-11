using System.Runtime.InteropServices;
using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests.ComparableArrayTests;

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
    public async Task ImplicitCastFromImmutableArray_CreatesComparableArrayFromInternalArray()
    {
        int[] innerArray = [1, 2, 3];
        var ints = ImmutableCollectionsMarshal.AsImmutableArray(innerArray);
        ComparableArray<int> array = ints;
        var actualArray = ComparableArrayMarshal.GetInternalArray(array);

        await Assert.That(actualArray).IsSameReferenceAs(innerArray);
    }
    [Test]
    public async Task ImplicitCastFromList_CreatesComparableArray()
    {
        var list = new List<int> { 1, 2, 3 };
        ComparableArray<int> array = list;

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }
}
