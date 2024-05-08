using System.Globalization;
using System.Text.RegularExpressions;

namespace TailwindMerge.Common;

internal static partial class Validators
{
    private static HashSet<string> _stringLengths = ["px", "full", "screen"];
    private static HashSet<string> _sizeLabels = ["length", "size", "percentage"];
    private static HashSet<string> _imageLabels = ["image", "url"];

    internal static Func<string, bool> IsLength = ( value ) =>
    {
        return IsNumber!( value ) || _stringLengths.Contains( value ) || FractionRegex().IsMatch( value );
    };

    internal static Func<string, bool> IsArbitraryLength = ( value ) =>
    {
        return GetIsArbitraryValue( value, "length", IsLengthOnly );
    };

    internal static Func<string, bool> IsNumber = ( value ) =>
    {
        return !string.IsNullOrEmpty( value ) && double.TryParse( value, CultureInfo.InvariantCulture, out _ );
    };

    internal static Func<string, bool> IsInteger = ( value ) =>
    {
        return !string.IsNullOrEmpty( value ) && int.TryParse( value, out _ );
    };

    internal static Func<string, bool> IsPercent = ( value ) =>
    {
        return value.EndsWith( '%' ) && IsNumber( value[..^1] );
    };

    internal static Func<string, bool> IsArbitraryNumber = ( value ) =>
    {
        return GetIsArbitraryValue( value, "number", IsNumber );
    };

    internal static Func<string, bool> IsTshirtSize = ( value ) =>
    {
        return TshirtUnitRegex().IsMatch( value );
    };

    internal static Func<string, bool> IsArbitraryValue = ( value ) =>
    {
        return ArbitraryValueRegex().IsMatch( value );
    };

    internal static Func<string, bool> IsArbitrarySize = ( value ) =>
    {
        return GetIsArbitraryValue( value, _sizeLabels, IsNever );
    };

    internal static Func<string, bool> IsArbitraryPosition = ( value ) =>
    {
        return GetIsArbitraryValue( value, "position", IsNever );
    };

    internal static Func<string, bool> IsArbitraryImage = ( value ) =>
    {
        return GetIsArbitraryValue( value, _imageLabels, IsImage );
    };

    internal static Func<string, bool> IsArbitraryShadow = ( value ) =>
    {
        return GetIsArbitraryValue( value, string.Empty, IsShadow );
    };

    internal static Func<string, bool> IsAny = ( _ ) => true;

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
