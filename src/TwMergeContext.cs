using System.Text.RegularExpressions;

using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

internal partial class TwMergeContext
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

        return GetClassGroupIdRecursive( classNameParts, _classMap ) ?? GetGroupIdForArbitraryProperty( className );
    }

    internal string[]? GetConflictingClassGroupIds( string classGroupId, bool hasPostfixModifier )
    {
        var conflicts = _config.ConflictingClassGroups.GetValueOrDefault( classGroupId );

        if( hasPostfixModifier && _config.ConflictingClassGroupModifiers.ContainsKey( classGroupId ) )
        {
            return [
                .. conflicts,
                .. _config.ConflictingClassGroupModifiers[classGroupId]
            ];
        }

        return conflicts;
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

        var sortedModifiers = new List<string>();
        var unsortedModifiers = new List<string>();

        foreach( var modifier in modifiers )
        {
            if( modifier.StartsWith( '[' ) )
            {
                // Sort the unsorted modifiers and append to result
                if( unsortedModifiers.Count > 0 )
                {
                    unsortedModifiers.Sort();
                    sortedModifiers.AddRange( unsortedModifiers );
                    unsortedModifiers.Clear();
                }

                // Append the arbitrary variant directly to maintain position
                sortedModifiers.Add( modifier );
            }
            else
            {
                // Collect regular modifiers
                unsortedModifiers.Add( modifier );
            }
        }

        if( unsortedModifiers.Count > 0 )
        {
            unsortedModifiers.Sort();
            sortedModifiers.AddRange( unsortedModifiers );
        }

        return sortedModifiers;
    }

    private string? GetClassGroupIdRecursive( string[] classNameParts, ClassNameNode node )
    {
        if( classNameParts.Length == 0 )
        {
            return node.ClassGroupId;
        }

        var currentClassNamePart = classNameParts[0];

        if( node.Next is not null && node.Next.TryGetValue( currentClassNamePart, out var nextNode ) )
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

    private string? GetGroupIdForArbitraryProperty( string className )
    {
        var match = ArbitraryPropertyRegex().Match( className );
        if( match.Success )
        {
            var arbitraryPropertyClassName = match.Groups[1].Value;
            if( !string.IsNullOrEmpty( arbitraryPropertyClassName ) )
            {
                var property = arbitraryPropertyClassName[..arbitraryPropertyClassName.IndexOf( ':' )];
                return "arbitrary.." + property;
            }
        }

        return null;
    }

    [GeneratedRegex( @"^\[(.+)\]$" )]
    private static partial Regex ArbitraryPropertyRegex();
}
