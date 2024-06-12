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
        if( string.IsNullOrEmpty( classGroup.BaseClassName ) )
        {
            foreach( var item in classGroup.ClassNameParts! )
            {
                var current = root.AddNextNode( item );
                current.ClassGroupId = classGroup.Id;
            }
        }
        // Process all other class groups
        else
        {
            var current = root.AddNextNode( classGroup.BaseClassName );

            if( classGroup.ClassNameParts is not null )
            {
                // Prevent class groups with common class names (e.g. `border`) 
                // from overriding each others `ClassGroupId`.
                if( string.IsNullOrEmpty( current.ClassGroupId ) )
                {
                    current.ClassGroupId = classGroup.Id;
                }

                foreach( var item in classGroup.ClassNameParts )
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
