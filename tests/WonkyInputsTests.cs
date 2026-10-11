namespace TailwindMerge.Tests;

public class WonkyInputsTests
{
    [Theory]
    [InlineData( " block", "block" )]
    [InlineData( "block ", "block" )]
    [InlineData( " block ", "block" )]
    [InlineData( "  block  px-2     py-4  ", "block px-2 py-4" )]
    [InlineData( "block\npx-2", "block px-2" )]
    [InlineData( "\nblock\npx-2\n", "block px-2" )]
    [InlineData( "  block\n        \n        px-2   \n          py-4  ", "block px-2 py-4" )]
    [InlineData( "\r  block\n\r        \n        px-2   \n          py-4  ", "block px-2 py-4" )]
    public void Merge_WonkyInputs_MergesCorrectly( string classList, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classList );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Fact]
    public void Merge_WonkyInputsInMultipleArguments_MergesCorrectly()
    {
        // Act
        var actual = new TwMerge().Merge( "  block  px-2", " ", "     py-4  " );

        // Assert
        Assert.Equal( "block px-2 py-4", actual );
    }
}
