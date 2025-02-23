using System.Globalization;
using System.Text.RegularExpressions;

namespace TailwindMerge.Common;

/// <summary>
/// Provides a set of validation functions for different class name parts.
/// </summary>
public static partial class Validators
{
	private static readonly HashSet<string> _imageLabels = ["image", "url"];
	private static readonly HashSet<string> _sizeLabels = ["length", "size", "percentage"];

	/// <summary>
	/// Always returns true, indicating that any value is valid.
	/// </summary>
	public static Func<string, bool> IsAny { get; } = _ => true;

	/// <summary>
	/// Validates whether a string represents a fraction.
	/// </summary>
	public static Func<string, bool> IsFraction { get; } = value
		=> FractionRegex().IsMatch( value );

	/// <summary>
	/// Validates whether a string represents a number.
	/// </summary>
	public static Func<string, bool> IsNumber { get; } = value
		=> !string.IsNullOrEmpty( value ) && double.TryParse( value, CultureInfo.InvariantCulture, out _ );

	/// <summary>
	/// Validates whether a string represents an integer.
	/// </summary>
	public static Func<string, bool> IsInteger { get; } = value
		=> !string.IsNullOrEmpty( value ) && int.TryParse( value, out _ );

	/// <summary>
	/// Validates whether a string represents a percentage.
	/// </summary>
	public static Func<string, bool> IsPercent { get; } = value
		=> value.EndsWith( '%' ) && IsNumber( value[..^1] );

	/// <summary>
	/// Validates whether a string represents a t-shirt size value.
	/// </summary>
	public static Func<string, bool> IsTshirtSize { get; } = value
		=> TshirtUnitRegex().IsMatch( value );

	/// <summary>
	/// Validates whether a string represents an arbitrary value.
	/// </summary>
	public static Func<string, bool> IsArbitraryValue { get; } = value
		=> ArbitraryValueRegex().IsMatch( value );

	/// <summary>
	/// Validates whether a string represents an arbitrary variable.
	/// </summary>
	public static Func<string, bool> IsArbitraryVariable { get; } = value
		=> ArbitraryVariableRegex().IsMatch( value );

	/// <summary>
	/// Validates whether a string represents an arbitrary variable length value.
	/// </summary>
	public static Func<string, bool> IsArbitraryVariableLength { get; } = value
		=> GetIsArbitraryVariable( value, IsLabelLength );

	/// <summary>
	/// Validates whether a string represents an arbitrary variable family name value.
	/// </summary>
	public static Func<string, bool> IsArbitraryVariableFamilyName { get; } = value
		=> GetIsArbitraryVariable( value, IsLabelFamilyName );

	/// <summary>
	/// Validates whether a string represents an arbitrary variable position value.
	/// </summary>
	public static Func<string, bool> IsArbitraryVariablePosition { get; } = value
		=> GetIsArbitraryVariable( value, IsLabelPosition );

	/// <summary>
	/// Validates whether a string represents an arbitrary variable size value.
	/// </summary>
	public static Func<string, bool> IsArbitraryVariableSize { get; } = value
		=> GetIsArbitraryVariable( value, IsLabelSize );

	/// <summary>
	/// Validates whether a string represents an arbitrary variable image value.
	/// </summary>
	public static Func<string, bool> IsArbitraryVariableImage { get; } = value
		=> GetIsArbitraryVariable( value, IsLabelImage );

	/// <summary>
	/// Validates whether a string represents an arbitrary variable shadow value.
	/// </summary>
	public static Func<string, bool> IsArbitraryVariableShadow { get; } = value
		=> GetIsArbitraryVariable( value, IsLabelShadow, true );

	/// <summary>
	/// Validates whether a string represents an arbitrary size.
	/// </summary>
	public static Func<string, bool> IsArbitrarySize { get; } = value
		=> GetIsArbitraryValue( value, IsLabelSize, IsNever );

	/// <summary>
	/// Validates whether a string represents an arbitrary length value.
	/// </summary>
	public static Func<string, bool> IsArbitraryLength { get; } = value
		=> GetIsArbitraryValue( value, IsLabelLength, IsLengthOnly );

	/// <summary>
	/// Validates whether a string represents an arbitrary number.
	/// </summary>
	public static Func<string, bool> IsArbitraryNumber { get; } = value
		=> GetIsArbitraryValue( value, IsLabelNumber, IsNumber );

