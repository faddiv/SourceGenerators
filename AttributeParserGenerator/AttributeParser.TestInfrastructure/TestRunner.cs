using AttributeParser.Core;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.TestInfrastructure;

public class TestRunner
{
    public static Dictionary<string, object?> ExtractValues(AttributeDataParser parser, AttributeData attributeData)
    {
        var results = new Dictionary<string, object?>();
        foreach (var result in parser.Parse(attributeData))
        {
            var name = result.GetName();
            var value = result.GetValue();
            results[name] = value;
        }

        return results;
    }

    public static InputWithDifferentTypes DeserializeInputWithDifferentTypes(
        AttributeDataParser parser,
        AttributeData attributeData)
    {
        var result = new InputWithDifferentTypes();
        foreach (var item in parser.Parse(attributeData))
        {
            switch (item.GetName())
            {
                case "intValue":
                    result.IntValue = item.GetValue<int>();
                    break;
                case "stringValue":
                    result.StringValue = item.GetValue<string>();
                    break;
                case "boolValue":
                    result.BoolValue = item.GetValue<bool>();
                    break;
                case "doubleValue":
                    result.DoubleValue = item.GetValue<double>()!;
                    break;
                case "enumValue":
                    result.EnumValue = item.GetValue<EnumValue>();
                    break;
                case "typeValue":
                    result.TypeValue = item.GetValue<INamedTypeSymbol>();
                    break;
                case "stringArray":
                    result.StringArray = item.GetValues<string>();
                    break;
                case "intArray":
                    result.IntArray = item.GetValues<int>();
                    break;
                case "boolArray":
                    result.BoolArray = item.GetValues<bool>();
                    break;
                case "enumArray":
                    result.EnumArray = item.GetValues<EnumValue>();
                    break;
                case "doubleArray":
                    result.DoubleArray = item.GetValues<double>();
                    break;
                case "typeArray":
                    result.TypeArray = item.GetValues<INamedTypeSymbol>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return result;
    }
}
