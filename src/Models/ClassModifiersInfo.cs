namespace TailwindMerge.Models;

internal readonly record struct ClassModifiersInfo( 
	ICollection<string> Modifiers,
    bool HasImportantModifier,
	string BaseClassName,
    int? PostfixModifierPosition,
	bool? IsExternal = null
);
