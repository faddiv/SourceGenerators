#nullable enable

using Foxy.PocoDictionary;
using System;

namespace Something;

public partial class OuterClass
{
    [PocoDictionary]
    public partial class Foo
    {
        public string? Bar { get; set; }
    
        public int? Baz { get; set; }
    }
}
