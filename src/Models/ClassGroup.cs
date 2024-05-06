namespace TailwindMerge.Models;

internal readonly struct ClassGroup(
    string id,
    string? className,
    string[]? items,
    Func<string, bool>[]? validators )
{
    internal string Id { get; } = id;
    internal string? ClassName { get; } = className;
    internal string[]? Items { get; } = items;
    internal Func<string, bool>[]? Validators { get; } = validators;

    internal ClassGroup( string id, string[] items )
        : this( id, null, items, null ) { }

    internal ClassGroup( string id, string className, string[] items )
        : this( id, className, items, null ) { }

    internal ClassGroup( string id, string className, Func<string, bool>[] validators ) 
        : this( id, className, null, validators ) { }
}
