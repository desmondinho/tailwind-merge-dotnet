namespace TailwindMerge.Tests;

public class TailwindCssVersionsTests
{
	[Theory]
	[InlineData( "text-red text-lg/7 text-lg/8", "text-red text-lg/8" )]
	[InlineData(
		"start-0 start-1 " +
		"end-0 end-1 " +
		"ps-0 ps-1 pe-0 pe-1 " +
		"ms-0 ms-1 me-0 me-1 " +
		"rounded-s-sm rounded-s-md rounded-e-sm rounded-e-md " +
		"rounded-ss-sm rounded-ss-md rounded-ee-sm rounded-ee-md",

		"start-1 end-1 ps-1 pe-1 ms-1 me-1 rounded-s-md rounded-e-md rounded-ss-md rounded-ee-md"
	)]
	[InlineData(
		"start-0 end-0 inset-0 ps-0 pe-0 p-0 ms-0 me-0 m-0 rounded-ss rounded-es rounded-s",
		"inset-0 p-0 m-0 rounded-s"
	)]
	[InlineData( "hyphens-auto hyphens-manual", "hyphens-manual" )]
	[InlineData(
		"from-0% from-10% from-[12.5%] via-0% via-10% via-[12.5%] to-0% to-10% to-[12.5%]",
		"from-[12.5%] via-[12.5%] to-[12.5%]"
	)]
	[InlineData(
		"list-image-none list-image-[url(./my-image.png)] list-image-[var(--value)]",
		"list-image-[var(--value)]"
	)]
	[InlineData( "caption-top caption-bottom", "caption-bottom" )]
	[InlineData( "line-clamp-2 line-clamp-none line-clamp-[10]", "line-clamp-[10]" )]
	[InlineData( "delay-150 delay-0 duration-150 duration-0", "delay-0 duration-0" )]
	[InlineData( "justify-normal justify-center justify-stretch", "justify-stretch" )]
	[InlineData( "content-normal content-center content-stretch", "content-stretch" )]
	[InlineData( "whitespace-nowrap whitespace-break-spaces", "whitespace-break-spaces" )]
	public void Merge_TailwindCssV33Classes_MergesCorrectly( string classLists, string expected )
	{
		// Act
		var actual = new TwMerge().Merge( classLists );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "h-svh h-dvh w-svw w-dvw", "h-dvh w-dvw" )]
	[InlineData(
		"has-[[data-potato]]:p-1 has-[[data-potato]]:p-2 group-has-[:checked]:grid group-has-[:checked]:flex",
		"has-[[data-potato]]:p-2 group-has-[:checked]:flex"
	)]
	[InlineData( "text-wrap text-pretty", "text-pretty" )]
	[InlineData( "w-5 h-3 size-10 w-12", "size-10 w-12" )]
	[InlineData(
		"grid-cols-2 grid-cols-subgrid grid-rows-5 grid-rows-subgrid",
		"grid-cols-subgrid grid-rows-subgrid"
	)]
	[InlineData( "min-w-0 min-w-50 min-w-px max-w-0 max-w-50 max-w-px", "min-w-px max-w-px" )]
	[InlineData( "forced-color-adjust-none forced-color-adjust-auto", "forced-color-adjust-auto" )]
	[InlineData( "appearance-none appearance-auto", "appearance-auto" )]
	[InlineData( "float-start float-end clear-start clear-end", "float-end clear-end" )]
	[InlineData( "*:p-10 *:p-20 hover:*:p-10 hover:*:p-20", "*:p-20 hover:*:p-20" )]
	public void Merge_TailwindCssV34Classes_MergesCorrectly( string classLists, string expected )
	{
		// Act
		var actual = new TwMerge().Merge( classLists );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "transform-3d transform-flat", "transform-flat" )]
	[InlineData( "rotate-12 rotate-x-2 rotate-none rotate-y-3", "rotate-x-2 rotate-none rotate-y-3" )]
	[InlineData( "perspective-dramatic perspective-none perspective-midrange", "perspective-midrange" )]
	[InlineData( "perspective-origin-center perspective-origin-top-left", "perspective-origin-top-left" )]
	[InlineData( "bg-linear-to-r bg-linear-45", "bg-linear-45" )]
	[InlineData( "bg-linear-to-r bg-radial-[something] bg-conic-10", "bg-conic-10" )]
	[InlineData(
		"ring-4 ring-orange inset-ring inset-ring-3 inset-ring-blue",
		"ring-4 ring-orange inset-ring-3 inset-ring-blue"
	)]
	[InlineData( "field-sizing-content field-sizing-fixed", "field-sizing-fixed" )]
	[InlineData( "scheme-normal scheme-dark", "scheme-dark" )]
	[InlineData( "font-stretch-expanded font-stretch-[66.66%] font-stretch-50%", "font-stretch-50%" )]
	[InlineData( "col-span-full col-2 row-span-3 row-4", "col-2 row-4" )]
	[InlineData( "via-red-500 via-(--mobile-header-gradient)", "via-(--mobile-header-gradient)" )]
	[InlineData( "via-red-500 via-(length:--mobile-header-gradient)", "via-red-500 via-(length:--mobile-header-gradient)" )]
	public void Merge_TailwindCssV4Classes_MergesCorrectly( string classLists, string expected )
	{
		// Act
		var actual = new TwMerge().Merge( classLists );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
	[InlineData( "items-baseline items-baseline-last", "items-baseline-last" )]
	[InlineData( "self-baseline self-baseline-last", "self-baseline-last" )]
	[InlineData( "place-content-center place-content-end-safe place-content-center-safe", "place-content-center-safe" )]
	[InlineData( "items-center-safe items-baseline items-end-safe", "items-end-safe" )]
	[InlineData( "wrap-break-word wrap-normal wrap-anywhere", "wrap-anywhere" )]
	[InlineData( "text-shadow-none text-shadow-2xl", "text-shadow-2xl" )]
	[InlineData(
		"text-shadow-none text-shadow-md text-shadow-red text-shadow-red-500 shadow-red shadow-3xs",
		"text-shadow-md text-shadow-red-500 shadow-red shadow-3xs"
	)]
	[InlineData( "mask-add mask-subtract", "mask-subtract" )]
	[InlineData( "mask-type-luminance mask-type-alpha", "mask-type-alpha" )]
	[InlineData( "mask-clip-border mask-no-clip", "mask-no-clip" )]
	[InlineData(
		"mask-(--something) mask-[something] mask-top-left mask-center mask-(position:--var) mask-[position:1px_1px] mask-position-(--var) mask-position-[1px_1px]",
		"mask-[something] mask-position-[1px_1px]"
	)]
	[InlineData(
		"mask-(--something) mask-[something] mask-auto mask-[size:foo] mask-(size:--foo) mask-size-[foo] mask-size-(--foo) mask-cover mask-contain",
		"mask-[something] mask-contain"
	)]
	[InlineData(
		"mask-(--foo) mask-[foo] mask-none " +
		"mask-linear-1 mask-linear-2 " +
		"mask-linear-from-[position:test] mask-linear-from-3 " +
		"mask-linear-to-[position:test] mask-linear-to-3 " +
		"mask-linear-from-color-red mask-linear-from-color-3 " +
		"mask-linear-to-color-red mask-linear-to-color-3 " +
		"mask-t-from-[position:test] mask-t-from-3 " +
		"mask-t-to-[position:test] mask-t-to-3 " +
		"mask-t-from-color-red mask-t-from-color-3 " +
		"mask-radial-(--test) mask-radial-[test] " +
		"mask-radial-from-[position:test] mask-radial-from-3 " +
		"mask-radial-to-[position:test] mask-radial-to-3 " +
		"mask-radial-from-color-red mask-radial-from-color-3 ",

		"mask-none mask-linear-2 mask-linear-from-3 mask-linear-to-3 mask-linear-from-color-3 mask-linear-to-color-3 mask-t-from-3 mask-t-to-3 mask-t-from-color-3 mask-radial-[test] mask-radial-from-3 mask-radial-to-3 mask-radial-from-color-3"
	)]
	[InlineData( "shadow-md shadow-lg/25 text-shadow-md text-shadow-lg/25", "shadow-lg/25 text-shadow-lg/25" )]
	[InlineData(
		"drop-shadow-some-color drop-shadow-[#123456] drop-shadow-lg drop-shadow-[10px_0]",
		"drop-shadow-[#123456] drop-shadow-[10px_0]"
	)]
	[InlineData( "drop-shadow-[#123456] drop-shadow-some-color", "drop-shadow-some-color" )]
	[InlineData( "drop-shadow-2xl drop-shadow-[shadow:foo]", "drop-shadow-[shadow:foo]" )]
	public void Merge_TailwindCssV41Classes_MergesCorrectly( string classLists, string expected )
	{
		// Act
		var actual = new TwMerge().Merge( classLists );

		// Assert
		Assert.Equal( expected, actual );
	}

	[Theory]
    [InlineData( "inset-s-1 inset-s-2", "inset-s-2" )]
    [InlineData( "inset-e-1 inset-e-2", "inset-e-2" )]
    [InlineData( "inset-bs-1 inset-bs-2", "inset-bs-2" )]
    [InlineData( "inset-be-1 inset-be-2", "inset-be-2" )]
    [InlineData( "start-1 inset-s-2", "inset-s-2" )]
    [InlineData( "inset-s-1 start-2", "start-2" )]
    [InlineData( "end-1 inset-e-2", "inset-e-2" )]
    [InlineData( "inset-e-1 end-2", "end-2" )]
    [InlineData( "inset-s-1 inset-e-2 inset-bs-3 inset-be-4 inset-0", "inset-0" )]
    [InlineData( "inset-0 inset-s-1 inset-bs-1", "inset-0 inset-s-1 inset-bs-1" )]
    [InlineData( "inset-y-1 inset-bs-2 inset-be-3", "inset-y-1 inset-bs-2 inset-be-3" )]
    [InlineData( "top-1 inset-bs-2 bottom-3 inset-be-4", "top-1 inset-bs-2 bottom-3 inset-be-4" )]

    [InlineData( "pbs-1 pbs-2", "pbs-2" )]
    [InlineData( "pbe-1 pbe-2", "pbe-2" )]
    [InlineData( "mbs-1 mbs-2", "mbs-2" )]
    [InlineData( "mbe-1 mbe-2", "mbe-2" )]
    [InlineData( "pt-1 pbs-2", "pt-1 pbs-2" )]
    [InlineData( "pb-1 pbe-2", "pb-1 pbe-2" )]
    [InlineData( "mt-1 mbs-2", "mt-1 mbs-2" )]
    [InlineData( "mb-1 mbe-2", "mb-1 mbe-2" )]
    [InlineData( "p-0 pbs-1 pbe-1", "p-0 pbs-1 pbe-1" )]
    [InlineData( "pbs-1 pbe-1 p-0", "p-0" )]
    [InlineData( "m-0 mbs-1 mbe-1", "m-0 mbs-1 mbe-1" )]
    [InlineData( "mbs-1 mbe-1 m-0", "m-0" )]
    [InlineData( "py-1 pbs-2 pbe-3", "py-1 pbs-2 pbe-3" )]
    [InlineData( "my-1 mbs-2 mbe-3", "my-1 mbs-2 mbe-3" )]

    [InlineData( "scroll-pbs-1 scroll-pbs-2", "scroll-pbs-2" )]
    [InlineData( "scroll-pbe-1 scroll-pbe-2", "scroll-pbe-2" )]
    [InlineData( "scroll-mbs-1 scroll-mbs-2", "scroll-mbs-2" )]
    [InlineData( "scroll-mbe-1 scroll-mbe-2", "scroll-mbe-2" )]
    [InlineData( "scroll-pt-1 scroll-pbs-2", "scroll-pt-1 scroll-pbs-2" )]
    [InlineData( "scroll-pb-1 scroll-pbe-2", "scroll-pb-1 scroll-pbe-2" )]
    [InlineData( "scroll-mt-1 scroll-mbs-2", "scroll-mt-1 scroll-mbs-2" )]
    [InlineData( "scroll-mb-1 scroll-mbe-2", "scroll-mb-1 scroll-mbe-2" )]
    [InlineData( "scroll-p-0 scroll-pbs-1 scroll-pbe-1", "scroll-p-0 scroll-pbs-1 scroll-pbe-1" )]
    [InlineData( "scroll-pbs-1 scroll-pbe-1 scroll-p-0", "scroll-p-0" )]
    [InlineData( "scroll-m-0 scroll-mbs-1 scroll-mbe-1", "scroll-m-0 scroll-mbs-1 scroll-mbe-1" )]
    [InlineData( "scroll-mbs-1 scroll-mbe-1 scroll-m-0", "scroll-m-0" )]
    [InlineData( "scroll-py-1 scroll-pbs-2 scroll-pbe-3", "scroll-py-1 scroll-pbs-2 scroll-pbe-3" )]
    [InlineData( "scroll-my-1 scroll-mbs-2 scroll-mbe-3", "scroll-my-1 scroll-mbs-2 scroll-mbe-3" )]

    [InlineData( "border-bs-1 border-bs-2", "border-bs-2" )]
    [InlineData( "border-be-1 border-be-2", "border-be-2" )]
    [InlineData( "border-bs-red border-bs-blue", "border-bs-blue" )]
    [InlineData( "border-be-red border-be-blue", "border-be-blue" )]
    [InlineData( "border-2 border-bs-4 border-be-6", "border-2 border-bs-4 border-be-6" )]
    [InlineData( "border-bs-4 border-be-6 border-2", "border-2" )]
    [InlineData( "border-red border-bs-blue border-be-green", "border-red border-bs-blue border-be-green" )]
    [InlineData( "border-bs-blue border-be-green border-red", "border-red" )]
    [InlineData( "border-y-2 border-bs-4 border-be-6", "border-y-2 border-bs-4 border-be-6" )]
    [InlineData( "border-t-2 border-bs-4 border-b-6 border-be-8", "border-t-2 border-bs-4 border-b-6 border-be-8" )]
    [InlineData( "border-y-red border-bs-blue border-be-green", "border-y-red border-bs-blue border-be-green" )]
    public void Merge_TailwindCssV42Classes_MergesCorrectly( string classLists, string expected )
    {
        // Act
        var actual = new TwMerge().Merge( classLists );

        // Assert
        Assert.Equal( expected, actual );
    }
}