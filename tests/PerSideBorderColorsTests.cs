namespace TailwindMerge.Tests;

public class PerSideBorderColorsTests
{
    [Theory]
    [InlineData( "border-t-some-blue border-t-other-blue", "border-t-other-blue" )]
    [InlineData( "border-t-some-blue border-some-blue", "border-some-blue" )]
    public void Merge_PerSideBorderColors_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}