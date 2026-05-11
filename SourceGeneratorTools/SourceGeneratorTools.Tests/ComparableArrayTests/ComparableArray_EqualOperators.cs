namespace SourceGeneratorTools.Tests.ComparableArrayTests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_EqualOperators
{
    [Test]
    public async Task EqualOperator_WithSameElements_ReturnsTrue()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(array1 == array2).IsTrue();
    }

    [Test]
    public async Task EqualOperator_WithDifferentElements_ReturnsFalse()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 4]);

        await Assert.That(array1 == array2).IsFalse();
    }

    [Test]
    public async Task NotEqualOperator_WithDifferentElements_ReturnsTrue()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 4]);

        await Assert.That(array1 != array2).IsTrue();
    }

    [Test]
    public async Task NotEqualOperator_WithSameElements_ReturnsFalse()
    {
        var array1 = new ComparableArray<int>([1, 2, 3]);
        var array2 = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(array1 != array2).IsFalse();
    }
}
