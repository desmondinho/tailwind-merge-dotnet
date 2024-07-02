using Microsoft.Extensions.Options;

using Moq;

namespace TailwindMerge.Tests;

public class SeparatorTests
{
    [Theory]
    [InlineData( "block hidden", "hidden" )]
    [InlineData( "p-3 p-2", "p-2" )]
    [InlineData( "!right-0 !inset-0", "!inset-0" )]
    [InlineData( "hover_focus_!right-0 focus_hover_!inset-0", "focus_hover_!inset-0" )]
    [InlineData( "hover:focus:!right-0 focus:hover:!inset-0", "hover:focus:!right-0 focus:hover:!inset-0" )]
    public void ShouldMergeCorrectlyWithSingleCharacterSeparator( string classLists, string expected )
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig
        {
            Separator = "_"
        };

        mockOptions.Setup( ap => ap.Value ).Returns( config );

        // Act
        var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "block hidden", "hidden" )]
    [InlineData( "p-3 p-2", "p-2" )]
    [InlineData( "!right-0 !inset-0", "!inset-0" )]
    [InlineData( "hover__focus__!right-0 focus__hover__!inset-0", "focus__hover__!inset-0" )]
    [InlineData( "hover:focus:!right-0 focus:hover:!inset-0", "hover:focus:!right-0 focus:hover:!inset-0" )]
    public void ShouldMergeCorrectlyWithMultipleCharactersSeparator( string classLists, string expected )
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig
        {
            Separator = "__"
        };

        mockOptions.Setup( ap => ap.Value ).Returns( config );

        // Act
        var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}