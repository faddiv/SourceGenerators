using Foxy.Params.SourceGenerator.Helpers;
using System.Threading.Tasks;

namespace SourceGeneratorTests
{
    public class CollectionComparerTests
    {
        [Test]
        public async Task GetHashCode_NullList_ShouldReturnDefaultHashCode()
        {
            // Act
            var result = CollectionComparer.GetHashCode<object>(null);

            // Assert
            await Assert.That(result).IsEqualTo(2011230944);
        }

        [Test]
        public async Task GetHashCode_NonEmptyList_ShouldReturnCorrectHashCode()
        {
            // Arrange
            var list = CreateList();

            // Act
            var result = CollectionComparer.GetHashCode(list);

            // Assert
            var expectedHashCode = 1884520134;
            await Assert.That(result).IsEqualTo(expectedHashCode);
        }

        [Test]
        public async Task GetHashCode_FromDifferentInstances_ShouldReturnTheSameHashCode()
        {
            // Arrange
            var list1 = CreateList();
            var list2 = CreateList();

            // Act
            var result1 = CollectionComparer.GetHashCode(list1);
            var result2 = CollectionComparer.GetHashCode(list2);

            // Assert
            await Assert.That(result2).IsEqualTo(result1);
        }

        private static int[] CreateList()
        {
            return [1, 2, 3];
        }

    }
}
