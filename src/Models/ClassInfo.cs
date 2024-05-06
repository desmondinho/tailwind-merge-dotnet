namespace TailwindMerge.Models;

internal readonly struct ClassInfo
{
    internal string Name { get; }
    internal string? GroupId { get; }
    internal string? ModifierId { get; }
    internal bool IsTailwindClass { get; }
    internal bool HasPostfixModifier { get; }

    internal ClassInfo( string name, bool isTailwindClass )
        : this( name, null, null, isTailwindClass, false ) { }

    internal ClassInfo( 
        string name, 
        string? groupId, 
        string? modifierId, 
        bool isTailwindClass,
        bool hasPostfixModifier )
    {
        Name = name;
        GroupId = groupId;
        ModifierId = modifierId;
        IsTailwindClass = isTailwindClass;
        HasPostfixModifier = hasPostfixModifier;
    }
}
