using Microsoft.Extensions.Options;

using Moq;

using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class ExtendConfigTests
{
    [Fact]
    public void Extend()
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = TwMergeConfig.Default()
            .Extend( [
                new( "shadow", "shadow", ["small", "medium"] )
            ] );
        mockOptions.Setup( ap => ap.Value ).Returns( config );

        var twMerge = new TwMerge( mockOptions.Object );

        // Act
        var actual = twMerge.Merge( "shadow-lg shadow-small" );

        // Assert
        Assert.Equal( "shadow-small", actual );
    }
}