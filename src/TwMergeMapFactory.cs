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
            ProcessClassGroupsRecursively(
                classMap,
                classGroup.Definitions,
                classGroup.Id,
                classGroup.BaseClassName
            );
        }

        return classMap;
    }

    internal static void ProcessClassGroupsRecursively(
        ClassNameNode node,
        object[] definitions,
        string classGroupId,
        string? classGroupBaseClassName )
    {
        var current = node;

        // In order to process all class groups but standalone
        if( !string.IsNullOrEmpty( classGroupBaseClassName ) )
        {
            current = node.AddNextNode( classGroupBaseClassName );
        }

        foreach( var definition in definitions )
        {
            if( definition is string stringDefinition )
            {
                var next = !string.IsNullOrEmpty( stringDefinition )
                    ? current.AddNextNode( stringDefinition )
                    : current;
                next.ClassGroupId = classGroupId;
                continue;
            }
            if( definition is Func<string, bool> validatorDefinition )
            {
                current.AddValidator( validatorDefinition, classGroupId );
                continue;
            }
            if( definition is Func<object[]> themeGetter )
            {
                ProcessClassGroupsRecursively( current, themeGetter(), classGroupId, null );
            }
        }
    }
}
