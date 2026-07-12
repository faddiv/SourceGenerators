using AttributeParser;
using AttributeParser.Core;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.TestInfrastructure;

public static partial class AttributeParsers
{
    [AttributeParser]
    public static partial InputWithDifferentTypes ParseInputWithDifferentTypes(
        AttributeData attributeData,
        AttributeDataParser parser);
}
