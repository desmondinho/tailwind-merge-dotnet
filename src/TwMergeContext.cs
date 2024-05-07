using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

internal class TwMergeContext
{
    private readonly TwMergeConfig _config;
    private readonly ClassNameNode _classMap;

    internal TwMergeContext( TwMergeConfig config )
    {
        _config = config;
        _classMap = TwMergeMapFactory.Create( config );
    }

    internal string? GetClassGroupId( string className )
    {
        var classNameParts = className.Split( Constants.ClassNameSeparator );

        // Classes like "-inset-1" produce an empty string as the first classNamePart.
        // We assume that classes for negative values are used correctly and remove it from classNameParts.
        if( classNameParts[0] == "" && classNameParts.Length != 1 )
        {
            classNameParts = classNameParts.Skip( 1 ).ToArray();
        }

        return GetClassGroupIdRecursive( classNameParts, _classMap );
    }

    // TODO: Refactor
    internal ClassModifiersInfo SplitModifiers( string className )
    {
        var separator = _config.Separator;
        var modifiers = new List<string>();
        var bracketDepth = 0;
        var modifierStart = 0;
        int? postfixModifierPosition = null;

        for( var i = 0; i < className.Length; i++ )
        {
            var currChar = className[i];

            if( bracketDepth == 0 )
            {
                if( currChar == separator[0] &&
                    ( separator.Length == 1 || className.Substring( i, separator.Length ) == separator ) )
                {
                    modifiers.Add( className[modifierStart..i] );
                    modifierStart = i + separator.Length;
                    continue;
                }

                if( currChar == '/' )
                {
                    postfixModifierPosition = i;
                    continue;
                }
            }

            if( currChar == '[' )
            {
                bracketDepth++;
            }
            else if( currChar == ']' )
            {
                bracketDepth--;
            }
        }

        var classNameWithImportantModifier = modifiers.Count == 0
            ? className
            : className[modifierStart..];

        var hasImportantModifier =
            classNameWithImportantModifier.StartsWith( Constants.ImportantModifier );

        var baseClassName = hasImportantModifier
            ? classNameWithImportantModifier[1..]
            : classNameWithImportantModifier;

        postfixModifierPosition = postfixModifierPosition > modifierStart
            ? postfixModifierPosition - modifierStart
            : null;

        return new ClassModifiersInfo(
            baseClassName,
            hasImportantModifier,
            postfixModifierPosition,
            modifiers
        );
    }

    internal IReadOnlyList<string> SortModifiers( IReadOnlyList<string> modifiers )
    {
        if( modifiers.Count <= 1 )
        {
            return modifiers;
        }

        var anyModifiers = new List<string>();
        var arbitraryModifiers = new List<string>();

        foreach( var modifier in modifiers )
        {
            if( modifier.StartsWith( '[' ) )
            {
                arbitraryModifiers.Add( modifier );
            }
            else
            {
                anyModifiers.Add( modifier );
            }
        }

        anyModifiers.Sort();
        anyModifiers.AddRange( arbitraryModifiers );

        return anyModifiers;
    }

    private string? GetClassGroupIdRecursive( string[] classNameParts, ClassNameNode node )
    {
        if( classNameParts.Length == 0 )
        {
            return node.ClassGroupId;
        }

        var currentClassNamePart = classNameParts[0];

        if( node.Next!.TryGetValue( currentClassNamePart, out var nextNode ) )
        {
            var classGroupId = GetClassGroupIdRecursive( classNameParts[1..], nextNode );

            if( !string.IsNullOrEmpty( classGroupId ) )
            {
                return classGroupId;
            }
        }

        if( node.Validators is { Count: 0 } )
        {
            return null;
        }

        var classNameRest = string.Join( Constants.ClassNameSeparator, classNameParts );

        return node.Validators?.FirstOrDefault( validator => validator.Validator( classNameRest ) ).ClassGroupId;
    }
}
