using Microsoft.Extensions.Options;

using Moq;
using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class ExtendTests
{
    [Theory]
    [InlineData( "shadow-lg shadow-100 shadow-200", "shadow-lg shadow-200" )]
    [InlineData( "custom-100 custom-200", "custom-200" )]
    [InlineData( "text-lg text-foo", "text-foo" )]
    [InlineData( "px-3 py-3 p-3", "py-3 p-3" )]
    [InlineData( "p-3 px-3 py-3", "p-3 px-3 py-3" )]
    [InlineData( "mx-2 my-2 h-2 m-2", "m-2" )]
    [InlineData( "m-2 mx-2 my-2 h-2", "m-2 mx-2 my-2 h-2" )]
    public void ShouldOverrideAndExtendConfigCorrectly( string classLists, string expected )
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

        // Act
        var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}