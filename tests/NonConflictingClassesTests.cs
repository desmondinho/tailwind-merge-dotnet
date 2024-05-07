namespace TailwindMerge.Tests;

public class NonConflictingClassesTests
{
    [Theory]
    [InlineData( "border-t border-white/10", "border-t border-white/10" )]
    [InlineData( "border-t border-white", "border-t border-white" )]
    [InlineData( "text-3.5xl text-black", "text-3.5xl text-black" )]
    public void Merge_NonConflictingClasses_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}