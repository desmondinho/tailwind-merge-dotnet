namespace TailwindMerge.Models;

internal readonly struct ClassInfo
{
    internal string Name { get; }
    internal string? GroupId { get; }
    internal bool IsTailwindClass { get; }

    internal ClassInfo( bool isTailwindClass, string name )
        : this( isTailwindClass, null, name ) { }

    internal ClassInfo( bool isTailwindClass, string? groupId, string name )
    {
        IsTailwindClass = isTailwindClass;
        GroupId = groupId;
        Name = name;
    }
}
