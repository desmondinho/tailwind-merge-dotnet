namespace TailwindMerge.Tests;

public class ClassGroupsTests
{
    [Theory]
    [InlineData( "inset-1 inset-x-1", "inset-1 inset-x-1" )]
    [InlineData( "inset-x-1 inset-1", "inset-1" )]
    [InlineData( "inset-x-1 left-1 inset-1", "inset-1" )]
    [InlineData( "inset-x-1 inset-1 left-1", "inset-1 left-1" )]
    [InlineData( "inset-x-1 right-1 inset-1", "inset-1" )]
    [InlineData( "inset-x-1 right-1 inset-x-1", "inset-x-1" )]
    [InlineData( "inset-x-1 right-1 inset-y-1", "inset-x-1 right-1 inset-y-1" )]
    [InlineData( "right-1 inset-x-1 inset-y-1", "inset-x-1 inset-y-1" )]
    [InlineData( "inset-x-1 hover:left-1 inset-1", "hover:left-1 inset-1" )]
    public void Merge_ClassGroups_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "overflow-x-auto overflow-x-hidden", "overflow-x-hidden" )]
    [InlineData( "basis-full basis-auto", "basis-auto" )]
    [InlineData( "w-full w-fit", "w-fit" )]
    [InlineData( "overflow-x-auto overflow-x-hidden overflow-x-scroll", "overflow-x-scroll" )]
    [InlineData( 
        "overflow-x-auto hover:overflow-x-hidden overflow-x-scroll", 
        "hover:overflow-x-hidden overflow-x-scroll" 
    )]
    public void Merge_SameClassGroups_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "ring shadow", "ring shadow" )]
    [InlineData( "ring-2 shadow-md", "ring-2 shadow-md" )]
    [InlineData( "shadow ring", "shadow ring" )]
    [InlineData( "shadow-md ring-2", "shadow-md ring-2" )]
    public void Merge_RingAndShadow_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "touch-pan-x touch-pan-right", "touch-pan-right" )]
    [InlineData( "touch-none touch-pan-x", "touch-pan-x" )]
    [InlineData( "touch-pan-x touch-none", "touch-none" )]
    [InlineData( 
        "touch-pan-x touch-pan-y touch-pinch-zoom", 
        "touch-pan-x touch-pan-y touch-pinch-zoom" 
    )]
    [InlineData( 
        "touch-manipulation touch-pan-x touch-pan-y touch-pinch-zoom",
        "touch-pan-x touch-pan-y touch-pinch-zoom" 
    )]
    [InlineData( "touch-pan-x touch-pan-y touch-pinch-zoom touch-auto", "touch-auto" )]
    public void Merge_TouchClasses_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "overflow-auto inline line-clamp-1", "line-clamp-1" )]
    [InlineData( "line-clamp-1 overflow-auto inline", "line-clamp-1 overflow-auto inline" )]
    public void Merge_LineClampClasses_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( 
        "lining-nums tabular-nums diagonal-fractions", 
        "lining-nums tabular-nums diagonal-fractions" 
    )]
    [InlineData( "normal-nums tabular-nums diagonal-fractions", "tabular-nums diagonal-fractions" )]
    [InlineData( "tabular-nums diagonal-fractions normal-nums", "normal-nums" )]
    [InlineData( "tabular-nums proportional-nums", "proportional-nums" )]
    public void Merge_FontVariantNumericClasses_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}