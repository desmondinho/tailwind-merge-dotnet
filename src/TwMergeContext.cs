using TailwindMerge.Models;

namespace TailwindMerge;

internal class TwMergeContext
{
    private const string ClassNameSeparator = "-";

    private readonly ClassNameNode _classMap;

    internal TwMergeContext( TwMergeConfig config )
    {
        _classMap = TwMergeMapFactory.Create( config );
    }

    internal string? GetClassGroupId( string className )
    {
        var classNameParts = className.Split( ClassNameSeparator );

        // Classes like "-inset-1" produce an empty string as the first classNamePart.
        // We assume that classes for negative values are used correctly and remove it from classNameParts.
        if( classNameParts[0] == "" && classNameParts.Length != 1 )
        {
            classNameParts = classNameParts.Skip( 1 ).ToArray();
        }

        return GetClassGroupIdRecursive( classNameParts, _classMap );
    }

    private string? GetClassGroupIdRecursive( string[] classNameParts, ClassNameNode node )
    {
        if( classNameParts.Length == 0 )
        {
            return node.ClassGroupId;
        }

        var currentClassNamePart = classNameParts[0];

        if( node.Children!.TryGetValue( currentClassNamePart, out var nextClassNameNode ) )
        {
            var classGroupId = GetClassGroupIdRecursive( classNameParts[1..], nextClassNameNode );

            if( !string.IsNullOrEmpty( classGroupId ) )
            {
                return classGroupId;
            }
        }

        if( node.Validators is { Count: 0 } )
        {
            return null;
        }

        var classNameRest = string.Join( ClassNameSeparator, classNameParts );

        return node.Validators?.FirstOrDefault( validator => validator.Validator( classNameRest ) ).ClassGroupId;
    }
}
