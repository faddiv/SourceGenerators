using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace AttributeParser.PreviousImplementation;

public partial class AttributeDataParser
{
    public readonly struct ParseResult(AttributeData attributeData, AttributeDataParser parser)
        : IEnumerable<AttributeArgument>
    {
        private readonly AttributeData _attributeData = attributeData;
        private readonly AttributeDataParser _parser = parser;

        public AttributeDataEnumerator GetEnumerator()
        {
            return new AttributeDataEnumerator(_attributeData, _parser);
        }

        IEnumerator<AttributeArgument> IEnumerable<AttributeArgument>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
