namespace TailwindMerge.Tests;

public class ModifiersTests
{
    [Theory]
    [InlineData( "hover:block hover:inline", "hover:inline" )]
    [InlineData( "hover:block hover:focus:inline", "hover:block hover:focus:inline" )]
    [InlineData( "line-through no-underline", "no-underline" )]
    [InlineData( "hover:block hover:focus:inline focus:hover:inline", "hover:block focus:hover:inline" )]
    [InlineData( "focus-within:inline focus-within:block", "focus-within:block" )]
    public void Merge_ClassesWithPrefixModifiers_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "text-lg/7 text-lg/8", "text-lg/8" )]
    [InlineData( "text-lg/none leading-9", "text-lg/none leading-9" )]
    [InlineData( "leading-9 text-lg/none", "text-lg/none" )]
    [InlineData( "w-full w-1/2", "w-1/2" )]
    public void Merge_ClassesWithPostfixModifiers_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}