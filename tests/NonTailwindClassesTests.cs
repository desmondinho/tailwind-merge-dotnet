namespace TailwindMerge.Tests;

public class NonTailwindClassesTests
{
    [Theory]
    [InlineData( "non-tailwind-class inline block", "non-tailwind-class block" )]
    [InlineData( "inline block inline-1", "block inline-1" )]
    [InlineData( "inline block i-inline", "block i-inline" )]
    [InlineData( "focus:inline focus:block focus:inline-1", "focus:block focus:inline-1" )]
    public void Merge_NonTailwindClasses_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}