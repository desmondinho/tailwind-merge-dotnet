using TailwindMerge.Common;

namespace TailwindMerge.Tests;

public class ValidatorsTests
{
    [Theory]
    [InlineData( "1" )]
    [InlineData( "1023713" )]
    [InlineData( "1.5" )]
    [InlineData( "1231.503761" )]
    [InlineData( "px" )]
    [InlineData( "full" )]
    [InlineData( "screen" )]
    [InlineData( "1/2" )]
    [InlineData( "123/345" )]
    public void IsLength_LengthValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsLength( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "[3.7%]" )]
    [InlineData( "[481px]" )]
    [InlineData( "[19.1rem]" )]
    [InlineData( "[50vw]" )]
    [InlineData( "[56vh]" )]
    [InlineData( "[length:var(--arbitrary)]" )]
    [InlineData( "1d5" )]
    [InlineData( "[12px" )]
    [InlineData( "12px]" )]
    [InlineData( "one" )]
    public void IsLength_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsLength( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "[3.7%]" )]
    [InlineData( "[481px]" )]
    [InlineData( "[19.1rem]" )]
    [InlineData( "[50vw]" )]
    [InlineData( "[56vh]" )]
    [InlineData( "[length:var(--arbitrary)]" )]
    public void IsArbitraryLength_ArbitraryLengthValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsArbitraryLength( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "1" )]
    [InlineData( "3px" )]
    [InlineData( "1d5" )]
    [InlineData( "[1]" )]
    [InlineData( "[12px" )]
    [InlineData( "12px]" )]
    [InlineData( "one" )]
    public void IsArbitraryLength_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsArbitraryLength( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "1" )]
    [InlineData( "123" )]
    [InlineData( "8312" )]
    public void IsInteger_IntegerValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsInteger( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "[8312]" )]
    [InlineData( "[2]" )]
    [InlineData( "[8312px]" )]
    [InlineData( "[8312%]" )]
    [InlineData( "[8312rem]" )]
    [InlineData( "8312.2" )]
    [InlineData( "1.2" )]
    [InlineData( "one" )]
    [InlineData( "1/2" )]
    [InlineData( "1%" )]
    [InlineData( "1px" )]
    public void IsInteger_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsInteger( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "[1]" )]
    [InlineData( "[bla]" )]
    [InlineData( "[not-an-arbitrary-value?]" )]
    [InlineData( "[auto,auto,minmax(0,1fr),calc(100vw-50%)]" )]
    public void IsArbitraryValue_ArbitraryValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsArbitraryValue( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "[]" )]
    [InlineData( "[1" )]
    [InlineData( "1]" )]
    [InlineData( "1" )]
    [InlineData( "one" )]
    [InlineData( "o[n]e" )]
    public void IsArbitraryValue_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsArbitraryValue( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "" )]
    [InlineData( "something" )]
    public void IsAny_AnyValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsAny( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "xs" )]
    [InlineData( "sm" )]
    [InlineData( "md" )]
    [InlineData( "lg" )]
    [InlineData( "xl" )]
    [InlineData( "2xl" )]
    [InlineData( "2.5xl" )]
    [InlineData( "10xl" )]
    [InlineData( "2xs" )]
    [InlineData( "2lg" )]
    public void IsTshirtSize_TshirtSizeValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsTshirtSize( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "" )]
    [InlineData( "hello" )]
    [InlineData( "1" )]
    [InlineData( "xl3" )]
    [InlineData( "2xl3" )]
    [InlineData( "-xl" )]
    [InlineData( "[sm]" )]
    public void IsTshirtSize_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsTshirtSize( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "[position:2px]" )]
    [InlineData( "[position:bla]" )]
    public void IsArbitraryPosition_ArbitraryPositionValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsArbitraryPosition( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "[2px]" )]
    [InlineData( "[bla]" )]
    [InlineData( "position:2px" )]
    public void IsArbitraryPosition_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsArbitraryPosition( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "[url:var(--my-url)]" )]
    [InlineData( "[url(something)]" )]
    [InlineData( "[url:bla]" )]
    [InlineData( "[image:bla]" )]
    [InlineData( "[linear-gradient(something)]" )]
    [InlineData( "[repeating-conic-gradient(something)]" )]
    public void IsArbitraryImage_ArbitraryImageValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsArbitraryImage( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "[var(--my-url)]" )]
    [InlineData( "[bla]" )]
    [InlineData( "url:2px" )]
    [InlineData( "url(2px)" )]
    public void IsArbitraryImage_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsArbitraryImage( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "[number:black]" )]
    [InlineData( "[number:bla]" )]
    [InlineData( "[number:230]" )]
    [InlineData( "[450]" )]
    public void IsArbitraryNumber_ArbitraryNumberValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsArbitraryNumber( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "[2px]" )]
    [InlineData( "[bla]" )]
    [InlineData( "[black]" )]
    [InlineData( "black" )]
    [InlineData( "450" )]
    public void IsArbitraryNumber_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsArbitraryNumber( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "[0_35px_60px_-15px_rgba(0,0,0,0.3)]" )]
    [InlineData( "[inset_0_1px_0,inset_0_-1px_0]" )]
    [InlineData( "[0_0_#00f]" )]
    [InlineData( "[.5rem_0_rgba(5,5,5,5)]" )]
    [InlineData( "[-.5rem_0_#123456]" )]
    [InlineData( "[0.5rem_-0_#123456]" )]
    [InlineData( "[0.5rem_-0.005vh_#123456]" )]
    [InlineData( "[0.5rem_-0.005vh]" )]
    public void IsArbitraryShadow_ArbitraryShadowValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsArbitraryShadow( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "[rgba(5,5,5,5)]" )]
    [InlineData( "[#00f]" )]
    [InlineData( "[something-else]" )]
    public void IsArbitraryShadow_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsArbitraryShadow( value );

        // Assert
        Assert.False( actual );
    }

    [Theory]
    [InlineData( "1%" )]
    [InlineData( "100.001%" )]
    [InlineData( ".01%" )]
    [InlineData( "0%" )]
    public void IsPercent_PercentValue_ReturnsTrue( string value )
    {
        // Act
        var actual = Validators.IsPercent( value );

        // Assert
        Assert.True( actual );
    }

    [Theory]
    [InlineData( "0" )]
    [InlineData( "one%" )]
    public void IsPercent_InvalidValue_ReturnsFalse( string value )
    {
        // Act
        var actual = Validators.IsPercent( value );

        // Assert
        Assert.False( actual );
    }
}