namespace TailwindMerge.Tests;

public class ColorsClassesTests
{
    [Theory]
    [InlineData( "bg-grey-5 bg-hotpink", "bg-hotpink" )]
    [InlineData( "hover:bg-grey-5 hover:bg-hotpink", "hover:bg-hotpink" )]
    [InlineData( "stroke-[hsl(350_80%_0%)] stroke-[10px]", "stroke-[hsl(350_80%_0%)] stroke-[10px]" )]
    public void Merge_Colors_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}