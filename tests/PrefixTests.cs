using Microsoft.Extensions.Options;

using Moq;

namespace TailwindMerge.Tests;

public class PrefixTests
{
    [Theory]
    [InlineData( "tw-block tw-hidden", "tw-hidden" )]
    [InlineData( "block hidden", "block hidden" )]
    [InlineData( "tw-p-3 tw-p-2", "tw-p-2" )]
    [InlineData( "p-3 p-2", "p-3 p-2" )]
    [InlineData( "!tw-right-0 !tw-inset-0", "!tw-inset-0" )]
    [InlineData( "hover:focus:!tw-right-0 focus:hover:!tw-inset-0", "focus:hover:!tw-inset-0" )]
    public void ShouldMergeCorrectlyWithPrefix( string classLists, string expected )
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig
        {
            Prefix = "tw-"
        };

        mockOptions.Setup( ap => ap.Value ).Returns( config );
        var twMerge = new TwMerge( mockOptions.Object );

        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}