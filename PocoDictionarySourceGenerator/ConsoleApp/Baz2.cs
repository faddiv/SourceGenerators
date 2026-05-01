namespace ConsoleApp;

public partial record struct Baz2 : global::System.Collections.Generic.IReadOnlyDictionary<string, object?>
{
    private static readonly System.Collections.Immutable.ImmutableArray<string> _keys = ["Foo", "Bar", "Date"];

    public int Count => 3;

    public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<string, object?>> GetEnumerator()
    {
        yield return new global::System.Collections.Generic.KeyValuePair<string, object?>("Foo", Foo);
        yield return new global::System.Collections.Generic.KeyValuePair<string, object?>("Bar", Bar);
        yield return new global::System.Collections.Generic.KeyValuePair<string, object?>("Date", Date);
    }

    global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool ContainsKey(string key)
    {
        return key is "Foo" or "Bar" or "Date";
    }

    public bool TryGetValue(string key, out object value)
    {
        value = key switch
        {
            "Foo" => Foo,
            "Bar" => Bar,
            "Date" => Date,
            _ => throw new global::System.Collections.Generic.KeyNotFoundException($"Key {key} not found")
        };
        return true;
    }

    public object this[string key] => TryGetValue(key, out var value)
        ? value
        : throw new global::System.Collections.Generic.KeyNotFoundException($"Key {key} not found");

    public global::System.Collections.Generic.IEnumerable<string> Keys => _keys;
    public global::System.Collections.Generic.IEnumerable<object> Values => [Foo, Bar, Date];
}