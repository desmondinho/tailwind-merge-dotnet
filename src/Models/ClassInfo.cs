namespace TailwindMerge.Models;

internal readonly record struct ClassInfo(
    string Name,
    string? GroupId,
    string? ModifierId,
    bool IsTailwindClass,
    bool HasPostfixModifier )
{

    internal ClassInfo( string name, bool isTailwindClass )
        : this( name, null, null, isTailwindClass, false ) { }
}
