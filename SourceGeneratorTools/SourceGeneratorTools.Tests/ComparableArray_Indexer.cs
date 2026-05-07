namespace SourceGeneratorTools.Tests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_Indexer
{
    [Test]
    public async Task Indexer_OnNonEmptyArray_ReturnsElement()
    {
        var array = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(array[0]).IsEqualTo(1);
        await Assert.That(array[1]).IsEqualTo(2);
        await Assert.That(array[2]).IsEqualTo(3);
    }

    [Test]
    public async Task Indexer_OnNullArray_ThrowsException()
    {
        ComparableArray<int> array = default;

        await Assert.That(() => { _ = array[0]; })
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Indexer_WithOutOfRangeIndex_ThrowsException()
    {
        var array = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(() => { _ = array[3]; })
            .Throws<IndexOutOfRangeException>();
    }
}