	/// <summary>
	/// Validates whether a string represents an arbitrary position.
	/// </summary>
	public static Func<string, bool> IsArbitraryPosition { get; } = value
		=> GetIsArbitraryValue( value, IsLabelPosition, IsNever );

	/// <summary>
	/// Validates whether a string represents an arbitrary image value.
	/// </summary>
	public static Func<string, bool> IsArbitraryImage { get; } = value
		=> GetIsArbitraryValue( value, IsLabelImage, IsImage );

	/// <summary>
	/// Validates whether a string represents an arbitrary shadow value.
	/// </summary>
	public static Func<string, bool> IsArbitraryShadow { get; } = value
		=> GetIsArbitraryValue( value, IsNever, IsShadow );

	/// <summary>
	/// Validates whether a string is not an arbitrary value or variable.
	/// </summary>
	public static Func<string, bool> IsAnyNonArbitrary { get; } = value
		=> !IsArbitraryValue( value ) && !IsArbitraryVariable( value );

	private static bool GetIsArbitraryValue(
		string value,
		Func<string, bool> testLabel,
		Func<string, bool> testValue )
	{
		var match = ArbitraryValueRegex().Match( value );
		if( match.Success )
		{
			if( !string.IsNullOrEmpty( match.Groups[1].Value ) )
			{
				return testLabel( match.Groups[1].Value );
			}

			return testValue( match.Groups[2].Value );
		}

		return false;
	}

	private static bool GetIsArbitraryVariable(
		string value,
		Func<string, bool> testLabel,
		bool shouldMatchNoLabel = false )
	{
		var match = ArbitraryVariableRegex().Match( value );
		if( match.Success )
		{
			if( !string.IsNullOrEmpty( match.Groups[1].Value ) )
			{
				return testLabel( match.Groups[1].Value );
			}

			return shouldMatchNoLabel;
		}

		return false;
	}

	private static bool IsLengthOnly( string value )
		=> LengthUnitRegex().IsMatch( value ) && !ColorFunctionRegex().IsMatch( value );
	private static bool IsNever( string _ ) => false;
	private static bool IsShadow( string value ) => ShadowRegex().IsMatch( value );
	private static bool IsImage( string value ) => ImageRegex().IsMatch( value );

	private static bool IsLabelLength( string label ) => label == "length";
	private static bool IsLabelNumber( string label ) => label == "number";
	private static bool IsLabelShadow( string label ) => label == "shadow";
	private static bool IsLabelPosition( string label ) => label == "position";
	private static bool IsLabelFamilyName( string label ) => label == "family-name";
	private static bool IsLabelSize( string label ) => _sizeLabels.Contains( label );
	private static bool IsLabelImage( string label ) => _imageLabels.Contains( label );

	[GeneratedRegex( @"^\[(?:(\w[\w-]*):)?(.+)\]$", RegexOptions.IgnoreCase )]
	private static partial Regex ArbitraryValueRegex();

	[GeneratedRegex( @"^\((?:(\w[\w-]*):)?(.+)\)$", RegexOptions.IgnoreCase )]
	private static partial Regex ArbitraryVariableRegex();

	[GeneratedRegex( @"^\d+\/\d+$" )]
	private static partial Regex FractionRegex();

	[GeneratedRegex( @"^(\d+(\.\d+)?)?(xs|sm|md|lg|xl)$" )]
	private static partial Regex TshirtUnitRegex();

	[GeneratedRegex( @"\d+(%|px|r?em|[sdl]?v([hwib]|min|max)|pt|pc|in|cm|mm|cap|ch|ex|r?lh|cq(w|h|i|b|min|max))|\b(calc|min|max|clamp)\(.+\)|^0$" )]
	private static partial Regex LengthUnitRegex();

	[GeneratedRegex( @"^(rgba?|hsla?|hwb|(ok)?(lab|lch))\(.+\)$" )]
	private static partial Regex ColorFunctionRegex();

	[GeneratedRegex( @"^(inset_)?-?((\d+)?\.?(\d+)[a-z]+|0)_-?((\d+)?\.?(\d+)[a-z]+|0)" )]
	private static partial Regex ShadowRegex();

	[GeneratedRegex( @"^(url|image|image-set|cross-fade|element|(repeating-)?(linear|radial|conic)-gradient)\(.+\)$" )]
	private static partial Regex ImageRegex();
}
