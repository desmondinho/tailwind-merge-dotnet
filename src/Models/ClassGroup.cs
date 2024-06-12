namespace TailwindMerge.Models;

/// <summary>
/// Represents a group of classes with associated metadata for the <see cref="TwMergeConfig"/>.
/// </summary>
/// <param name="Id">The unique identifier for the class group.</param>
/// <param name="BaseClassName">The base class name of the class group.</param>
/// <param name="ClassNameParts">An array of class name parts belonging to the group.</param>
/// <param name="Validators">An array of validation functions for the class names.</param>
public readonly record struct ClassGroup(
    string Id,
    string? BaseClassName,
    string[]? ClassNameParts,
    Func<string, bool>[]? Validators )
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClassGroup"/> struct 
    /// with the specified <paramref name="id"/> and <paramref name="classNameParts"/>.
    /// </summary>
    /// <param name="id">The unique identifier for the class group.</param>
    /// <param name="classNameParts">An array of class name parts belonging to the group.</param>
    public ClassGroup( string id, string[] classNameParts )
        : this( id, null, classNameParts, null ) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassGroup"/> struct 
    /// with the specified <paramref name="id"/>, <paramref name="baseClassName"/>, and <paramref name="classNameParts"/>.
    /// </summary>
    /// <param name="id">The unique identifier for the class group.</param>
    /// <param name="baseClassName">The name of the class group.</param>
    /// <param name="classNameParts">An array of class name parts belonging to the group.</param>
    public ClassGroup( string id, string baseClassName, string[] classNameParts )
        : this( id, baseClassName, classNameParts, null ) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassGroup"/> struct 
    /// with the specified <paramref name="id"/>, <paramref name="baseClassName"/>, and <paramref name="validators"/>.
    /// </summary>
    /// <param name="id">The unique identifier for the class group.</param>
    /// <param name="baseClassName">The name of the class group.</param>
    /// <param name="validators">An array of validation functions for the class names.</param>
    public ClassGroup( string id, string baseClassName, Func<string, bool>[] validators )
        : this( id, baseClassName, null, validators ) { }
}
