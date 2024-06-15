using TailwindMerge.Models;

namespace TailwindMerge;

internal class TwMergeMapFactory
{
    internal static ClassNameNode Create( TwMergeConfig config )
    {
        // Initialize a root node of the map
        var classMap = new ClassNameNode();

        foreach( var classGroup in config.ClassGroups2 )
        {
            ProcessClassGroups( classMap, classGroup );
        }

        return classMap;
    }

    internal static void ProcessClassGroups( ClassNameNode root, ClassGroup2 classGroup )
    {
        // Process standalone class groups (e.g. `display`, `container`)
        if( string.IsNullOrEmpty( classGroup.BaseClassName ) )
        {
            foreach( var definition in classGroup.Definitions )
            {
                if( definition is string @string )
                {
                    var current = root.AddNextNode( @string );
                    current.ClassGroupId = classGroup.Id;
                }
            }

            //foreach( var classNamePart in classGroup.ClassNameParts! )
            //{
            //    var current = root.AddNextNode( classNamePart );
            //    current.ClassGroupId = classGroup.Id;
            //}
        }
        // Process all other class groups
        else
        {
            var current = root.AddNextNode( classGroup.BaseClassName );

            foreach( var definition in classGroup.Definitions )
            {
                if( definition is string @string )
                {
                    // Prevent class groups with common class names (e.g. `border`) 
                    // from overriding each others `ClassGroupId`.
                    if( string.IsNullOrEmpty( current.ClassGroupId ) )
                    {
                        current.ClassGroupId = classGroup.Id;
                    }

                    if( !string.IsNullOrEmpty( @string ) )
                    {
                        var next = current.AddNextNode( @string );
                        next.ClassGroupId = classGroup.Id;
                    }
                }
                else if( definition is Func<string, bool> validator )
                {
                    current.AddValidator( validator, classGroup.Id );
                }
            }

            //if( classGroup.ClassNameParts is not null )
            //{
            //    // Prevent class groups with common class names (e.g. `border`) 
            //    // from overriding each others `ClassGroupId`.
            //    if( string.IsNullOrEmpty( current.ClassGroupId ) )
            //    {
            //        current.ClassGroupId = classGroup.Id;
            //    }

            //    foreach( var item in classGroup.ClassNameParts )
            //    {
            //        if( !string.IsNullOrEmpty( item ) )
            //        {
            //            var next = current.AddNextNode( item );
            //            next.ClassGroupId = classGroup.Id;
            //        }
            //    }
            //}

            //if( classGroup.Validators is not null )
            //{
            //    foreach( var validator in classGroup.Validators )
            //    {
            //        current.AddValidator( validator, classGroup.Id );
            //    }
            //}
        }
    }
}
