using Microsoft.Extensions.Options;

using Moq;
using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class ExtendTests
{
    [Fact]
    public void ShouldOverrideAndExtendConfigCorrectly()
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig();

        config.Override( new ExtendedConfig()
        {
            ClassGroups = new()
            {
                ["shadow"] = new ClassGroup( "shadow", ["100", "200"] ),
                ["customKey"] = new ClassGroup( "custom", ["100"] )
            },
            ConflictingClassGroups = new()
            {
                ["p"] = ["px"]
            }
        } );

        config.Extend( new ExtendedConfig()
        {
            ClassGroups = new()
            {
                ["shadow"] = new ClassGroup( "shadow", ["300"] ),
                ["customKey"] = new ClassGroup( "custom", ["200"] ),
                ["font-size"] = new ClassGroup( "text", ["foo"] ),
            },
            ConflictingClassGroups = new()
            {
                ["m"] = ["h"],
            }
        } );
        mockOptions.Setup( ap => ap.Value ).Returns( config );

        var twMerge = new TwMerge( mockOptions.Object );

        Assert.Equal( "shadow-lg shadow-200", twMerge.Merge( "shadow-lg shadow-100 shadow-200" ) );
        Assert.Equal( "custom-200", twMerge.Merge( "custom-100 custom-200" ) );
        Assert.Equal( "text-foo", twMerge.Merge( "text-lg text-foo" ) );
        Assert.Equal( "py-3 p-3", twMerge.Merge( "px-3 py-3 p-3" ) );
        Assert.Equal( "p-3 px-3 py-3", twMerge.Merge( "p-3 px-3 py-3" ) );
        Assert.Equal( "m-2", twMerge.Merge( "mx-2 my-2 h-2 m-2" ) );
        Assert.Equal( "m-2 mx-2 my-2 h-2", twMerge.Merge( "m-2 mx-2 my-2 h-2" ) );
    }
}