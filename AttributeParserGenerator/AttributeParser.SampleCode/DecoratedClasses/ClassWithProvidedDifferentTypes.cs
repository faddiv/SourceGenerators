using AttributeParserGenerator.SampleCode.TestAttributes;

namespace AttributeParserGenerator.SampleCode.DecoratedClasses;

[InputWithDifferentTypes(
    "Test Value",
    42,
    true,
    EnumValue.Value2,
    3.14,
    typeof(TargetClass),
    ["Test String 1", "Test String 2"],
    [1, 2, 3],
    [true, false, true],
    [EnumValue.Value1, EnumValue.Value3],
    [1.1, 2.2, 3.3],
    [typeof(TargetClass), typeof(AnotherTargetClass)])]
public class ClassWithProvidedDifferentTypes;
