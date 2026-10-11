using Microsoft.Extensions.Options;

using Moq;

using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class TwMergeTests
{
    [Theory]
    [InlineData( "mix-blend-normal mix-blend-multiply", "mix-blend-multiply" )]
    [InlineData( "h-10 h-min", "h-min" )]
    [InlineData( "stroke-black stroke-1", "stroke-black stroke-1" )]
    [InlineData( "stroke-2 stroke-[3]", "stroke-[3]" )]
    [InlineData( "outline-black outline-1", "outline-black outline-1" )]
    [InlineData( "grayscale-0 grayscale-[50%]", "grayscale-[50%]" )]
    [InlineData( "grow grow-[2]", "grow-[2]" )]
    [InlineData( "", null )]
    [InlineData( null, null )]
    public void Merge_MergesCorrectly( string classList, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classList );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Fact]
    public void Merge_SameValues_ReturnsCachedResult()
    {
        // Arrange
        var twMerge = new TwMerge();

        // Act
        var actual = twMerge.Merge( "h-10 h-min" );

        // Assert
        Assert.Equal( "h-min", actual );

        // Act
        actual = twMerge.Merge( "h-10 h-min" );

        // Assert
        Assert.Equal( "h-min", actual );
    }

    [Fact]
    public void Merge_ConfigExtendedAfterCreation_IgnoresChanges()
    {
        // Arrange
        var config = new TwMergeConfig();
        var twMerge = CreateTwMerge( config );

        config.Extend( new ExtendedConfig()
        {
            Theme = new()
            {
                [ThemeKeys.Spacing] = ["huge"]
            },
            ClassGroups = new()
            {
                ["shadow"] = new ClassGroup( ["elevated"] )
            },
            ConflictingClassGroups = new()
            {
                ["m"] = ["h"]
            }
        } );

        // Act & Assert
        Assert.Equal( "p-2 p-huge", twMerge.Merge( "p-2 p-huge" ) );
        Assert.Equal( "elevated shadow-lg", twMerge.Merge( "elevated shadow-lg" ) );
        Assert.Equal( "h-4 m-4", twMerge.Merge( "h-4 m-4" ) );
    }

    [Fact]
    public void Merge_PostfixLookupClassGroupsExtendedAfterCreation_IgnoresChanges()
    {
        // Arrange
        Func<string, bool> namedValidator = value => value == "foo-bar/baz";

        var config = new TwMergeConfig();
        config.Override( new ExtendedConfig()
        {
            ClassGroups = new()
            {
                ["base"] = new ClassGroup( "foo", ["bar"] ),
                ["named"] = new ClassGroup( [namedValidator] ),
            },
            ConflictingClassGroups = new()
            {
                ["named"] = ["base"],
            }
        } );
        var twMerge = CreateTwMerge( config );

        config.Extend( new ExtendedConfig()
        {
            PostfixLookupClassGroups = ["base"]
        } );

        // Act
        var actual = twMerge.Merge( "foo-bar/baz foo-bar" );

        // Assert
        Assert.Equal( "foo-bar", actual );
    }

    [Fact]
    public void Merge_PrefixSetAfterCreation_IgnoresChange()
    {
        // Arrange
        var config = new TwMergeConfig();
        var twMerge = CreateTwMerge( config );

        config.Prefix = "tw";

        // Act
        var actual = twMerge.Merge( "p-2 p-3" );

        // Assert
        Assert.Equal( "p-3", actual );
    }

    private static TwMerge CreateTwMerge( TwMergeConfig config )
    {
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        mockOptions.Setup( ap => ap.Value ).Returns( config );

        return new TwMerge( mockOptions.Object );
    }
}