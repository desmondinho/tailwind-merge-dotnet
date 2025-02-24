using Microsoft.Extensions.Options;

using Moq;

namespace TailwindMerge.Tests;

public class PrefixTests
{
    [Theory]
    [InlineData( "tw:block tw:hidden", "tw:hidden" )]
    [InlineData( "block hidden", "block hidden" )]

    [InlineData( "tw:p-3 tw:p-2", "tw:p-2" )]
    [InlineData( "p-3 p-2", "p-3 p-2" )]

    [InlineData( "tw:right-0! tw:inset-0!", "tw:inset-0!" )]
    [InlineData( "tw:hover:focus:right-0! tw:focus:hover:inset-0!", "tw:focus:hover:inset-0!" )]
    public void ShouldMergeCorrectlyWithPrefix( string classLists, string expected )
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig
        {
            Prefix = "tw"
        };

        mockOptions.Setup( ap => ap.Value ).Returns( config );

        // Act
        var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}