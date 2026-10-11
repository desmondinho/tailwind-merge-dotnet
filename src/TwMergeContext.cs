using System.Text.RegularExpressions;

using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

internal partial class TwMergeContext
{
	private readonly ClassNameNode _classMap;
	private readonly string? _prefix;
	private readonly Dictionary<string, string[]> _conflictingClassGroups;
	private readonly Dictionary<string, string[]> _conflictingClassGroupModifiers;
	private readonly HashSet<string> _postfixLookupClassGroups;
	private readonly HashSet<string> _orderSensitiveModifiers;

	// Everything needed for merging is copied from the configuration,
	// so changes made to it afterwards have no effect
	internal TwMergeContext( TwMergeConfig config )
	{
		_classMap = TwMergeMapFactory.Create( config );
		_prefix = config.Prefix;
		_conflictingClassGroups = new( config.ConflictingClassGroups );
		_conflictingClassGroupModifiers = new( config.ConflictingClassGroupModifiers );
		_postfixLookupClassGroups = [.. config.PostfixLookupClassGroups];
		_orderSensitiveModifiers = [.. config.OrderSensitiveModifiers];
	}

	internal bool IsPostfixLookupClassGroup( string classGroupId )
		=> _postfixLookupClassGroups.Contains( classGroupId );

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
		var conflicts = _conflictingClassGroups.GetValueOrDefault( classGroupId );

		if( hasPostfixModifier &&
			_conflictingClassGroupModifiers.TryGetValue( classGroupId, out var conflictingModifiers ) )
		{
			return [
				.. conflicts,
				.. conflictingModifiers];
		}

		return conflicts;
	}

	// TODO: Refactor
	internal ClassModifiersInfo SplitModifiers( string className )
	{
		Func<string, ClassModifiersInfo> parseClassName = ( string className ) =>
		{
			var modifiers = new List<string>();
			var bracketDepth = 0;
			var parentDepth = 0;
			var modifierStart = 0;
			var postfixModifierPosition = default( int? );

			for( var i = 0; i < className.Length; i++ )
			{
				var currChar = className[i];

				if( bracketDepth == 0 && parentDepth == 0 )
				{
					if( currChar == Constants.ModifierSeparator )
					{
						modifiers.Add( className[modifierStart..i] );
						modifierStart = i + Constants.ModifierSeparatorLength;
						continue;
					}

					if( currChar == '/' )
					{
						postfixModifierPosition = i;
						continue;
					}
				}

				switch( currChar )
				{
					case '[':
						bracketDepth++;
						break;
					case ']':
						bracketDepth--;
						break;
					case '(':
						parentDepth++;
						break;
					case ')':
						parentDepth--;
						break;
				}
			}

			var baseClassNameWithImportantModifier =
				modifiers.Count == 0 ? className : className[modifierStart..];
			var baseClassName = StripImportantModifier( baseClassNameWithImportantModifier );
			var hasImportantModifier = baseClassName != baseClassNameWithImportantModifier;

			postfixModifierPosition = postfixModifierPosition > modifierStart
				? postfixModifierPosition - modifierStart
				: null;

			return new ClassModifiersInfo(
				modifiers,
				hasImportantModifier,
				baseClassName,
				postfixModifierPosition
			);
		};

		if( !string.IsNullOrEmpty( _prefix ) )
		{
			var fullPrefix = _prefix + Constants.ModifierSeparator;
			var parseClassNameOriginal = parseClassName;

			parseClassName = ( className ) =>
			{
				if( className.StartsWith( fullPrefix ) )
				{
					return parseClassNameOriginal( className[fullPrefix.Length..] );
				}
				else
				{
					return new ClassModifiersInfo(
						Modifiers: [],
						HasImportantModifier: false,
						BaseClassName: className,
						PostfixModifierPosition: null,
						IsExternal: true
					);
				}
			};
		}

		// Skipping 'experimentalParseClassName' for the time being.
		// https://github.com/dcastil/tailwind-merge/blob/main/src/lib/parse-class-name.ts#L83

		return parseClassName( className );
	}

	internal ICollection<string> SortModifiers( ICollection<string> modifiers )
	{
		if( modifiers.Count <= 1 )
		{
			return modifiers;
		}

		var sortedModifiers = new List<string>();
		var unsortedModifiers = new List<string>();

		foreach( var modifier in modifiers )
		{
			var isPositionSensitive = modifier.StartsWith( '[' ) || _orderSensitiveModifiers.Contains( modifier );

			if( isPositionSensitive )
			{
				if( unsortedModifiers.Count > 0 )
				{
					unsortedModifiers.Sort();
					sortedModifiers.AddRange( unsortedModifiers );
					unsortedModifiers.Clear();
				}

				sortedModifiers.Add( modifier );
			}
			else
			{
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

	private static string? GetClassGroupIdRecursive( string[] classNameParts, ClassNameNode node )
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

	private static string? GetGroupIdForArbitraryProperty( string className )
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

	private static string StripImportantModifier( string className )
	{
		if( className.EndsWith( Constants.ImportantModifier ) )
		{
			return className[..^1];
		}

		/*
         * In Tailwind CSS v3 the important modifier was at the start of the base class name. 
         * This is still supported for legacy reasons.
         * See https://github.com/dcastil/tailwind-merge/issues/513#issuecomment-2614029864
         */
		if( className.StartsWith( Constants.ImportantModifier ) )
		{
			return className[1..];
		}

		return className;
	}

	[GeneratedRegex( @"^\[(.+)\]$" )]
	private static partial Regex ArbitraryPropertyRegex();
}
