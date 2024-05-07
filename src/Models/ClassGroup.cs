namespace TailwindMerge.Models;

internal readonly record struct ClassGroup(
    string Id,
    string? ClassName,
    string[]? Items,
    Func<string, bool>[]? Validators
)
{
    internal ClassGroup( string id, string[] items )
        : this( id, null, items, null ) { }

    internal ClassGroup( string id, string className, string[] items )
        : this( id, className, items, null ) { }

    internal ClassGroup( string id, string className, Func<string, bool>[] validators )
        : this( id, className, null, validators ) { }
}
