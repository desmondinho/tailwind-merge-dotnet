using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

internal class TwMergeMapFactory
{
    internal static ClassNameNode Create( TwMergeConfig config )
    {
        // Initialize a root node of the map
        var classMap = new ClassNameNode()
        {
            Next = new( 158 )
        };

        foreach( var (classGroupId, classGroup) in config.ClassGroups )
        {
            ProcessClassGroupsRecursively(
                classMap,
                classGroupId,
                classGroup.BaseClassName,
                classGroup.Definitions,
                config.Theme
            );
        }

        return classMap;
    }

    private static void ProcessClassGroupsRecursively(
        ClassNameNode node,
        string classGroupId,
        string? classGroupBaseClassName,
        object[] definitions,
        Dictionary<string, object[]> theme )
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

            if( definition is ThemeGetter themeGetter )
            {
                ProcessClassGroupsRecursively( current, classGroupId, null, themeGetter( theme ), theme );
                continue;
            }

            if( definition is ClassGroup nestedClassGroup )
            {
                var next = current.AddNextNode( nestedClassGroup.BaseClassName! );
                ProcessClassGroupsRecursively( next, classGroupId, null, nestedClassGroup.Definitions, theme );
            }
        }
    }
}
