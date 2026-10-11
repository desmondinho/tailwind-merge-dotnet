namespace TailwindMerge.Common;

/// <summary>
/// Represents a reference to a theme scale that is resolved when the class map is built.
/// </summary>
/// <param name="Key">The key of the theme scale.</param>
public sealed record ThemeReference( string Key );

/// <summary>
/// Provides utility method for working with theme configurations.
/// </summary>
public static class ThemeUtility
{
    /// <summary>
    /// Creates a <see cref="ThemeReference"/> that references the theme scale with the specified key.
    /// </summary>
    /// <param name="key">The key of the theme scale.</param>
    /// <returns>A <see cref="ThemeReference"/>.</returns>
    public static ThemeReference FromTheme( string key ) => new( key );
}
