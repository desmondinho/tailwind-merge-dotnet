namespace TailwindMerge.Tests;

public class PseudoVariantsTests
{
    [Theory]
    [InlineData( "empty:p-2 empty:p-3", "empty:p-3" )]
    [InlineData( "hover:empty:p-2 hover:empty:p-3", "hover:empty:p-3" )]
    [InlineData( "read-only:p-2 read-only:p-3", "read-only:p-3" )]
    public void Merge_PseudoVariants_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "group-empty:p-2 group-empty:p-3", "group-empty:p-3" )]
    [InlineData( "peer-empty:p-2 peer-empty:p-3", "peer-empty:p-3" )]
    [InlineData( "group-empty:p-2 peer-empty:p-3", "group-empty:p-2 peer-empty:p-3" )]
    [InlineData( "hover:group-empty:p-2 hover:group-empty:p-3", "hover:group-empty:p-3" )]
    [InlineData( "group-read-only:p-2 group-read-only:p-3", "group-read-only:p-3" )]
    public void Merge_PseudoVariantsGroups_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}