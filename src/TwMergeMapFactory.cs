using TailwindMerge.Models;

namespace TailwindMerge;

internal class TwMergeMapFactory
{
    internal static ClassNameNode Create( TwMergeConfig config )
    {
        // Initialize a root node of the map
        var classMap = new ClassNameNode();

        foreach( var classGroup in config.ClassGroups )
        {
            ProcessClassGroups( classMap, classGroup );
        }

        return classMap;
    }

    internal static void ProcessClassGroups( ClassNameNode root, ClassGroup classGroup )
    {
        // Process standalone class groups (e.g. `display`, `container`)
        if( string.IsNullOrEmpty( classGroup.ClassName ) )
        {
            foreach( var item in classGroup.Items! )
            {
                var current = root.AddNextNode( item );
                current.ClassGroupId = classGroup.Id;
            }
        }
        // Process all other class groups
        else
        {
            var current = root.AddNextNode( classGroup.ClassName );

            if( classGroup.Items is not null )
            {
                // To prevent class groups that have common class name (e.g. `border`) 
                // from overriding each others `ClassGroupId`.
                if( current.Next is null )
                {
                    current.ClassGroupId = classGroup.Id;
                }

                foreach( var item in classGroup.Items )
                {
                    if( !string.IsNullOrEmpty( item ) )
                    {
                        var next = current.AddNextNode( item );
                        next.ClassGroupId = classGroup.Id;
                    }
                }
            }

            if( classGroup.Validators is not null )
            {
                foreach( var validator in classGroup.Validators )
                {
                    current.AddValidator( validator, classGroup.Id );
                }
            }
        }
    }
}
