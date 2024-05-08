namespace TailwindMerge.Tests;

public class ContentUtilitiesTests
{
    [Theory]
    [InlineData( 
        "content-['hello'] content-[attr(data-content)]", 
        "content-[attr(data-content)]" 
    )]
    public void Merge_ContentUtilities_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}