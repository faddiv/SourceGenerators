namespace SourceGeneratorTools.Tests;

public static class TestHelpers
{
    public static string GenerateRandomName()
    {
        return Random.Shared.GetString("abcdefghijklmnopqrstuvwxyz", 5);
    }
}
