using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Test]
    public async Task AppendLineInterpolated_WithString_AddsLineWithText()
    {
        var builder = new SourceBuilder();
        var name = TestHelpers.GenerateRandomName();

        builder.AppendLine($"Hello, {name}!");

        await Assert.That(builder).HasContent($"Hello, {name}!");
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\r\n")]
    public async Task AppendLineInterpolated_WithNewLine_AddsNewLine(string newLine)
    {
        var builder = new SourceBuilder(newLine: newLine);
        var name = TestHelpers.GenerateRandomName();

        builder.AppendLine($"Hello, {name}!");

        await Assert.That(builder).HasRawContent($"Hello, {name}!{newLine}");
    }

    [Test]
    public async Task AppendLineInterpolated_WithArray_AddsCommaSeparatedList()
    {
        var builder = new SourceBuilder();
        var items = new[] { "Item1", "Item2", "Item3" };

        builder.AppendLine($"({items})");

        await Assert.That(builder).HasContent("(Item1, Item2, Item3)");
    }

    [Test]
    public async Task AppendLineInterpolated_WithNullArray_AddsEmptyParentheses()
    {
        var builder = new SourceBuilder();
        string[]? items = null;

        builder.AppendLine($"({items})");

        await Assert.That(builder).HasContent("()");
    }

    [Test]
    public async Task AppendLineInterpolated_WithIEnumerable_AddsCommaSeparatedList()
    {
        var builder = new SourceBuilder();
        IEnumerable<string> items = ["Item1", "Item2", "Item3"];

        builder.AppendLine($"({items})");

        await Assert.That(builder).HasContent("(Item1, Item2, Item3)");
    }

    [Test]
    public async Task AppendLineInterpolated_WithEmptyIEnumerable_AddsEmptyParentheses()
    {
        var builder = new SourceBuilder();
        IEnumerable<string> items = Array.Empty<string>();

        builder.AppendLine($"({items})");

        await Assert.That(builder).HasContent("()");
    }

    [Test]
    public async Task AppendLineInterpolated_WithIEnumerableCastedObject_AddsCommaSeparatedList()
    {
        var builder = new SourceBuilder();
        object items = new[] { "Item1", "Item2", "Item3" };

        builder.AppendLine($"({items})");

        await Assert.That(builder).HasContent("(Item1, Item2, Item3)");
    }

    [Test]
    public async Task AppendLineInterpolated_WithNullIEnumerable_AddsEmptyString()
    {
        var builder = new SourceBuilder();
        IEnumerable<string>? items = null;

        builder.AppendLine($"({items})");

        await Assert.That(builder).HasContent("()");
    }

    [Test]
    public async Task AppendLineInterpolated_WithInt32_AddsValue()
    {
        var builder = new SourceBuilder();
        const int value = 42;

        builder.AppendLine($"Value: {value}");

        await Assert.That(builder).HasContent("Value: 42");
    }

    [Test]
    [Arguments(null)]
    [Arguments(42)]
    public async Task AppendLineInterpolated_WithInt32Nullable_AddsValue(int? value)
    {
        var builder = new SourceBuilder();

        builder.AppendLine($"Value: {value}");

        await Assert.That(builder).HasContent($"Value: {value}");
    }

    [Test]
    public async Task AppendLineInterpolated_WithNull_AddsEmptyString()
    {
        var builder = new SourceBuilder();
        object? value = null;

        builder.AppendLine($"Value: {value}");

        await Assert.That(builder).HasContent("Value: ");
    }

    [Test]
    public async Task AppendLineInterpolated_WithObject_AddsObjectToString()
    {
        var builder = new SourceBuilder();
        var value = new { Name = "Test" };

        builder.AppendLine($"Value: {value}");

        await Assert.That(builder).HasContent("Value: { Name = Test }");
    }
}
