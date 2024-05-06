using TailwindMerge.Common;

namespace TailwindMerge;

/// <summary>
/// A utility for merging conflicting Tailwind CSS classes.
/// </summary>
public class TwMerge
{
    private readonly TwMergeConfig _config;

    /// <summary>
    /// Initializes a new instance of <see cref="TwMerge" />.
    /// </summary>
    public TwMerge()
    {
        _config = TwMergeConfig.Default();
    }

    /// <summary>
    /// Merges the given <paramref name="classNames"/>, resolving any conflicts if present.
    /// </summary>
    /// <param name="classNames">The collection of CSS classes to be merged.</param>
    /// <returns>A <see langword="string"/> of merged CSS classes.</returns>
    public string Merge( params string[] classNames )
    {
        return "";
    }  
}
