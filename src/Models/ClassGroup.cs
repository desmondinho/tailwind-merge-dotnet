namespace TailwindMerge.Models;

internal readonly struct ClassGroup
{
    internal string Id { get; }
    internal string? ClassName { get; }
    internal string[]? Items { get; }
    internal Func<string, bool>[]? Validators { get; }

    internal ClassGroup( string id, string[] items )
        : this( id, null, items, null ) { }

    internal ClassGroup( string id, string className, string[] items )
        : this( id, className, items, null ) { }

    internal ClassGroup( string id, string className, Func<string, bool>[] validators )
        : this( id, className, null, validators ) { }

    internal ClassGroup( string id, string? className, string[]? items, Func<string, bool>[]? validators )
    {
        Id = id;
        ClassName = className;
        Items = items;
        Validators = validators;
    }
}
