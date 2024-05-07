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
                _ = root.AddNextNode( item, classGroup.Id );
            }
        }
        // Process all other class groups
        else
        {
            var current = root.AddNextNode( classGroup.ClassName, classGroup.Id );

            if( classGroup.Items is not null )
            {
                foreach( var item in classGroup.Items )
                {
                    if( !string.IsNullOrEmpty( item ) )
                    {
                        _ = current.AddNextNode( item, classGroup.Id );
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
