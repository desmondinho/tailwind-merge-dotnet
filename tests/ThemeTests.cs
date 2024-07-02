using Microsoft.Extensions.Options;

using Moq;
using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class ThemeTests
{
    [Theory]
    [InlineData( "p-3 p-my-space p-my-margin", "p-my-space p-my-margin" )]
    [InlineData( "m-3 m-my-space m-my-margin", "m-my-margin" )]
    public void Merge_WithExtendedThemeScale_ShouldMergeCorrectly( string classLists, string expected )
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig();

        config.Extend( new ExtendedConfig()
        {
            Theme = new()
            {
                ["spacing"] = ["my-space"],
                ["margin"] = ["my-margin"]
            }
        } );

        mockOptions.Setup( ap => ap.Value ).Returns( config );

        // Act
        var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "p-3 p-hello p-hallo", "p-3 p-hello p-hallo" )]
    [InlineData( "px-3 px-hello px-hallo", "px-hallo" )]
    public void Merge_WithExtendedThemeObject_ShouldMergeCorrectly( string classLists, string expected )
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig();

        config.Extend( new ExtendedConfig()
        {
            Theme = new()
            {
                ["my-theme"] = ["hallo", "hello"]
            },
            ClassGroups = new()
            {
                ["px"] = new ClassGroup( "px", [ThemeUtility.FromTheme( "my-theme" )] ),
            }
        } );

        mockOptions.Setup( ap => ap.Value ).Returns( config );

        // Act
        var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}