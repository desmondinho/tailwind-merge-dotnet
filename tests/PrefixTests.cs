using Microsoft.Extensions.Options;

using Moq;

namespace TailwindMerge.Tests;

public class PrefixTests
{
    [Fact]
    public void ShouldMergeCorrectlyWithPrefix()
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig
        {
            Prefix = "tw-"
        };

        mockOptions.Setup( ap => ap.Value ).Returns( config );
        var twMerge = new TwMerge( mockOptions.Object );

        Assert.Equal( "tw-hidden", twMerge.Merge( "tw-block tw-hidden" ) );
        Assert.Equal( "block hidden", twMerge.Merge( "block hidden" ) );

        Assert.Equal( "tw-p-2", twMerge.Merge( "tw-p-3 tw-p-2" ) );
        Assert.Equal( "p-3 p-2", twMerge.Merge( "p-3 p-2" ) );

        Assert.Equal( "!tw-inset-0", twMerge.Merge( "!tw-right-0 !tw-inset-0" ) );

        Assert.Equal( "focus:hover:!tw-inset-0", twMerge.Merge( "hover:focus:!tw-right-0 focus:hover:!tw-inset-0" ) );
    }
}