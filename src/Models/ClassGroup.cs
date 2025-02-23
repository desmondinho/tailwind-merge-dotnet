namespace TailwindMerge.Models;

/// <summary>
/// Represents a group of CSS classes with associated metadata for the TailwindMerge utility.
/// </summary>
/// <param name="baseClassName">The base class name for the group.</param>
/// <param name="definitions">An array of definitions associated with the class group.</param>
public readonly struct ClassGroup( string? baseClassName, object[] definitions )
{
    /// <summary>
    /// Gets the base class name for the group.
    /// </summary>
    public string? BaseClassName { get; } = baseClassName;

    /// <summary>
    /// Gets an array of definitions associated with the class group.
    /// </summary>
    public object[] Definitions { get; } = definitions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassGroup"/> struct with the specified <paramref name="definitions"/>.
    /// </summary>
    /// <param name="definitions">An array of definitions associated with the class group.</param>
    public ClassGroup( object[] definitions )
        : this( null, definitions ) { }
}