namespace TailwindMerge.Tests;

public class NegativeValueClassesTests
{
    [Theory]
    [InlineData( "-m-2 -m-5", "-m-5" )]
    [InlineData( "-top-12 -top-2000", "-top-2000" )]
    public void Merge_NegativeValues_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "-m-2 m-auto", "m-auto" )]
    [InlineData( "top-12 -top-69", "-top-69" )]
    public void Merge_NegativeAndPositiveValues_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "-right-1 inset-x-1", "inset-x-1" )]
    [InlineData( "hover:focus:-right-1 focus:hover:inset-x-1", "focus:hover:inset-x-1" )]
    public void Merge_NegativeValuesAcrossGroups_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}