using System;
using Foxy.PocoDictionary;

namespace ConsoleApp;

public partial class Baz
{
    public int Foo { get; set; }
    public string Bar { get; set; }
    public DateTime Date { get; set; }
}

public partial record struct Baz2(
    int Foo,
    string Bar,
    DateTime Date);
