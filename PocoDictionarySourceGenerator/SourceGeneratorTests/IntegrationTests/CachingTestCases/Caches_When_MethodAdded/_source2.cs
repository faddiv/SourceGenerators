using Foxy.PocoDictionary;
using System;

namespace Something;

[PocoDictionary]
public partial class Foo
{
    public string? Bar { get; set; }

    public int? Method() => 42;
}
