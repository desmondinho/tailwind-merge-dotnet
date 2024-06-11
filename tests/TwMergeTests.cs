namespace TailwindMerge.Tests;

public class TwMergeTests
{
    [Theory]
    [InlineData( "mix-blend-normal mix-blend-multiply", "mix-blend-multiply" )]
    [InlineData( "h-10 h-min", "h-min" )]
    [InlineData( "stroke-black stroke-1", "stroke-black stroke-1" )]
    [InlineData( "stroke-2 stroke-[3]", "stroke-[3]" )]
    [InlineData( "outline-black outline-1", "outline-black outline-1" )]
    [InlineData( "grayscale-0 grayscale-[50%]", "grayscale-[50%]" )]
    [InlineData( "grow grow-[2]", "grow-[2]" )]
    [InlineData( "", null )]
    [InlineData( null, null )]
    public void Merge_MergesCorrectly( string classList, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classList );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Fact]
    public void Merge_SameValues_ReturnsCachedResult()
    {
        // Arrange
        var twMerge = new TwMerge();

        // Act
        var actual = twMerge.Merge( "h-10 h-min" );

        // Assert
        Assert.Equal( "h-min", actual );

        // Act
        actual = twMerge.Merge( "h-10 h-min" );

        // Assert
        Assert.Equal( "h-min", actual );
    }
}