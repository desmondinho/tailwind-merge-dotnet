namespace TailwindMerge.Models;

/// <summary>
/// Represents the extended configuration for the TailwindMerge utility.
/// </summary>
public class ExtendedConfig
{
    /// <summary>
    /// Gets or sets the theme for the configuration extension.
    /// </summary>
    public Dictionary<string, object[]>? Theme { get; set; }

    /// <summary>
    /// Gets or sets the class groups for the configuration extension.
    /// </summary>
    public Dictionary<string, ClassGroup>? ClassGroups { get; set; }

    /// <summary>
    /// Gets or sets the conflicting class groups for the configuration extension.
    /// </summary>
    public Dictionary<string, string[]>? ConflictingClassGroups { get; set; }

    /// <summary>
    /// Gets or sets the conflicting class group modifiers for the configuration extension.
    /// </summary>
    public Dictionary<string, string[]>? ConflictingClassGroupModifiers { get; set; }
}
