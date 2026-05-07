namespace SourceGeneratorTools.Tests;

public class ComparableArray_GetHashCode
{
    [Test]
    public async Task GetHashCode_OnArrayWithSameElements_ReturnsSameHashCode()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(array1.GetHashCode()).IsEqualTo(array2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_OnArrayWithDifferentElements_ReturnsDifferentHashCode()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 4]);

        await Assert.That(array1.GetHashCode()).IsNotEqualTo(array2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_OnNullArray_ReturnsZero()
    {
        ComparableArray<int> array = default;

        await Assert.That(array.GetHashCode()).IsEqualTo(0);
    }

    [Test]
    public async Task GetHashCode_OnEmptyArray_ReturnsZero()
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        ComparableArray<int> array = [];

        await Assert.That(array.GetHashCode()).IsEqualTo(0);
    }

    [Test]
    public async Task GetHashCode_OnNullElement_TreatsAsZero()
    {
        ComparableArray<string?> array = [null];

        await Assert.That(array.GetHashCode()).IsEqualTo(17*23);
    }
}
