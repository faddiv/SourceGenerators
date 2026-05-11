using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests.ComparableArrayTests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_Ctor
{
    [Test]
    public async Task CollectionInitializerOnStruct_CreatesArrayWithElements()
    {
        ComparableArray<int> array = [1, 2, 3];

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task CollectionInitializerOnClass_CreatesArrayWithElements()
    {
        ComparableArray<string> array = ["1", "2", "3"];

        await Assert.That(array).IsEquivalentTo(["1", "2", "3"]);
    }

    [Test]
    public async Task CollectionInitializerWithSpreadOperator_CreatesArrayWithElements()
    {
        int[] array1 = [1, 2, 3];
        int[] array2 = [4, 5, 6];
        ComparableArray<int> array = [0, ..array1, ..array2, 7];

        await Assert.That(array).IsEquivalentTo([0, 1, 2, 3, 4, 5, 6, 7]);
    }

    [Test]
    public async Task CtorWithArray_CreatesArrayWithElements()
    {
        int[] ints = [1, 2, 3];
        var array = new ComparableArray<int>(ints);

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task CtorWithComparableArray_UnpacksInternalRepresentation()
    {
        int[] innerArray = [1, 2, 3];
        var originalArray = new ComparableArray<int>(innerArray);
        var array = new ComparableArray<int>(originalArray);
        var actualArray = ComparableArrayMarshal.GetInternalArray(array);

        await Assert.That(actualArray).IsSameReferenceAs(innerArray);
    }

    [Test]
    public async Task DefaultCtor_CreatesEmptyArray()
    {
        ComparableArray<int> array = default;

        await Assert.That(array).IsEmpty();
    }

    [Test]
    public async Task Create_WithIEnumerable_CreatesArrayWithElements()
    {
        IEnumerable<int> ints = [1, 2, 3];
        var array = SourceGeneratorTools.ComparableArray.Create(ints);

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }
}
