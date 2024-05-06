namespace TailwindMerge.Tests;

public class StandaloneClassesTests
{
    [Theory]
    [InlineData( "inline block", "block" )]
    [InlineData( "underline line-through", "line-through" )]
    [InlineData( "line-through no-underline", "no-underline" )]
    [InlineData( "hover:block hover:inline", "hover:inline" )]
    [InlineData( "hover:inline hover:block", "hover:block" )]
    [InlineData( 
        "inline hover:inline focus:inline hover:block hover:focus:block", 
        "inline focus:inline hover:block hover:focus:block" 
    )]
    public void MergesStandaloneClassesFromSameGroupCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}