using System;
using System.Collections.Generic;
using System.Text;
using Foxy.PocoDictionary;

// ReSharper disable UnusedParameter.Local

namespace ConsoleApp
{
    partial class Program
    {
        static void Main()
        {
            Console.WriteLine("Baz fields:");
            var baz = new Baz { Foo = 42, Bar = "Hello", Date = new DateTime(2024, 1, 1) };
            foreach (var kv in baz)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }

            Console.WriteLine("Foo fields:");
            var foo = new Foo { Bar = "World", Baz = 100 };
            foreach (var kv in foo)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }
            
            Console.WriteLine($"By key: {foo["Bar"]}");
            Console.WriteLine($"Contains key: {foo.ContainsKey("Bar")}");
        }
    }
}
