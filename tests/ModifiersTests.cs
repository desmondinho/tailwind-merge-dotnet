using Microsoft.Extensions.Options;

using Moq;

using TailwindMerge.Models;

namespace TailwindMerge.Tests;

public class ModifiersTests
{
    [Theory]
    [InlineData( "hover:block hover:inline", "hover:inline" )]
    [InlineData( "hover:block hover:focus:inline", "hover:block hover:focus:inline" )]
    [InlineData( "line-through no-underline", "no-underline" )]
    [InlineData( "hover:block hover:focus:inline focus:hover:inline", "hover:block focus:hover:inline" )]
    [InlineData( "focus-within:inline focus-within:block", "focus-within:block" )]
    public void Merge_ClassesWithPrefixModifiers_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

    [Theory]
    [InlineData( "text-lg/7 text-lg/8", "text-lg/8" )]
    [InlineData( "text-lg/none leading-9", "text-lg/none leading-9" )]
    [InlineData( "leading-9 text-lg/none", "text-lg/none" )]
    [InlineData( "w-full w-1/2", "w-1/2" )]
    public void Merge_ClassesWithPostfixModifiers_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }

	[Theory]
	[InlineData( "foo-bar foo-bar/baz", "foo-bar/baz" )]
	[InlineData( "foo-bar/baz foo-bar", "foo-bar/baz foo-bar" )]
	public void Merge_ResolvesFullClassNamesForConfiguredPostfixLookupGroups( string classLists, string expected )
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
			},
			PostfixLookupClassGroups = ["base"],
		} );

		var mockOptions = new Mock<IOptions<TwMergeConfig>>();
		mockOptions.Setup( ap => ap.Value ).Returns( config );

		// Act
		var actual = new TwMerge( mockOptions.Object ).Merge( classLists );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "c:d:e:text-lg d:c:e:text-lg", "d:c:e:text-lg" )]
	[InlineData( "before:after:text-lg before:after:text-lg", "before:after:text-lg" )]
	[InlineData( "before:after:text-lg after:before:text-lg", "before:after:text-lg after:before:text-lg" )]
	[InlineData( "x:y:before:z:text-lg y:x:before:z:text-lg", "y:x:before:z:text-lg" )]
	[InlineData( "c:d:e:block d:c:e:inline", "d:c:e:inline" )]
	[InlineData( "*:before:block *:before:inline", "*:before:inline" )]
	[InlineData( "*:before:block before:*:inline", "*:before:block before:*:inline" )]
	[InlineData( "x:y:*:z:block y:x:*:z:inline", "y:x:*:z:inline" )]
	public void Merge_ClassesWithOrderSensitiveModifiers_MergesCorrectly( string classLists, string expected )
	{
		// Act
		var actual = new TwMerge().Merge( classLists );

		// Assert
		Assert.Equal( expected, actual );
	}
}