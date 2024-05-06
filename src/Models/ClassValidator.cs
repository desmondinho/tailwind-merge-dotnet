namespace TailwindMerge.Models;

internal readonly struct ClassValidator( string classGroupId, Func<string, bool> validator )
{
    internal string ClassGroupId { get; } = classGroupId;
    internal Func<string, bool> Validator { get; } = validator;
}