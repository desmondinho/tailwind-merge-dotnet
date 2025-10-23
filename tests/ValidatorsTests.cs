using V = TailwindMerge.Common.Validators;

namespace TailwindMerge.Tests;

public class ValidatorsTests
{
	[Theory]
	[InlineData( "test", true )]
	[InlineData( "something", true )]
	public void IsAny( string value, bool expected )
	{
		var actual = V.IsAny( value );

		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "test", true )]
	[InlineData( "1234-hello-world", true )]
	[InlineData( "[hello", true )]
	[InlineData( "hello]", true )]
	[InlineData( "[)", true )]
	[InlineData( "(hello]", true )]

	[InlineData( "[test]", false )]
	[InlineData( "[label:test]", false )]
	[InlineData( "(test)", false )]
	[InlineData( "(label:test)", false )]
	public void IsAnyNonArbitrary( string value, bool expected )
	{
		var actual = V.IsAnyNonArbitrary( value );

		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "[url:var(--my-url)]", true )]
	[InlineData( "[url(something)]", true )]
	[InlineData( "[url:bla]", true )]
	[InlineData( "[image:bla]", true )]
	[InlineData( "[linear-gradient(something)]", true )]
	[InlineData( "[repeating-conic-gradient(something)]", true )]

	[InlineData( "[var(--my-url)]", false )]
	[InlineData( "[bla]", false )]
	[InlineData( "url:2px", false )]
	[InlineData( "url(2px)", false )]
	public void IsArbitraryImage( string value, bool expected )
	{
		var actual = V.IsArbitraryImage( value );

		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "[3.7%]", true )]
	[InlineData( "[481px]", true )]
	[InlineData( "[19.1rem]", true )]
	[InlineData( "[50vw]", true )]
	[InlineData( "[56vh]", true )]
	[InlineData( "[length:var(--arbitrary)]", true )]

	[InlineData( "1", false )]
	[InlineData( "3px", false )]
	[InlineData( "1d5", false )]
	[InlineData( "[1]", false )]
	[InlineData( "[12px", false )]
	[InlineData( "12px]", false )]
	[InlineData( "one", false )]
	public void IsArbitraryLength( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryLength( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "[number:black]", true )]
	[InlineData( "[number:bla]", true )]
	[InlineData( "[number:230]", true )]
	[InlineData( "[450]", true )]

	[InlineData( "[2px]", false )]
	[InlineData( "[bla]", false )]
	[InlineData( "[black]", false )]
	[InlineData( "black", false )]
	[InlineData( "450", false )]
	public void IsArbitraryNumber( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryNumber( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "[position:2px]", true )]
	[InlineData( "[position:bla]", true )]
	[InlineData( "[percentage:bla]", true )]

	[InlineData( "[2px]", false )]
	[InlineData( "[bla]", false )]
	[InlineData( "position:2px", false )]
	public void IsArbitraryPosition( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryPosition( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "[0_35px_60px_-15px_rgba(0,0,0,0.3)]", true )]
	[InlineData( "[inset_0_1px_0,inset_0_-1px_0]", true )]
	[InlineData( "[0_0_#00f]", true )]
	[InlineData( "[.5rem_0_rgba(5,5,5,5)]", true )]
	[InlineData( "[-.5rem_0_#123456]", true )]
	[InlineData( "[0.5rem_-0_#123456]", true )]
	[InlineData( "[0.5rem_-0.005vh_#123456]", true )]
	[InlineData( "[0.5rem_-0.005vh]", true )]

	[InlineData( "[rgba(5,5,5,5)]", false )]
	[InlineData( "[#00f]", false )]
	[InlineData( "[something-else]", false )]
	public void IsArbitraryShadow( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryShadow( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "[size:2px]", true )]
	[InlineData( "[size:bla]", true )]
	[InlineData( "[length:bla]", true )]

	[InlineData( "[2px]", false )]
	[InlineData( "[bla]", false )]
	[InlineData( "size:2px", false )]
	[InlineData( "[percentage:bla]", false )]
	public void IsArbitrarySize( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitrarySize( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "[1]", true )]
	[InlineData( "[bla]", true )]
	[InlineData( "[not-an-arbitrary-value?]", true )]
	[InlineData( "[auto,auto,minmax(0,1fr),calc(100vw-50%)]", true )]

	[InlineData( "[]", false )]
	[InlineData( "[1", false )]
	[InlineData( "1]", false )]
	[InlineData( "1", false )]
	[InlineData( "one", false )]
	[InlineData( "o[n]e", false )]
	public void IsArbitraryValue( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryValue( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "(1)", true )]
	[InlineData( "(bla)", true )]
	[InlineData( "(not-an-arbitrary-value?)", true )]
	[InlineData( "(--my-arbitrary-variable)", true )]
	[InlineData( "(label:--my-arbitrary-variable)", true )]

	[InlineData( "()", false )]
	[InlineData( "(1", false )]
	[InlineData( "1)", false )]
	[InlineData( "1", false )]
	[InlineData( "one", false )]
	[InlineData( "o(n)e", false )]
	public void IsArbitraryVariable( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryVariable( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "(family-name:test)", true )]

	[InlineData( "(other:test)", false )]
	[InlineData( "(test)", false )]
	[InlineData( "family-name:test", false )]
	public void IsArbitraryVariableFamilyName( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryVariableFamilyName( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "(image:test)", true )]
	[InlineData( "(url:test)", true )]

	[InlineData( "(other:test)", false )]
	[InlineData( "(test)", false )]
	[InlineData( "image:test", false )]
	public void IsArbitraryVariableImage( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryVariableImage( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "(length:test)", true )]

	[InlineData( "(other:test)", false )]
	[InlineData( "(test)", false )]
	[InlineData( "length:test", false )]
	public void IsArbitraryVariableLength( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryVariableLength( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "(position:test)", true )]

	[InlineData( "(other:test)", false )]
	[InlineData( "(test)", false )]
	[InlineData( "position:test", false )]
	[InlineData( "percentage:test", false )]
	public void IsArbitraryVariablePosition( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryVariablePosition( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "(shadow:test)", true )]
	[InlineData( "(test)", true )]

	[InlineData( "(other:test)", false )]
	[InlineData( "shadow:test", false )]
	public void IsArbitraryVariableShadow( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryVariableShadow( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "(size:test)", true )]
	[InlineData( "(length:test)", true )]

	[InlineData( "(other:test)", false )]
	[InlineData( "(test)", false )]
	[InlineData( "shadow:test", false )]
	[InlineData( "(percentage:test)", false )]
	public void IsArbitraryVariableSize( string value, bool expected )
	{
		// Act
		var actual = V.IsArbitraryVariableSize( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "1/2", true )]
	[InlineData( "123/209", true )]

	[InlineData( "1", false )]
	[InlineData( "1/2/3", false )]
	[InlineData( "[1/2]", false )]
	public void IsFraction( string value, bool expected )
	{
		// Act
		var actual = V.IsFraction( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "1", true )]
	[InlineData( "123", true )]
	[InlineData( "8312", true )]

	[InlineData( "[8312]", false )]
	[InlineData( "[2]", false )]
	[InlineData( "[8312px]", false )]
	[InlineData( "[8312%]", false )]
	[InlineData( "[8312rem]", false )]
	[InlineData( "8312.2", false )]
	[InlineData( "1.2", false )]
	[InlineData( "one", false )]
	[InlineData( "1/2", false )]
	[InlineData( "1%", false )]
	[InlineData( "1px", false )]
	public void IsInteger( string value, bool expected )
	{
		// Act
		var actual = V.IsInteger( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "1", true )]
	[InlineData( "123", true )]
	[InlineData( "8312", true )]
	[InlineData( "8312.2", true )]
	[InlineData( "1.2", true )]

	[InlineData( "[8312]", false )]
	[InlineData( "[2]", false )]
	[InlineData( "[8312px]", false )]
	[InlineData( "[8312%]", false )]
	[InlineData( "[8312rem]", false )]
	[InlineData( "one", false )]
	[InlineData( "1/2", false )]
	[InlineData( "1%", false )]
	[InlineData( "1px", false )]
	public void IsNumber( string value, bool expected )
	{
		// Act
		var actual = V.IsNumber( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "1%", true )]
	[InlineData( "100.001%", true )]
	[InlineData( ".01%", true )]
	[InlineData( "0%", true )]

	[InlineData( "0", false )]
	[InlineData( "one%", false )]
	public void IsPercent( string value, bool expected )
	{
		// Act
		var actual = V.IsPercent( value );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "xs", true )]
	[InlineData( "sm", true )]
	[InlineData( "md", true )]
	[InlineData( "lg", true )]
	[InlineData( "xl", true )]
	[InlineData( "2xl", true )]
	[InlineData( "2.5xl", true )]
	[InlineData( "10xl", true )]
	[InlineData( "2xs", true )]
	[InlineData( "2lg", true )]

	[InlineData( "", false )]
	[InlineData( "hello", false )]
	[InlineData( "1", false )]
	[InlineData( "xl3", false )]
	[InlineData( "2xl3", false )]
	[InlineData( "-xl", false )]
	[InlineData( "[sm]", false )]
	public void IsTshirtSize( string value, bool expected )
	{
		// Act
		var actual = V.IsTshirtSize( value );

		// Assert
		Assert.Equal( expected, actual );
	}
}