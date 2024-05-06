namespace TailwindMerge.Models;

internal readonly struct ClassModifiersInfo
{
    internal string BaseClassName { get; }
    internal bool HasImportantModifier { get; }
    internal int? PostfixModifierPosition { get; }
    internal IReadOnlyList<string> Modifiers { get; }

    internal ClassModifiersInfo(
        string baseClassName,
        bool hasImportantModifier,
        int? postfixModifierPosition,
        IReadOnlyList<string> modifiers )
    {
        BaseClassName = baseClassName;
        HasImportantModifier = hasImportantModifier;
        PostfixModifierPosition = postfixModifierPosition;
        Modifiers = modifiers;
    }
}
