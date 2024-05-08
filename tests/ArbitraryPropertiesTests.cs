namespace TailwindMerge.Tests;

public class ArbitraryPropertiesTests
{
    [Theory]
    [InlineData( "[paint-order:markers] [paint-order:normal]", "[paint-order:normal]" )]
    [InlineData(
        "[paint-order:markers] [--my-var:2rem] [paint-order:normal] [--my-var:4px]",
        "[paint-order:normal] [--my-var:4px]"
    )]
    public void Merge_ArbitraryProperties_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "[paint-order:markers] hover:[paint-order:normal]",
        "[paint-order:markers] hover:[paint-order:normal]"
    )]
    [InlineData(
        "hover:[paint-order:markers] hover:[paint-order:normal]",
        "hover:[paint-order:normal]"
    )]
    [InlineData(
        "hover:focus:[paint-order:markers] focus:hover:[paint-order:normal]",
        "focus:hover:[paint-order:normal]"
    )]
    [InlineData(
        "[paint-order:markers] [paint-order:normal] [--my-var:2rem] lg:[--my-var:4px]",
        "[paint-order:normal] [--my-var:2rem] lg:[--my-var:4px]"
    )]
    public void Merge_ArbitraryPropertiesWithModifiers_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData(
        "[-unknown-prop:::123:::] [-unknown-prop:url(https://hi.com)]",
        "[-unknown-prop:url(https://hi.com)]"
    )]
    public void Merge_ComplexArbitraryProperties_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "![some:prop] [some:other]", "![some:prop] [some:other]" )]
    [InlineData( "![some:prop] [some:other] [some:one] ![some:another]", "[some:one] ![some:another]" )]
    public void Merge_ArbitraryPropertiesWithImportantModifier_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}