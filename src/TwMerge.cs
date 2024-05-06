using System.Text.RegularExpressions;

using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

/// <summary>
/// A utility for merging conflicting Tailwind CSS classes.
/// </summary>
public partial class TwMerge
{
    private readonly TwMergeConfig _config;
    private readonly TwMergeContext _context;

    /// <summary>
    /// Initializes a new instance of <see cref="TwMerge" />.
    /// </summary>
    public TwMerge()
    {
        _config = TwMergeConfig.Default();
        _context = new TwMergeContext( _config );
    }

    /// <summary>
    /// Merges the given <paramref name="classNames"/>, resolving any conflicts if present.
    /// </summary>
    /// <param name="classNames">The collection of CSS classes to be merged.</param>
    /// <returns>A <see langword="string"/> of merged CSS classes.</returns>
    public string Merge( params string[] classNames )
    {
        var joinedClassNames = string.Join( ' ', classNames );

        return Merge( joinedClassNames );
    }

    private string Merge( string classList )
    {
        var classes = SeparatorRegex()
            .Split( classList.Trim() )
            .Select( GetClassInfo );

        var filteredClassNames = FilterConflictingClasses( classes );

        return string.Join( " ", filteredClassNames );
    }

    private ClassInfo GetClassInfo( string className )
    {
        var modifiersInfo = _context.SplitModifiers( className );
        var hasPostfixModifier = modifiersInfo.PostfixModifierPosition.HasValue;

        var baseClassName = hasPostfixModifier
            ? modifiersInfo.BaseClassName[..modifiersInfo.PostfixModifierPosition!.Value]
            : modifiersInfo.BaseClassName;

        var classGroupId = _context.GetClassGroupId( baseClassName );

        if( string.IsNullOrEmpty( classGroupId ) )
        {
            if( !hasPostfixModifier )
            {
                return new ClassInfo( className, isTailwindClass: false );
            }

            classGroupId = _context.GetClassGroupId( modifiersInfo.BaseClassName );

            if( string.IsNullOrEmpty( classGroupId ) )
            {
                return new ClassInfo( className, isTailwindClass: false );
            }

            hasPostfixModifier = false;
        }

        var variantModifier = string.Join( ':', _context.SortModifiers( modifiersInfo.Modifiers ) );
        var modifierId = modifiersInfo.HasImportantModifier
            ? variantModifier + Constants.ImportantModifier
            : variantModifier;

        return new ClassInfo( 
            className, 
            classGroupId, 
            modifierId, 
            isTailwindClass: true, 
            hasPostfixModifier 
        );
    }

    private IEnumerable<string> FilterConflictingClasses( IEnumerable<ClassInfo> classes )
    {
        var conflictingClassGroups = new HashSet<string>();

        return classes
            .Reverse()
            // Last class in conflict wins, so we need to filter conflicting classes in reverse order.
            .Where( c =>
            {
                if( !c.IsTailwindClass )
                {
                    return true;
                }

                var classGroupId = c.ModifierId + c.GroupId;
                if( conflictingClassGroups.Contains( classGroupId ) )
                {
                    return false;
                }

                _ = conflictingClassGroups.Add( classGroupId );
                
                // TODO: Handle conflicting class groups

                return true;
            } )
            .Reverse()
            .Select( c => c.Name );
    }

    [GeneratedRegex( @"\s+" )]
    private static partial Regex SeparatorRegex();
}
