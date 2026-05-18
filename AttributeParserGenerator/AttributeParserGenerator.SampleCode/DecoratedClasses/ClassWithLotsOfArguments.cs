using AttributeParserGenerator.SampleCode.TestAttributes;

namespace AttributeParserGenerator.SampleCode.DecoratedClasses;

[AttributeWithLotsOfArguments(
    "Arg 1", "Arg 2", "Arg 3", "Arg 4", "Arg 5", "Arg 6", "Arg 7", "Arg 8", "Arg 9", "Arg 10",
    IntValue1 = 1, IntValue2 = 2, IntValue3 = 3, IntValue4 = 4, IntValue5 = 5, IntValue6 = 6, IntValue7 = 7, IntValue8 = 8, IntValue9 = 9, IntValue10 = 10)]
public class ClassWithLotsOfArguments;
