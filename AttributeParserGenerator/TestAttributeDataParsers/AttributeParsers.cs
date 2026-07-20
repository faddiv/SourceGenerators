using AttributeParser;
using AttributeParser.Core;
using AttributeParserGenerator.SampleCode.TestAttributes;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.TestInfrastructure;

public static partial class AttributeParsers
{
    [AttributeParser]
    public static partial InputWithDifferentTypesData ParseInputWithDifferentTypes(
        AttributeData attributeData,
        AttributeDataParser parser);

    [AttributeParser]
    public static partial FirstInputData ParseFirstInputData(
        AttributeData attributeData,
        AttributeDataParser parser);

    [AttributeParser]
    public static partial AttributeWithLotsOfArgumentsData ParseAttributeWithLotsOfArgumentsData(
        AttributeData attributeData,
        AttributeDataParser parser);

    [AttributeParser]
    public static partial InputWithDifferentTypesData ParseInputWithDifferentTypesData(
        AttributeData attributeData,
        AttributeDataParser parser);

    [AttributeParser]
    public static partial InputWithOptionalData ParseInputWithOptionalData(
        AttributeData attributeData,
        AttributeDataParser parser);
}
