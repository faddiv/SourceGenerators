using System.Runtime.CompilerServices;

namespace AttributeParser.SourceGenerator.Tests;

public static class Initialization
{
    [ModuleInitializer]
    public static void Initialize() => VerifySourceGenerators.Initialize();
}
