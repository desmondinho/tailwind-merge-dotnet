using TailwindMerge.Common;

namespace TailwindMerge.Models;

internal record ClassNameNode
{
    private List<ClassValidator>? _validators;

    internal string? ClassGroupId { get; set; }
    internal Dictionary<string, ClassNameNode>? Next { get; set; }
    internal IReadOnlyCollection<ClassValidator>? Validators => _validators?.AsReadOnly();

    internal ClassNameNode AddNextNode( string className )
    {
        var current = this;

        foreach( var part in className.Split( Constants.ClassNameSeparator, StringSplitOptions.RemoveEmptyEntries ) )
        {
            current.Next ??= [];

            if( !current.Next.TryGetValue( part, out var next ) )
            {
                next = new ClassNameNode();
                current.Next[part] = next;
            }

            current = next;
        }

        return current;
    }

    internal void AddValidator( Func<string, bool> validator, string classGroupId )
    {
        _validators ??= [];
        _validators.Add( new ClassValidator( classGroupId, validator ) );
    }
}
