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

    /// <summary>
    /// Gets or sets the class group IDs which should be resolved again with their postfix modifier attached.
    /// </summary>
    /// <remarks>
    /// This is needed when a slash can make the full class name belong to a different class group than the part before the slash.
    /// </remarks>
    public string[]? PostfixLookupClassGroups { get; set; }

    /// <summary>
    /// Gets or sets the modifiers whose order among multiple modifiers is preserved
    /// because their order changes which element gets targeted.
    /// </summary>
    public string[]? OrderSensitiveModifiers { get; set; }
}
