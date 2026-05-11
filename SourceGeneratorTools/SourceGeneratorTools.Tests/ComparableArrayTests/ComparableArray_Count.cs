namespace SourceGeneratorTools.Tests.ComparableArrayTests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_Count
{
    [Test]
    public async Task Count_OnNonEmptyArray_ReturnsNonZero()
    {
        var array = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(array.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Count_OnNullArray_ReturnsZero()
    {
        ComparableArray<int> array = default;

        await Assert.That(array.Count).IsEqualTo(0);
    }
}
