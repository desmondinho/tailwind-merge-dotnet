namespace TailwindMerge.Models;

internal readonly record struct ClassValidator( string ClassGroupId, Func<string, bool> Validator );