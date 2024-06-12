using System.Globalization;
using System.Text.RegularExpressions;

namespace TailwindMerge.Common;

/// <summary>
/// Provides a set of validation functions for different class name parts.
/// </summary>
public static partial class Validators
{
    private static readonly HashSet<string> _stringLengths = ["px", "full", "screen"];
    private static readonly HashSet<string> _sizeLabels = ["length", "size", "percentage"];
    private static readonly HashSet<string> _imageLabels = ["image", "url"];

    /// <summary>
    /// Validates whether a string represents a length value.
    /// </summary>
    public static bool IsLength( string value )
    {
        return IsNumber( value ) || _stringLengths.Contains( value ) || FractionRegex().IsMatch( value );
    }

    /// <summary>
    /// Validates whether a string represents an arbitrary length value.
    /// </summary>
    public static bool IsArbitraryLength( string value )
    {
        return GetIsArbitraryValue( value, "length", IsLengthOnly );
    }

    /// <summary>
    /// Validates whether a string represents a number.
    /// </summary>
    public static bool IsNumber( string value )
    {
        return !string.IsNullOrEmpty( value ) && double.TryParse( value, CultureInfo.InvariantCulture, out _ );
    }

    /// <summary>
    /// Validates whether a string represents an integer.
    /// </summary>
    public static bool IsInteger( string value )
    {
        return !string.IsNullOrEmpty( value ) && int.TryParse( value, out _ );
    }

    /// <summary>
    /// Validates whether a string represents a percentage.
    /// </summary>
    public static bool IsPercent( string value )
    {
        return value.EndsWith( '%' ) && IsNumber( value[..^1] );
    }

    /// <summary>
    /// Validates whether a string represents an arbitrary number.
    /// </summary>
    public static bool IsArbitraryNumber( string value )
    {
        return GetIsArbitraryValue( value, "number", IsNumber );
    }

    /// <summary>
    /// Validates whether a string represents a t-shirt size value.
    /// </summary>
    public static bool IsTshirtSize( string value )
    {
        return TshirtUnitRegex().IsMatch( value );
    }

    /// <summary>
    /// Validates whether a string represents an arbitrary value.
    /// </summary>
    public static bool IsArbitraryValue( string value )
    {
        return ArbitraryValueRegex().IsMatch( value );
    }

    /// <summary>
    /// Validates whether a string represents an arbitrary size.
    /// </summary>
    public static bool IsArbitrarySize( string value )
    {
        return GetIsArbitraryValue( value, _sizeLabels, IsNever );
    }

    /// <summary>
    /// Validates whether a string represents an arbitrary position.
    /// </summary>
    public static bool IsArbitraryPosition( string value )
    {
        return GetIsArbitraryValue( value, "position", IsNever );
    }

    /// <summary>
    /// Validates whether a string represents an arbitrary image value.
    /// </summary>
    public static bool IsArbitraryImage( string value )
    {
        return GetIsArbitraryValue( value, _imageLabels, IsImage );
    }

    /// <summary>
    /// Validates whether a string represents an arbitrary shadow value.
    /// </summary>
    public static bool IsArbitraryShadow( string value )
    {
        return GetIsArbitraryValue( value, string.Empty, IsShadow );
    }

    /// <summary>
    /// Always returns true, indicating that any value is valid.
    /// </summary>
    public static bool IsAny( string value ) => true;

    private static bool GetIsArbitraryValue( string value, object label, Func<string, bool> testValue )
    {
        var match = ArbitraryValueRegex().Match( value );

        if( match.Success )
        {
            if( !string.IsNullOrEmpty( match.Groups[1].Value ) )
            {
                if( label is string str )
                {
                    return match.Groups[1].Value == str;
                }

                if( label is HashSet<string> set )
                {
                    return set.Contains( match.Groups[1].Value );
                }
            }

            return testValue( match.Groups[2].Value );
        }

        return false;
    }

    private static bool IsLengthOnly( string value )
    {
        return LengthUnitRegex().IsMatch( value ) && !ColorFunctionRegex().IsMatch( value );
    }

    private static bool IsNever( string value )
    {
        return false;
    }

    private static bool IsShadow( string value )
    {
        return ShadowRegex().IsMatch( value );
    }

    private static bool IsImage( string value )
    {
        return ImageRegex().IsMatch( value );
    }

    [GeneratedRegex( @"^\[(?:([a-z-]+):)?(.+)\]$", RegexOptions.IgnoreCase )]
    private static partial Regex ArbitraryValueRegex();

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
