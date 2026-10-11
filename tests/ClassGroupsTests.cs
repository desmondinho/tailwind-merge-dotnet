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

    // Since Tailwind CSS v4 the axis utilities compile to logical shorthand properties (px → padding-inline),
    // which fully override their logical-side longhands (ps → padding-inline-start) in every writing mode.
    [Theory]
    [InlineData( "ps-2 px-4", "px-4" )]
    [InlineData( "pe-2 px-4", "px-4" )]
    [InlineData( "px-4 ps-2", "px-4 ps-2" )]
    [InlineData( "pbs-2 py-4", "py-4" )]
    [InlineData( "ms-2 mx-4", "mx-4" )]
    [InlineData( "mbe-2 my-4", "my-4" )]
    [InlineData( "start-2 inset-x-4", "inset-x-4" )]
    [InlineData( "end-2 inset-x-4", "inset-x-4" )]
    [InlineData( "inset-bs-2 inset-y-4", "inset-y-4" )]
    [InlineData( "border-s-2 border-x-4", "border-x-4" )]
    [InlineData( "border-be-2 border-y-4", "border-y-4" )]
    [InlineData( "border-s-red-500 border-x-blue-500", "border-x-blue-500" )]
    [InlineData( "border-bs-red-500 border-y-blue-500", "border-y-blue-500" )]
    [InlineData( "scroll-ms-2 scroll-mx-4", "scroll-mx-4" )]
    [InlineData( "scroll-mbs-2 scroll-my-4", "scroll-my-4" )]
    [InlineData( "scroll-ps-2 scroll-px-4", "scroll-px-4" )]
    [InlineData( "scroll-pbe-2 scroll-py-4", "scroll-py-4" )]
    public void Merge_AxisShorthandsOverrideLogicalSides_MergesCorrectly( string classLists, string expected )
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
    [InlineData( "columns-12 columns-auto", "columns-auto" )]
    [InlineData( "columns-auto columns-2xl", "columns-2xl" )]
    public void Merge_SameClassGroups_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "max-w-lg max-w-none", "max-w-none" )]
    [InlineData( "max-w-none max-w-lg", "max-w-lg" )]
    [InlineData( "max-h-96 max-h-none", "max-h-none" )]
    [InlineData( "max-h-none max-h-96", "max-h-96" )]
    [InlineData( "max-h-[300px] max-h-none", "max-h-none" )]
    [InlineData( "max-h-none max-h-screen", "max-h-screen" )]
    public void Merge_NoneValuesInSizingGroups_MergesCorrectly( string classLists, string expected )
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