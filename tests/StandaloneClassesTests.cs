namespace TailwindMerge.Tests;

public class StandaloneClassesTests
{
    [Theory]
    [InlineData( "inline block", "block" )]
    [InlineData( "underline line-through", "line-through" )]
    [InlineData( "line-through no-underline", "no-underline" )]
    public void MergesStandaloneClassesFromSameGroupCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}