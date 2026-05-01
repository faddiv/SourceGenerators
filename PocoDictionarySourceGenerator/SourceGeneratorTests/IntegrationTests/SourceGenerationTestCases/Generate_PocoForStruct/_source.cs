#nullable enable

using Foxy.PocoDictionary;
using System;

namespace Something;

[PocoDictionary]
public partial struct Foo
{
    public string? Bar { get; set; }
    
    public int? Baz { get; set; }
}
