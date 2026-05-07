
namespace SourceGeneratorTools.Tests;

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
    public async Task CtorWithList_CreatesArrayWithElements()
    {
        var list = new List<int> { 1, 2, 3 };
        var array = new ComparableArray<int>(list);

        await Assert.That(array).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task DefaultCtor_CreatesEmptyArray()
    {
        ComparableArray<int> array = default;

        await Assert.That(array).IsEmpty();
    }
}
