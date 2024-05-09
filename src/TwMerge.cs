using System.Text.RegularExpressions;

using LruCacheNet;

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
    private readonly LruCache<string, string> _cache;

    /// <summary>
    /// Initializes a new instance of <see cref="TwMerge" />.
    /// </summary>
    public TwMerge()
    {
        _config = TwMergeConfig.Default();
        _context = new TwMergeContext( _config );
        _cache = new LruCache<string, string>( _config.CacheSize );
    }

    /// <summary>
    /// Merges the given <paramref name="classNames"/>, resolving any conflicts if present.
    /// </summary>
    /// <param name="classNames">The collection of CSS classes to be merged.</param>
    /// <returns>A <see langword="string"/> of merged CSS classes.</returns>
    public string Merge( params string[] classNames )
    {
        var joinedClassNames = string.Join( ' ', classNames );

        if( _cache.TryGetValue( joinedClassNames, out var cachedResult ) )
        {
            return cachedResult;
        }

        var result = Merge( joinedClassNames );
        _cache.AddOrUpdate( joinedClassNames, result );

        return result;
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
        (var baseClassName,
            var hasImportantModifier,
            var postfixModifierPosition,
            var modifiers) = _context.SplitModifiers( className );

        var hasPostfixModifier = postfixModifierPosition.HasValue;

        var classGroupId = _context.GetClassGroupId( hasPostfixModifier
            ? baseClassName[..postfixModifierPosition!.Value]
            : baseClassName );

        if( string.IsNullOrEmpty( classGroupId ) )
        {
            if( !hasPostfixModifier )
            {
                return new ClassInfo( className, isTailwindClass: false );
            }

            classGroupId = _context.GetClassGroupId( baseClassName );

            if( string.IsNullOrEmpty( classGroupId ) )
            {
                return new ClassInfo( className, isTailwindClass: false );
            }

            hasPostfixModifier = false;
        }

        var variantModifier = string.Join( ':', _context.SortModifiers( modifiers ) );
        var modifierId = hasImportantModifier
            ? variantModifier + Constants.ImportantModifier
            : variantModifier;

        return new ClassInfo(
            className,
            classGroupId,
            modifierId,
            IsTailwindClass: true,
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

                var classGroups = _context.GetConflictingClassGroupIds( c.GroupId!, c.HasPostfixModifier );
                if( classGroups is { Length: > 0 } )
                {
                    foreach( var group in classGroups )
                    {
                        _ = conflictingClassGroups.Add( c.ModifierId + group );
                    }
                }

                return true;
            } )
            .Reverse()
            .Select( c => c.Name );
    }

    [GeneratedRegex( @"\s+" )]
    private static partial Regex SeparatorRegex();
}
