namespace TailwindMerge.Tests;

public class ArbitraryValuesTests
{
    [Theory]
    [InlineData( "m-[2px] m-[10px]", "m-[10px]" )]
    [InlineData(
        "m-[2px] m-[11svmin] m-[12in] m-[13lvi] m-[14vb] m-[15vmax] m-[16mm] m-[17%] m-[18em] m-[19px] m-[10dvh]",
        "m-[10dvh]" 
    )]
    [InlineData(
        "h-[10px] h-[11cqw] h-[12cqh] h-[13cqi] h-[14cqb] h-[15cqmin] h-[16cqmax]",
        "h-[16cqmax]" 
    )]
    [InlineData( "z-20 z-[99]", "z-[99]" )]
    [InlineData( "my-[2px] m-[10rem]", "m-[10rem]" )]
    [InlineData( "cursor-pointer cursor-[grab]", "cursor-[grab]" )]
    [InlineData( "m-[2px] m-[calc(100%-var(--arbitrary))]", "m-[calc(100%-var(--arbitrary))]" )]
    [InlineData( "m-[2px] m-[length:var(--mystery-var)]", "m-[length:var(--mystery-var)]" )]
    [InlineData( "opacity-10 opacity-[0.025]", "opacity-[0.025]" )]
    [InlineData( "scale-75 scale-[1.7]", "scale-[1.7]" )]
    [InlineData( "brightness-90 brightness-[1.75]", "brightness-[1.75]" )]
    [InlineData( "min-h-[0.5px] min-h-[0]", "min-h-[0]" )]
    [InlineData( "text-[0.5px] text-[color:0]", "text-[0.5px] text-[color:0]" )]
    [InlineData( "text-[0.5px] text-[--my-0]", "text-[0.5px] text-[--my-0]" )]
    public void Merge_ArbitraryValues_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "hover:m-[2px] hover:m-[length:var(--c)]", "hover:m-[length:var(--c)]" )]
    [InlineData( "hover:focus:m-[2px] focus:hover:m-[length:var(--c)]", "focus:hover:m-[length:var(--c)]" )]
    [InlineData(
        "border-b border-[color:rgb(var(--color-gray-500-rgb)/50%))]",
        "border-b border-[color:rgb(var(--color-gray-500-rgb)/50%))]" 
    )]
    [InlineData(
        "border-[color:rgb(var(--color-gray-500-rgb)/50%))] border-b",
        "border-[color:rgb(var(--color-gray-500-rgb)/50%))] border-b" 
    )]
    [InlineData(
        "border-b border-[color:rgb(var(--color-gray-500-rgb)/50%))] border-some-coloooor",
        "border-b border-some-coloooor" 
    )]
    public void Merge_ArbitraryLengthsWithLabelsAndModifiers_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "grid-rows-[1fr,auto] grid-rows-2", "grid-rows-2" )]
    [InlineData( "grid-rows-[repeat(20,minmax(0,1fr))] grid-rows-3", "grid-rows-3" )]
    public void Merge_ComplexArbitraryValues_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "mt-2 mt-[calc(theme(fontSize.4xl)/1.125)]",
        "mt-[calc(theme(fontSize.4xl)/1.125)]"
    )]
    [InlineData(
        "p-2 p-[calc(theme(fontSize.4xl)/1.125)_10px]",
        "p-[calc(theme(fontSize.4xl)/1.125)_10px]"
    )]
    [InlineData(
        "mt-2 mt-[length:theme(someScale.someValue)]",
        "mt-[length:theme(someScale.someValue)]"
    )]
    [InlineData(
        "mt-2 mt-[theme(someScale.someValue)]",
        "mt-[theme(someScale.someValue)]"
    )]
    [InlineData(
        "text-2xl text-[length:theme(someScale.someValue)]",
        "text-[length:theme(someScale.someValue)]"
    )]
    [InlineData(
        "text-2xl text-[calc(theme(fontSize.4xl)/1.125)]",
        "text-[calc(theme(fontSize.4xl)/1.125)]"
    )]
    [InlineData(
        "bg-cover bg-[percentage:30%] bg-[length:200px_100px]",
        "bg-[length:200px_100px]"
    )]
    [InlineData(
        "bg-none bg-[url(.)] bg-[image:.] bg-[url:.] bg-[linear-gradient(.)] bg-linear-to-r",
		"bg-linear-to-r"
	)]
    public void Merge_AmbiguousArbitraryValues_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}