#nullable enable

using Foxy.PocoDictionary;
using System;

namespace Something;

[PocoDictionary]
public partial record struct Foo(
    string? Bar,
    int? Baz);
