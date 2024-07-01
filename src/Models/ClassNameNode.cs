using TailwindMerge.Common;

namespace TailwindMerge.Models;

internal record ClassNameNode
{
    internal string? ClassGroupId { get; set; }
    internal Dictionary<string, ClassNameNode>? Next { get; set; }
    internal List<ClassValidator>? Validators { get; set; }

    internal ClassNameNode AddNextNode( string className )
    {
        var current = this;
        var parts = className.Split( Constants.ClassNameSeparator, StringSplitOptions.RemoveEmptyEntries );

        foreach( var part in parts )
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
        Validators ??= [];
        Validators.Add( new ClassValidator( classGroupId, validator ) );
    }
}
