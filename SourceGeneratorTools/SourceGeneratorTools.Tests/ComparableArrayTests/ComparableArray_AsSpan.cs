using System.Runtime.CompilerServices;

namespace SourceGeneratorTools.Tests.ComparableArrayTests;

// ReSharper disable once InconsistentNaming
public class ComparableArray_AsSpan
{
    [Test]
    public async Task AsSpan_OnNonEmptyArray_ReturnsSpan()
    {
        int[] innerArray = [1, 2, 3];
        var array = new ComparableArray<int>(innerArray);
        var span = array.AsSpan();

        await Assert.That(Unsafe.AreSame(in span.GetPinnableReference(), ref innerArray[0])).IsTrue();
    }
}
