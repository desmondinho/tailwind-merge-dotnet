using System.Reflection;

using Microsoft.Extensions.Options;

using Moq;
using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class ThemeTests
{
    [Theory]
    [InlineData( "p-3 p-my-space p-my-margin", "p-my-space p-my-margin" )]
    [InlineData( "leading-3 leading-my-space leading-my-leading", "leading-my-leading" )]
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
                ["leading"] = ["my-leading"]
            }
        } );

        mockOptions.Setup( ap => ap.Value ).Returns( config );

        // Act
        var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    // `leading-none` is a static Tailwind v4 utility (`line-height: 1`) that does
    // not come from the `--leading-*` theme namespace, so overriding the `leading` theme
    // must not stop it from conflicting with other line-height utilities — same as
    // `rounded-none` / `shadow-none` survive `radius` / `shadow` theme overrides.
    [Theory]
    [InlineData( "leading-tight leading-none", "leading-none" )]
    [InlineData( "leading-none leading-tight", "leading-tight" )]
    [InlineData( "leading-4 leading-none", "leading-none" )]
    public void Merge_WithOverriddenLeadingThemeScale_ShouldKeepLeadingNone( string classLists, string expected )
    {
        // Arrange
        var mockOptions = new Mock<IOptions<TwMergeConfig>>();
        var config = new TwMergeConfig();

        config.Override( new ExtendedConfig()
        {
            Theme = new()
            {
                ["leading"] = ["tight"]
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

    [Fact]
    public void FromTheme_ExposesThemeKey()
    {
        // Act
        var themeReference = ThemeUtility.FromTheme( ThemeKeys.Spacing );

        // Assert
        Assert.Equal( ThemeKeys.Spacing, themeReference.Key );
    }

    [Fact]
    public void ThemeKeys_MatchDefaultThemeKeys()
    {
        // Arrange
        var themeKeys = typeof( ThemeKeys )
            .GetFields( BindingFlags.Public | BindingFlags.Static )
            .Where( field => field.IsLiteral )
            .Select( field => (string)field.GetRawConstantValue()! );

        // Act
        var defaultThemeKeys = new TwMergeConfig().Theme.Keys;

        // Assert
        Assert.Equal( defaultThemeKeys.Order(), themeKeys.Order() );
    }
}