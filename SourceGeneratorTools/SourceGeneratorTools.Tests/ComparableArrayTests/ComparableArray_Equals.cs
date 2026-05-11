namespace SourceGeneratorTools.Tests.ComparableArrayTests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_Equals
{
    [Test]
    public async Task Equals_WithSameElements_ReturnsTrue()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(() => array1.Equals(array2))
            .ThrowsNothing();
        await Assert.That(array1.Equals(array2)).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentElements_ReturnsFalse()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 4]);

        await Assert.That(array1.Equals(array2)).IsFalse();
    }

    [Test]
    public async Task Equals_OnDefaultArray_ReturnsTrue()
    {
        ComparableArray<int> array1 = default;
        ComparableArray<int> array2 = default;

        await Assert.That(array1.Equals(array2)).IsTrue();
    }

    [Test]
    public async Task Equals_OnDefaultArrayWithNonDefaultArray_ReturnsFalse()
    {
        ComparableArray<int> array1 = default;
        var array2 = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(() => array1.Equals(array2))
            .ThrowsNothing();

        await Assert.That(array1.Equals(array2))
            .IsFalse();
    }

    [Test]
    public async Task Equals_OnNonDefaultArrayWithDefaultArray_ReturnsFalse()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        ComparableArray<int> array2 = default;

        await Assert.That(() => array1.Equals(array2))
            .ThrowsNothing();
        await Assert.That(array1.Equals(array2)).IsFalse();
    }

    [Test]
    public async Task Equals_OnSameReference_ReturnsTrue()
    {
        int[] originalArray = [1, 2, 3];
        var array1 = new ComparableArray<int>(originalArray);
        var array2 = new ComparableArray<int>(originalArray);

        await Assert.That(array1.Equals(array2)).IsTrue();
    }

    [Test]
    public async Task Equals_OnDifferentlySizedArrays_ReturnsFalse()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2]);

        await Assert.That(array1.Equals(array2)).IsFalse();
    }

    [Test]
    [Arguments(1, null, false)]
    [Arguments(null, 1, false)]
    [Arguments(null, null, true)]
    [Arguments(1, 1, true)]
    public async Task Equals_OnNullStructElement_ReturnsFalse(int? item1, int? item2, bool equals)
    {
        var array1 = new ComparableArray<int?>([1, item1, 3]);
        var array2 = new ComparableArray<int?>([1, item2, 3]);

        await Assert.That(array1.Equals(array2)).IsEqualTo(equals);
    }

    [Test]
    [Arguments("1", null, false)]
    [Arguments(null, "1", false)]
    [Arguments(null, null, true)]
    [Arguments("1", "1", true)]
    public async Task Equals_OnNullClassElement_ReturnsFalse(string? item1, string? item2, bool equals)
    {
        var array1 = new ComparableArray<string?>(["1", item1, "3"]);
        var array2 = new ComparableArray<string?>(["1", item2, "3"]);

        await Assert.That(() => array1.Equals(array2))
            .ThrowsNothing();
        await Assert.That(array1.Equals(array2)).IsEqualTo(equals);
    }

    [Test]
    public async Task Equals_OnObject_ReturnsFalse()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var obj = new object();

        await Assert.That(array1.Equals(obj)).IsFalse();
    }

    [Test]
    public async Task Equals_OnComparableArrayCastedToObject_ReturnsTrue()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var obj = (object)array1;

        await Assert.That(array1.Equals(obj)).IsTrue();
    }

    [Test]
    public async Task Equals_OnNullObject_ReturnsFalse()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        object? obj = null;

        await Assert.That(array1.Equals(obj)).IsFalse();
    }
}
