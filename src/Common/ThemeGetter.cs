namespace TailwindMerge.Common;

/// <summary>
/// Represents a method that retrieves an array of objects from a theme based on a specified key.
/// </summary>
/// <param name="theme">The theme to retrieve values from.</param>
/// <returns>An array of objects associated with the specified key in the theme.</returns>
public delegate object[] ThemeGetter( Dictionary<string, object[]> theme );

/// <summary>
/// Provides utility method for working with theme configurations.
/// </summary>
public static class ThemeUtility
{
    /// <summary>
    /// Creates a <see cref="ThemeGetter"/> delegate that retrieves values from a theme based on the specified key.
    /// </summary>
    /// <param name="key">The key to use for retrieving values from the theme.</param>
    /// <returns>A <see cref="ThemeGetter"/>.</returns>
    public static ThemeGetter FromTheme( string key )
    {
        ThemeGetter themeGetter = ( theme ) =>
        {
            if( theme.TryGetValue( key, out var value ) )
            {
                return value;
            }

            return [];
        };

        return themeGetter;
    }
}

