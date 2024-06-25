using Microsoft.Extensions.Options;

using Moq;
using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class ThemeTests
{
    [Fact]
    public void Merge_WithExtendedTheme_ShouldMergeCorrectly()
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

        var twMerge = new TwMerge( mockOptions.Object );

        Assert.Equal( "p-my-space p-my-margin", twMerge.Merge( "p-3 p-my-space p-my-margin" ) );
        Assert.Equal( "m-my-margin", twMerge.Merge( "m-3 m-my-space m-my-margin" ) );
    }
}