namespace TailwindMerge.Models;

/// <summary>
/// Represents a group of CSS classes with associated metadata for the TailwindMerge utility.
/// </summary>
/// <param name="id">The unique identifier for the class group.</param>
/// <param name="baseClassName">The base class name for the group.</param>
/// <param name="definitions">An array of definitions associated with the class group.</param>
public readonly struct ClassGroup( string id, string? baseClassName, object[] definitions )
{
    /// <summary>
    /// Gets the unique identifier for the class group.
    /// </summary>
    public string Id { get; } = id;

    /// <summary>
    /// Gets the base class name for the group.
    /// </summary>
    public string? BaseClassName { get; } = baseClassName;

    /// <summary>
    /// Gets an array of definitions associated with the class group.
    /// </summary>
    public object[] Definitions { get; } = definitions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassGroup"/> struct with the 
    /// specified <paramref name="id"/> and <paramref name="definitions"/>.
    /// </summary>
    /// <param name="id">The unique identifier for the class group.</param>
    /// <param name="definitions">An array of definitions associated with the class group.</param>
    public ClassGroup( string id, object[] definitions )
        : this( id, null, definitions ) { }
}
