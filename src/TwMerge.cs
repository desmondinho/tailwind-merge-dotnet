using System.Text.RegularExpressions;

using LruCacheNet;

using Microsoft.Extensions.Options;

using TailwindMerge.Common;

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
	/// Initializes a new instance of <see cref="TwMerge" /> with optional configuration options.
	/// </summary>
	/// <param name="options">The configuration options.</param>
	public TwMerge( IOptions<TwMergeConfig>? options = null )
	{
		_config = options?.Value ?? TwMergeConfig.Default();
		_context = new TwMergeContext( _config );
		_cache = new LruCache<string, string>( _config.CacheSize );
	}

	/// <summary>
	/// Merges the given <paramref name="classNames"/>, resolving any conflicts if present.
	/// </summary>
	/// <param name="classNames">The collection of CSS classes to be merged.</param>
	/// <returns>A <see langword="string"/> of merged CSS classes.</returns>
	public string? Merge( params string?[] classNames )
	{
		var joinedClassNames = string.Join( ' ', classNames );

		if( string.IsNullOrEmpty( joinedClassNames ) )
		{
			return null;
		}

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
		var classGroupsInConflict = new HashSet<string>();
		var classNames = ClassesSeparatorRegex().Split( classList.Trim() );

		var result = "";

		for( var i = classNames.Length - 1; i >= 0; i-- )
		{
			var originalClassName = classNames[i];

			(
				var modifiers,
				var hasImportantModifier,
				var baseClassName,
				var postfixModifierPosition,
				var isExternal
			) = _context.SplitModifiers( originalClassName );

			if( isExternal is true )
			{
				result = originalClassName + (result.Length > 0 ? ' ' + result : result);
				continue;
			}

			var hasPostfixModifier = postfixModifierPosition.HasValue;
			var classGroupId = _context.GetClassGroupId(
				hasPostfixModifier
					? baseClassName[..postfixModifierPosition!.Value]
					: baseClassName
			);

			if( string.IsNullOrEmpty( classGroupId ) )
			{
				if( !hasPostfixModifier )
				{
					// Not a Tailwind class
					result = originalClassName + (result.Length > 0 ? ' ' + result : result);
					continue;
				}

				classGroupId = _context.GetClassGroupId( baseClassName );

				if( string.IsNullOrEmpty( classGroupId ) )
				{
					// Not a Tailwind class
					result = originalClassName + (result.Length > 0 ? ' ' + result : result);
					continue;
				}

				hasPostfixModifier = false;
			}

			var variantModifier = string.Join( ':', _context.SortModifiers( modifiers ) );

			var modifierId = hasImportantModifier
				? variantModifier + Constants.ImportantModifier
				: variantModifier;

			var classId = modifierId + classGroupId;

			if( classGroupsInConflict.Contains( classId ) )
			{
				// Tailwind class omitted due to conflict
				continue;
			}

			classGroupsInConflict.Add( classId );

			var conflictingClassGroups = _context.GetConflictingClassGroupIds( classGroupId, hasPostfixModifier );
			if( conflictingClassGroups is { Length: > 0 } )
			{
				foreach( var classGroup in conflictingClassGroups )
				{
					classGroupsInConflict.Add( modifierId + classGroup );
				}
			}

			// Tailwind class not in conflict
			result = originalClassName + (result.Length > 0 ? ' ' + result : result);
		}

		return result;
	}

	[GeneratedRegex( @"\s+" )]
	private static partial Regex ClassesSeparatorRegex();
}
