using AttributeParserGenerator.SampleCode.TestAttributes;

namespace AttributeParserGenerator.SampleCode.DecoratedClasses;

[InputWithDifferentTypes(
    "Test Value",
    42,
    true,
    EnumValue.Value2,
    3.14,
    typeof(TargetClass))]
public class ClassWithProvidedDifferentTypes;
