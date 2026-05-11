using System.Collections;

namespace SourceGeneratorTools.Tests.ComparableArrayTests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_GetEnumerator
{
    [Test]
    public async Task GetEnumerator_OnArray_EnumeratesAllElements()
    {
        var array = new ComparableArray<int>([1, 2, 3]);

        int value = 1;
        foreach (var i in array)
        {
            await Assert.That(i).IsEqualTo(value++);
        }
    }

    [Test]
    public async Task GetEnumerator_OnDefaultArray_ReturnsEmptyEnumerator()
    {
        ComparableArray<int> array = default;

        using var enumerator = array.GetEnumerator();
        await Assert.That(enumerator.MoveNext()).IsFalse();
    }

    [Test]
    public async Task GetEnumerator_OnEnumerable_EnumeratesAllElements()
    {
        IEnumerable array = new ComparableArray<int>([1, 2, 3]);

        await Assert.That(array.GetEnumerator)
            .ThrowsNothing()
            .And
            .IsNotNull();

        int value = 1;
        foreach (var i in array)
        {
            await Assert.That(i).IsEqualTo(value++);
        }
    }
}
