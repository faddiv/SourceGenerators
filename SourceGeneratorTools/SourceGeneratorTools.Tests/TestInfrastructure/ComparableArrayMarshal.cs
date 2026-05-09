namespace SourceGeneratorTools.Tests.TestInfrastructure;

public static class ComparableArrayMarshal
{
    public static IReadOnlyList<T>? GetInternalArray<T>(ComparableArray<T> array)
    {
        return array
            .GetType()
            .GetField("_array", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .GetValue(array) as IReadOnlyList<T>;
    }
}
