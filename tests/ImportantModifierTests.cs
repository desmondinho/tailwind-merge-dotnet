namespace TailwindMerge.Tests;

public class ImportantModifierTests
{
    [Theory]
	[InlineData( "font-medium! font-bold!", "font-bold!" )]
	[InlineData( "font-medium! font-bold! font-thin", "font-bold! font-thin" )]
	[InlineData( "right-2! -inset-x-px!", "-inset-x-px!" )]
	[InlineData( "focus:inline! focus:block!", "focus:block!" )]
	[InlineData( "[--my-var:20px]! [--my-var:30px]!", "[--my-var:30px]!" )]

	// Tailwind CSS v3 legacy syntax
	[InlineData( "font-medium! !font-bold", "!font-bold" )]

	[InlineData( "!font-medium !font-bold", "!font-bold" )]
    [InlineData( "!font-medium !font-bold font-thin", "!font-bold font-thin" )]
    [InlineData( "!right-2 !-inset-x-px", "!-inset-x-px" )]
    [InlineData( "focus:!inline focus:!block", "focus:!block" )]
    [InlineData( "![--my-var:20px] ![--my-var:30px]", "![--my-var:30px]" )]
    public void Merge_ClassesWithImportantModifier_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}