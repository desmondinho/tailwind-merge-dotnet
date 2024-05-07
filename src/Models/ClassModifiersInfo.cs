namespace TailwindMerge.Models;

internal readonly record struct ClassModifiersInfo( 
    string BaseClassName,
    bool HasImportantModifier,
    int? PostfixModifierPosition,
    IReadOnlyList<string> Modifiers );
