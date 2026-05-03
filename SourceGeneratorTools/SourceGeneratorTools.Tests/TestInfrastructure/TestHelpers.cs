namespace SourceGeneratorTools.Tests.TestInfrastructure;

public static class TestHelpers
{
    public static string GenerateRandomName()
    {
        return Random.Shared.GetString("abcdefghijklmnopqrstuvwxyz", 5);
    }
}
