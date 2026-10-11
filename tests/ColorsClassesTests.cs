namespace TailwindMerge.Tests;

public class ColorsClassesTests
{
    [Theory]
    [InlineData( "bg-grey-5 bg-hotpink", "bg-hotpink" )]
    [InlineData( "hover:bg-grey-5 hover:bg-hotpink", "hover:bg-hotpink" )]
    [InlineData( 
        "stroke-[hsl(350_80%_0%)] stroke-[10px]", 
        "stroke-[hsl(350_80%_0%)] stroke-[10px]" 
    )]
    public void Merge_Colors_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "text-sm text-[color(display-p3_1_0_0/50%)]",
        "text-sm text-[color(display-p3_1_0_0/50%)]"
    )]
    [InlineData(
        "text-[color(display-p3_1_0_0/50%)] text-sm",
        "text-[color(display-p3_1_0_0/50%)] text-sm"
    )]
    [InlineData( "text-red-500 text-[color(display-p3_1_0_0/50%)]", "text-[color(display-p3_1_0_0/50%)]" )]
    [InlineData( "text-[color(display-p3_1_0_0/50%)] text-red-500", "text-red-500" )]
    [InlineData(
        "border-2 border-[color(display-p3_1_0_0/50%)]",
        "border-2 border-[color(display-p3_1_0_0/50%)]"
    )]
    [InlineData(
        "border-[color(display-p3_1_0_0/50%)] border-2",
        "border-[color(display-p3_1_0_0/50%)] border-2"
    )]
    [InlineData(
        "stroke-2 stroke-[color(display-p3_1_0_0/50%)]",
        "stroke-2 stroke-[color(display-p3_1_0_0/50%)]"
    )]
    [InlineData(
        "stroke-[color(display-p3_1_0_0/50%)] stroke-2",
        "stroke-[color(display-p3_1_0_0/50%)] stroke-2"
    )]
    public void Merge_ColorFunctionsWithPercentages_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "text-sm text-[light-dark(white,rgb(0_0_0/50%))]",
        "text-sm text-[light-dark(white,rgb(0_0_0/50%))]"
    )]
    [InlineData(
        "text-[light-dark(white,rgb(0_0_0/50%))] text-sm",
        "text-[light-dark(white,rgb(0_0_0/50%))] text-sm"
    )]
    [InlineData( "text-red-500 text-[light-dark(white,rgb(0_0_0/50%))]", "text-[light-dark(white,rgb(0_0_0/50%))]" )]
    [InlineData( "text-[light-dark(white,rgb(0_0_0/50%))] text-red-500", "text-red-500" )]
    [InlineData(
        "border-2 border-[light-dark(white,rgb(0_0_0/50%))]",
        "border-2 border-[light-dark(white,rgb(0_0_0/50%))]"
    )]
    [InlineData(
        "border-[light-dark(white,rgb(0_0_0/50%))] border-2",
        "border-[light-dark(white,rgb(0_0_0/50%))] border-2"
    )]
    [InlineData(
        "stroke-2 stroke-[light-dark(white,rgb(0_0_0/50%))]",
        "stroke-2 stroke-[light-dark(white,rgb(0_0_0/50%))]"
    )]
    [InlineData(
        "stroke-[light-dark(white,rgb(0_0_0/50%))] stroke-2",
        "stroke-[light-dark(white,rgb(0_0_0/50%))] stroke-2"
    )]
    public void Merge_LightDarkFunctionsWithPercentages_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}