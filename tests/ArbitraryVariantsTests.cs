namespace TailwindMerge.Tests;

public class ArbitraryVariantsTests
{
    [Theory]
    [InlineData( "[p]:underline [p]:line-through", "[p]:line-through" )]
    [InlineData( "[&>*]:underline [&>*]:line-through", "[&>*]:line-through" )]
    [InlineData(
        "[&>*]:underline [&>*]:line-through [&_div]:line-through",
        "[&>*]:line-through [&_div]:line-through"
    )]
    [InlineData(
        "supports-[display:grid]:flex supports-[display:grid]:grid",
        "supports-[display:grid]:grid"
    )]
    public void Merge_BasicArbitraryVariants_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "dark:lg:hover:[&>*]:underline dark:lg:hover:[&>*]:line-through",
        "dark:lg:hover:[&>*]:line-through"
    )]
    [InlineData(
        "dark:lg:hover:[&>*]:underline dark:hover:lg:[&>*]:line-through",
        "dark:hover:lg:[&>*]:line-through"
    )]
    [InlineData(
        "hover:[&>*]:underline [&>*]:hover:line-through",
        "hover:[&>*]:underline [&>*]:hover:line-through"
    )]
    [InlineData(
        "hover:dark:[&>*]:underline dark:hover:[&>*]:underline dark:[&>*]:hover:line-through",
        "dark:hover:[&>*]:underline dark:[&>*]:hover:line-through"
    )]
    public void Merge_ArbitraryVariantsWithModifiers_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "[@media_screen{@media(hover:hover)}]:underline [@media_screen{@media(hover:hover)}]:line-through",
        "[@media_screen{@media(hover:hover)}]:line-through"
    )]
    [InlineData(
        "hover:[@media_screen{@media(hover:hover)}]:underline hover:[@media_screen{@media(hover:hover)}]:line-through",
        "hover:[@media_screen{@media(hover:hover)}]:line-through"
    )]
    public void Merge_ArbitraryVariantsWithComplexSyntaxInTheme_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "[&[data-open]]:underline [&[data-open]]:line-through", "[&[data-open]]:line-through" )]
    public void Merge_ArbitraryVariantsWithAttributeSelectors_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "[&[data-foo][data-bar]:not([data-baz])]:underline [&[data-foo][data-bar]:not([data-baz])]:line-through",
        "[&[data-foo][data-bar]:not([data-baz])]:line-through"
    )]
    public void Merge_ArbitraryVariantsWithMultipleAttributeSelectors_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "[&>*]:[&_div]:underline [&>*]:[&_div]:line-through", "[&>*]:[&_div]:line-through" )]
    [InlineData( 
        "[&>*]:[&_div]:underline [&_div]:[&>*]:line-through", 
        "[&>*]:[&_div]:underline [&_div]:[&>*]:line-through" 
    )]
    [InlineData(
        "hover:dark:[&>*]:focus:disabled:[&_div]:underline dark:hover:[&>*]:disabled:focus:[&_div]:line-through",
        "dark:hover:[&>*]:disabled:focus:[&_div]:line-through"
    )]
    [InlineData(
        "hover:dark:[&>*]:focus:[&_div]:disabled:underline dark:hover:[&>*]:disabled:focus:[&_div]:line-through",
        "hover:dark:[&>*]:focus:[&_div]:disabled:underline dark:hover:[&>*]:disabled:focus:[&_div]:line-through"
    )]
    public void Merge_MultipleArbitraryVariants_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "[&>*]:[color:red] [&>*]:[color:blue]", "[&>*]:[color:blue]" )]
    [InlineData(
        "[&[data-foo][data-bar]:not([data-baz])]:nod:noa:[color:red] [&[data-foo][data-bar]:not([data-baz])]:noa:nod:[color:blue]",
        "[&[data-foo][data-bar]:not([data-baz])]:noa:nod:[color:blue]"
    )]
    public void Merge_ArbitraryVariantsWithArbitraryProperties_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}