using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

/// <summary>
/// Represents the configuration settings for the <see cref="TwMerge"/>.
/// </summary>
public class TwMergeConfig
{
	/// <summary>
	/// Gets or sets the maximum size of the LRU cache used for memoizing results.
	/// </summary>
	/// <remarks>
	/// The default is 500
	/// </remarks>
	public int CacheSize { get; set; }

	/// <summary>
	/// Gets or sets the <seealso href="https://tailwindcss.com/docs/configuration#separator">separator</seealso> 
	/// that is used to separate modifiers (e.g., screen sizes, hover, focus, etc.) from utility names.
	/// </summary>
	/// <remarks>
	/// The default is <c>:</c>
	/// </remarks>
	public string Separator { get; set; }

	/// <summary>
	/// Gets or sets the <seealso href="https://tailwindcss.com/docs/configuration#prefix">prefix</seealso> 
	/// that allows you to add a custom prefix to all of Tailwind’s generated utility classes.
	/// </summary>
	public string? Prefix { get; set; }

	/// <summary>
	/// Gets or sets the class groups of the configuration.
	/// </summary>
	public Dictionary<string, ClassGroup> ClassGroups { get; set; }

	/// <summary>
	/// Gets or sets the conflicting class groups of the configuration.
	/// </summary>
	public Dictionary<string, string[]> ConflictingClassGroups { get; set; }

	/// <summary>
	/// Gets or sets the conflicting class group modifiers of the configuration.
	/// </summary>
	public Dictionary<string, string[]> ConflictingClassGroupModifiers { get; set; }

	/// <summary>
	/// Gets or sets the theme of the configuration.
	/// </summary>
	public Dictionary<string, object[]> Theme { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TwMergeConfig"/> class.
	/// </summary>
	public TwMergeConfig()
	{
		CacheSize = 500;
		Separator = ":";

		var colors = ThemeUtility.FromTheme( "colors" );
		var spacing = ThemeUtility.FromTheme( "spacing" );
		var blur = ThemeUtility.FromTheme( "blur" );
		var brightness = ThemeUtility.FromTheme( "brightness" );
		var borderColor = ThemeUtility.FromTheme( "borderColor" );
		var borderRadius = ThemeUtility.FromTheme( "borderRadius" );
		var borderSpacing = ThemeUtility.FromTheme( "borderSpacing" );
		var borderWidth = ThemeUtility.FromTheme( "borderWidth" );
		var contrast = ThemeUtility.FromTheme( "contrast" );
		var grayscale = ThemeUtility.FromTheme( "grayscale" );
		var hueRotate = ThemeUtility.FromTheme( "hueRotate" );
		var invert = ThemeUtility.FromTheme( "invert" );
		var gap = ThemeUtility.FromTheme( "gap" );
		var gradientColorStops = ThemeUtility.FromTheme( "gradientColorStops" );
		var gradientColorStopPositions = ThemeUtility.FromTheme( "gradientColorStopPositions" );
		var inset = ThemeUtility.FromTheme( "inset" );
		var margin = ThemeUtility.FromTheme( "margin" );
		var opacity = ThemeUtility.FromTheme( "opacity" );
		var padding = ThemeUtility.FromTheme( "padding" );
		var saturate = ThemeUtility.FromTheme( "saturate" );
		var scale = ThemeUtility.FromTheme( "scale" );
		var sepia = ThemeUtility.FromTheme( "sepia" );
		var skew = ThemeUtility.FromTheme( "skew" );
		var space = ThemeUtility.FromTheme( "space" );
		var translate = ThemeUtility.FromTheme( "translate" );

		object[] any = [Validators.IsAny];
		object[] number = [Validators.IsNumber, Validators.IsArbitraryNumber];
		object[] numberAndArbitrary = [.. number, Validators.IsArbitraryValue];
		object[] spacingWithArbitrary = [Validators.IsArbitraryValue, spacing];
		object[] spacingWithAutoAndArbitrary = ["auto", Validators.IsArbitraryValue, spacing];
		object[] lengthWithEmptyAndArbitrary = ["", Validators.IsLength, Validators.IsArbitraryLength];
		object[] zeroAndEmpty = ["", "0", Validators.IsArbitraryValue];
		object[] rotate = ["none", Validators.IsInteger, Validators.IsArbitraryValue];

		string[] autoAndNone = ["auto", "none"];
		string[] align = ["start", "end", "center", "between", "around", "evenly", "stretch"];
		string[] breaks = ["auto", "avoid", "all", "avoid-page", "page", "left", "right", "column"];
		string[] blendModes = [
			"normal",
			"multiply",
			"screen",
			"overlay",
			"darken",
			"lighten",
			"color-dodge",
			"color-burn",
			"hard-light",
			"soft-light",
			"difference",
			"exclusion",
			"hue",
			"saturation",
			"color",
			"luminosity"
		];
		string[] lineStyles = ["solid", "dashed", "dotted", "double", "none"];
		string[] overscroll = ["auto", "contain", "none"];
		string[] overflow = ["auto", "hidden", "clip", "visible", "scroll"];
		string[] positions = ["bottom", "center", "left", "left-bottom", "left-top", "right", "right-bottom", "right-top", "top"];

		Theme = new( 25 )
		{
			["colors"] = [Validators.IsAny],
			["spacing"] = [Validators.IsLength, Validators.IsArbitraryLength],
			["blur"] = ["none", "", Validators.IsTshirtSize, Validators.IsArbitraryValue],
			["brightness"] = number,
			["borderColor"] = [colors],
			["borderRadius"] = ["none", "", "full", Validators.IsTshirtSize, Validators.IsArbitraryValue],
			["borderSpacing"] = spacingWithArbitrary,
			["borderWidth"] = lengthWithEmptyAndArbitrary,
			["contrast"] = number,
			["grayscale"] = zeroAndEmpty,
			["hueRotate"] = numberAndArbitrary,
			["invert"] = zeroAndEmpty,
			["gap"] = spacingWithArbitrary,
			["gradientColorStops"] = [colors],
			["gradientColorStopPositions"] = [Validators.IsPercent, Validators.IsArbitraryLength],
			["inset"] = spacingWithAutoAndArbitrary,
			["margin"] = spacingWithAutoAndArbitrary,
			["opacity"] = number,
			["padding"] = spacingWithArbitrary,
			["saturate"] = number,
			["scale"] = number,
			["sepia"] = zeroAndEmpty,
			["skew"] = numberAndArbitrary,
			["space"] = spacingWithArbitrary,
			["translate"] = spacingWithArbitrary
		};

		ClassGroups = new( 270 )
		{
			/*
            * Aspect Ratio
            * See https://tailwindcss.com/docs/aspect-ratio
            */
			["aspect"] = new ClassGroup( "aspect", ["auto", "square", "video", Validators.IsArbitraryValue] ),
			/*
             * Container
             * See https://tailwindcss.com/docs/container
             */
			["container"] = new ClassGroup( ["container"] ),
			/*
             * Columns
             * See https://tailwindcss.com/docs/columns
             */
			["columns"] = new ClassGroup( "columns", [Validators.IsTshirtSize] ),
			/*
             * Break After
             * See https://tailwindcss.com/docs/break-after
             */
			["break-after"] = new ClassGroup( "break-after", breaks ),
			/*
             * Break Before
             * See https://tailwindcss.com/docs/break-before
             */
			["break-before"] = new ClassGroup( "break-before", breaks ),
			/*
             * Break Inside
             * See https://tailwindcss.com/docs/break-inside
             */
			["break-inside"] = new ClassGroup( "break-inside", ["auto", "avoid", "avoid-page", "avoid-column"] ),
			/*
             * Box Decoration Break
             * See https://tailwindcss.com/docs/box-decoration-break
             */
			["box-decoration"] = new ClassGroup( "box-decoration", ["slice", "clone"] ),
			/*
             * Box Sizing
             * See https://tailwindcss.com/docs/box-sizing
             */
			["box"] = new ClassGroup( "box", ["border", "content"] ),
			/*
             * Display
             * See https://tailwindcss.com/docs/display
             */
			["display"] = new ClassGroup( [
				"block",
				"inline-block",
				"inline",
				"flex",
				"inline-flex",
				"table",
				"inline-table",
				"table-caption",
				"table-cell",
				"table-column",
				"table-column-group",
				"table-footer-group",
				"table-header-group",
				"table-row-group",
				"table-row",
				"flow-root",
				"grid",
				"inline-grid",
				"contents",
				"list-item",
				"hidden"] ),
			/*
             * Floats
             * See https://tailwindcss.com/docs/float
             */
			["float"] = new ClassGroup( "float", ["right", "left", "none", "start", "end"] ),
			/*
             * Clear
             * See https://tailwindcss.com/docs/clear
             */
			["clear"] = new ClassGroup( "clear", ["left", "right", "both", "none", "start", "end"] ),
			/*
             * Isolation
             * See https://tailwindcss.com/docs/isolation
             */
			["isolation"] = new ClassGroup( ["isolate", "isolation-auto"] ),
			/*
             * Object Fit
             * See https://tailwindcss.com/docs/object-fit
             */
			["object-fit"] = new ClassGroup( "object", ["contain", "cover", "fill", "none", "scale-down"] ),
			/*
             * Object Position
             * See https://tailwindcss.com/docs/object-position
             */
			["object-position"] = new ClassGroup( "object", [.. positions, Validators.IsArbitraryValue] ),
			/*
             * Overflow
             * See https://tailwindcss.com/docs/overflow
             */
			["overflow"] = new ClassGroup( "overflow", overflow ),
			/*
             * Overflow X
             * See https://tailwindcss.com/docs/overflow
             */
			["overflow-x"] = new ClassGroup( "overflow-x", overflow ),
			/*
             * Overflow Y
             * See https://tailwindcss.com/docs/overflow
             */
			["overflow-y"] = new ClassGroup( "overflow-y", overflow ),
			/*
             * Overscroll Behavior
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
			["overscroll"] = new ClassGroup( "overscroll", overscroll ),
			/*
             * Overscroll Behavior X
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
			["overscroll-x"] = new ClassGroup( "overscroll-x", overscroll ),
			/*
             * Overscroll Behavior Y
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
			["overscroll-y"] = new ClassGroup( "overscroll-y", overscroll ),
			/*
             * Position
             * See https://tailwindcss.com/docs/position
             */
			["position"] = new ClassGroup( ["static", "fixed", "absolute", "relative", "sticky"] ),
			/*
             * Top / Right / Bottom / Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["inset"] = new ClassGroup( "inset", [inset] ),
			/*
             * Right / Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["inset-x"] = new ClassGroup( "inset-x", [inset] ),
			/*
             * Top / Bottom
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["inset-y"] = new ClassGroup( "inset-y", [inset] ),
			/*
             * Start
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["start"] = new ClassGroup( "start", [inset] ),
			/*
             * End
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["end"] = new ClassGroup( "end", [inset] ),
			/*
             * Top
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["top"] = new ClassGroup( "top", [inset] ),
			/*
             * Right
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["right"] = new ClassGroup( "right", [inset] ),
			/*
             * Bottom
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["bottom"] = new ClassGroup( "bottom", [inset] ),
			/*
             * Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["left"] = new ClassGroup( "left", [inset] ),
			/*
             * Visibility
             * See https://tailwindcss.com/docs/visibility
             */
			["visibility"] = new ClassGroup( ["visible", "invisible", "collapse"] ),
			/*
             * Z-Index
             * See https://tailwindcss.com/docs/z-index
             */
			["z"] = new ClassGroup( "z", ["auto", Validators.IsInteger, Validators.IsArbitraryValue] ),
			/*
             * Flex Basis
             * See https://tailwindcss.com/docs/flex-basis
             */
			["basis"] = new ClassGroup( "basis", spacingWithAutoAndArbitrary ),
			/*
             * Flex Direction
             * See https://tailwindcss.com/docs/flex-direction
             */
			["flex-direction"] = new ClassGroup( "flex", ["row", "row-reverse", "col", "col-reverse"] ),
			/*
             * Flex Wrap
             * See https://tailwindcss.com/docs/flex-wrap
             */
			["flex-wrap"] = new ClassGroup( "flex", ["wrap", "wrap-reverse", "nowrap"] ),
			/*
             * Flex
             * See https://tailwindcss.com/docs/flex
             */
			["flex"] = new ClassGroup( "flex", ["1", "auto", "initial", "none", Validators.IsArbitraryValue] ),
			/*
             * Flex Grow
             * See https://tailwindcss.com/docs/flex-grow
             */
			["grow"] = new ClassGroup( "grow", zeroAndEmpty ),
			/*
             * Flex Shrink
             * See https://tailwindcss.com/docs/flex-shrink
             */
			["shrink"] = new ClassGroup( "shrink", zeroAndEmpty ),
			/*
             * Order
             * See https://tailwindcss.com/docs/order
             */
			["order"] = new ClassGroup( "order", ["first", "last", "none", Validators.IsInteger, Validators.IsArbitraryValue] ),
			/*
             * Grid Template Columns
             * See https://tailwindcss.com/docs/grid-template-columns
             */
			["grid-cols"] = new ClassGroup( "grid-cols", any ),
			/*
             * Grid Column Start / End
             * See https://tailwindcss.com/docs/grid-column
             */
			["col-start-end"] = new ClassGroup( "col", [
				"auto",
				new ClassGroup( "span", ["full", .. numberAndArbitrary] ),
				Validators.IsArbitraryValue] ),
			/*
             * Grid Column Start
             * See https://tailwindcss.com/docs/grid-column
             */
			["col-start"] = new ClassGroup( "col-start", ["auto", .. numberAndArbitrary] ),
			/*
             * Grid Column End
             * See https://tailwindcss.com/docs/grid-column
             */
			["col-end"] = new ClassGroup( "col-end", ["auto", .. numberAndArbitrary] ),
			/*
             * Grid Template Rows
             * See https://tailwindcss.com/docs/grid-template-rows
             */
			["grid-rows"] = new ClassGroup( "grid-rows", any ),
			/*
             * Grid Row Start / End
             * See https://tailwindcss.com/docs/grid-row
             */
			["row-start-end"] = new ClassGroup( "row", [
				"auto",
				new ClassGroup( "span", numberAndArbitrary ),
				Validators.IsArbitraryValue] ),
			/*
             * Grid Row Start
             * See https://tailwindcss.com/docs/grid-row
             */
			["row-start"] = new ClassGroup( "row-start", ["auto", .. numberAndArbitrary] ),
			/*
             * Grid Row End
             * See https://tailwindcss.com/docs/grid-row
             */
			["row-end"] = new ClassGroup( "row-end", ["auto", .. numberAndArbitrary] ),
			/*
             * Grid Auto Flow
             * See https://tailwindcss.com/docs/grid-auto-flow
             */
			["grid-flow"] = new ClassGroup( "grid-flow", ["row", "col", "dense", "row-dense", "col-dense"] ),
			/*
             * Grid Auto Columns
             * See https://tailwindcss.com/docs/grid-auto-columns
             */
			["auto-cols"] = new ClassGroup( "auto-cols", ["auto", "min", "max", "fr", Validators.IsArbitraryValue] ),
			/*
             * Grid Auto Rows
             * See https://tailwindcss.com/docs/grid-auto-rows
             */
			["auto-rows"] = new ClassGroup( "auto-rows", ["auto", "min", "max", "fr", Validators.IsArbitraryValue] ),
			/*
             * Gap
             * See https://tailwindcss.com/docs/gap
             */
			["gap"] = new ClassGroup( "gap", [gap] ),
			/*
             * Gap X
             * See https://tailwindcss.com/docs/gap
             */
			["gap-x"] = new ClassGroup( "gap-x", [gap] ),
			/*
             * Gap Y
             * See https://tailwindcss.com/docs/gap
             */
			["gap-y"] = new ClassGroup( "gap-y", [gap] ),
			/*
             * Justify Content
             * See https://tailwindcss.com/docs/justify-content
             */
			["justify-content"] = new ClassGroup( "justify", ["normal", .. align] ),
			/*
             * Justify Items
             * See https://tailwindcss.com/docs/justify-items
             */
			["justify-items"] = new ClassGroup( "justify-items", ["start", "end", "center", "stretch"] ),
			/*
             * Justify Self
             * See https://tailwindcss.com/docs/justify-self
             */
			["justify-self"] = new ClassGroup( "justify-self", ["auto", "start", "end", "center", "stretch"] ),
			/*
             * Align Content
             * See https://tailwindcss.com/docs/align-content
             */
			["align-content"] = new ClassGroup( "content", ["normal", "baseline", .. align] ),
			/*
             * Align Items
             * See https://tailwindcss.com/docs/align-items
             */
			["align-items"] = new ClassGroup( "items", ["start", "end", "center", "baseline", "stretch"] ),
			/*
             * Align Self
             * See https://tailwindcss.com/docs/align-self
             */
			["align-self"] = new ClassGroup( "self", ["auto", "start", "end", "center", "baseline", "stretch"] ),
			/*
             * Place Content
             * See https://tailwindcss.com/docs/place-content
             */
			["place-content"] = new ClassGroup( "place-content", ["baseline", .. align] ),
			/*
             * Place Items
             * See https://tailwindcss.com/docs/place-items
             */
			["place-items"] = new ClassGroup( "place-items", ["start", "end", "center", "baseline", "stretch"] ),
			/*
             * Place Self
             * See https://tailwindcss.com/docs/place-self
             */
			["place-self"] = new ClassGroup( "place-self", ["auto", "start", "end", "center", "stretch"] ),
			/*
             * Padding
             * See https://tailwindcss.com/docs/padding
             */
			["p"] = new ClassGroup( "p", [padding] ),
			/*
             * Padding X
             * See https://tailwindcss.com/docs/padding
             */
			["px"] = new ClassGroup( "px", [padding] ),
			/*
             * Padding Y
             * See https://tailwindcss.com/docs/padding
             */
			["py"] = new ClassGroup( "py", [padding] ),
			/*
             * Padding Start
             * See https://tailwindcss.com/docs/padding
             */
			["ps"] = new ClassGroup( "ps", [padding] ),
			/*
             * Padding End
             * See https://tailwindcss.com/docs/padding
             */
			["pe"] = new ClassGroup( "pe", [padding] ),
			/*
             * Padding Top
             * See https://tailwindcss.com/docs/padding
             */
			["pt"] = new ClassGroup( "pt", [padding] ),
			/*
             * Padding Right
             * See https://tailwindcss.com/docs/padding
             */
			["pr"] = new ClassGroup( "pr", [padding] ),
			/*
             * Padding Bottom
             * See https://tailwindcss.com/docs/padding
             */
			["pb"] = new ClassGroup( "pb", [padding] ),
			/*
             * Padding Left
             * See https://tailwindcss.com/docs/padding
             */
			["pl"] = new ClassGroup( "pl", [padding] ),
			/*
             * Margin
             * See https://tailwindcss.com/docs/margin
             */
			["m"] = new ClassGroup( "m", [margin] ),
			/*
             * Margin X
             * See https://tailwindcss.com/docs/margin
             */
			["mx"] = new ClassGroup( "mx", [margin] ),
			/*
             * Margin Y
             * See https://tailwindcss.com/docs/margin
             */
			["my"] = new ClassGroup( "my", [margin] ),
			/*
             * Margin Start
             * See https://tailwindcss.com/docs/margin
             */
			["ms"] = new ClassGroup( "ms", [margin] ),
			/*
             * Margin End
             * See https://tailwindcss.com/docs/margin
             */
			["me"] = new ClassGroup( "me", [margin] ),
			/*
             * Margin Top
             * See https://tailwindcss.com/docs/margin
             */
			["mt"] = new ClassGroup( "mt", [margin] ),
			/*
             * Margin Right
             * See https://tailwindcss.com/docs/margin
             */
			["mr"] = new ClassGroup( "mr", [margin] ),
			/*
             * Margin Bottom
             * See https://tailwindcss.com/docs/margin
             */
			["mb"] = new ClassGroup( "mb", [margin] ),
			/*
             * Margin Left
             * See https://tailwindcss.com/docs/margin
             */
			["ml"] = new ClassGroup( "ml", [margin] ),
			/*
             * Space Between X
             * See https://tailwindcss.com/docs/space
             */
			["space-x"] = new ClassGroup( "space-x", [space] ),
			/*
             * Space Between X Reverse
             * See https://tailwindcss.com/docs/space
             */
			["space-x-reverse"] = new ClassGroup( ["space-x-reverse"] ),
			/*
             * Space Between Y
             * See https://tailwindcss.com/docs/space
             */
			["space-y"] = new ClassGroup( "space-y", [space] ),
			/*
             * Space Between Y Reverse
             * See https://tailwindcss.com/docs/space
             */
			["space-y-reverse"] = new ClassGroup( ["space-y-reverse"] ),
			/*
             * Width
             * See https://tailwindcss.com/docs/width
             */
			["w"] = new ClassGroup( "w", ["auto", "min", "max", "fit", "svw", "lvw", "dvw", spacing, Validators.IsArbitraryValue] ),
			/*
             * Min-Width
             * See https://tailwindcss.com/docs/min-width
             */
			["min-w"] = new ClassGroup( "min-w", ["min", "max", "fit", spacing, Validators.IsArbitraryValue] ),
			/*
             * Max-Width
             * See https://tailwindcss.com/docs/max-width
             */
			["max-w"] = new ClassGroup( "max-w", [
				"none",
				"full",
				"min",
				"max",
				"fit",
				"prose",
				spacing,
				new ClassGroup( "screen", [Validators.IsTshirtSize] ),
				Validators.IsTshirtSize] ),
			/*
             * Height
             * See https://tailwindcss.com/docs/height
             */
			["h"] = new ClassGroup( "h", ["auto", "min", "max", "fit", "svh", "lvh", "dvh", spacing, Validators.IsArbitraryValue] ),
			/*
             * Min-Height
             * See https://tailwindcss.com/docs/min-height
             */
			["min-h"] = new ClassGroup( "min-h", ["min", "max", "fit", "svh", "lvh", "dvh", spacing, Validators.IsArbitraryValue] ),
			/*
             * Max-Height
             * See https://tailwindcss.com/docs/max-height
             */
			["max-h"] = new ClassGroup( "max-h", ["min", "max", "fit", "svh", "lvh", "dvh", spacing, Validators.IsArbitraryValue] ),
			/*
             * Size
             * See https://tailwindcss.com/docs/size
             */
			["size"] = new ClassGroup( "size", ["auto", "min", "max", "fit", spacing, Validators.IsArbitraryValue] ),
			/*
             * Font Size
             * See https://tailwindcss.com/docs/font-size
             */
			["font-size"] = new ClassGroup( "text", ["base", Validators.IsTshirtSize, Validators.IsArbitraryLength] ),
			/*
             * Font Smoothing
             * See https://tailwindcss.com/docs/font-smoothing
             */
			["font-smoothing"] = new ClassGroup( ["antialiased", "subpixel-antialiased"] ),
			/*
             * Font Style
             * See https://tailwindcss.com/docs/font-style
             */
			["font-style"] = new ClassGroup( ["italic", "not-italic"] ),
			/*
             * Font Weight
             * See https://tailwindcss.com/docs/font-weight
             */
			["font-weight"] = new ClassGroup( "font", [
				"thin",
				"extralight",
				"light",
				"normal",
				"medium",
				"semibold",
				"bold",
				"extrabold",
				"bold",
				Validators.IsArbitraryNumber] ),
			/*
             * Font Family
             * See https://tailwindcss.com/docs/font-family
             */
			["font-family"] = new ClassGroup( "font", any ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-normal"] = new ClassGroup( ["normal-nums"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-ordinal"] = new ClassGroup( ["ordinal"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-slashed-zero"] = new ClassGroup( ["slashed-zero"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-figure"] = new ClassGroup( ["lining-nums", "oldstyle-nums"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-spacing"] = new ClassGroup( ["proportional-nums", "tabular-nums"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-fraction"] = new ClassGroup( ["diagonal-fractions", "stacked-fractions"] ),
			/*
             * Letter Spacing
             * See https://tailwindcss.com/docs/letter-spacing
             */
			["tracking"] = new ClassGroup( "tracking", [
				"tighter",
				"tight",
				"normal",
				"wide",
				"wider",
				"widest",
				Validators.IsArbitraryValue] ),
			/*
             * Line Clamp
             * See https://tailwindcss.com/docs/line-clamp
             */
			["line-clamp"] = new ClassGroup( "line-clamp", ["none", .. number] ),
			/*
             * Line Height
             * See https://tailwindcss.com/docs/line-height
             */
			["leading"] = new ClassGroup( "leading", [
				"none",
				"tight",
				"snug",
				"normal",
				"relaxed",
				"loose",
				Validators.IsLength,
				Validators.IsArbitraryValue] ),
			/*
             * List Style Image
             * See https://tailwindcss.com/docs/list-style-image
             */
			["list-image"] = new ClassGroup( "list-image", ["none", Validators.IsArbitraryValue] ),
			/*
             * List Style Type
             * See https://tailwindcss.com/docs/list-style-type
             */
			["list-style-type"] = new ClassGroup( "list", ["none", "disc", "decimal", Validators.IsArbitraryValue] ),
			/*
             * List Style Position
             * See https://tailwindcss.com/docs/list-style-position
             */
			["list-style-position"] = new ClassGroup( "list", ["inside", "outside"] ),
			/*
             * Placeholder Color
             * See https://tailwindcss.com/docs/placeholder-color
             */
			["placeholder-color"] = new ClassGroup( "placeholder", [colors] ),
			/*
             * Text Alignment
             * See https://tailwindcss.com/docs/text-align
             */
			["text-alignment"] = new ClassGroup( "text", ["left", "center", "right", "justify", "start", "end"] ),
			/*
             * Text Color
             * See https://tailwindcss.com/docs/text-color
             */
			["text-color"] = new ClassGroup( "text", [colors] ),
			/*
             * Text Decoration
             * See https://tailwindcss.com/docs/text-decoration
             */
			["text-decoration"] = new ClassGroup( ["underline", "overline", "line-through", "no-underline"] ),
			/*
             * Text Decoration Style
             * See https://tailwindcss.com/docs/text-decoration-style
             */
			["text-decoration-style"] = new ClassGroup( "decoration", ["wavy", .. lineStyles] ),
			/*
             * Text Decoration Color
             * See https://tailwindcss.com/docs/text-decoration-color
             */
			["text-decoration-color"] = new ClassGroup( "decoration", [colors] ),
			/*
             * Text Decoration Thickness
             * See https://tailwindcss.com/docs/text-decoration-thickness
             */
			["text-decoration-thickness"] = new ClassGroup( "decoration", ["auto", "from-font", Validators.IsLength, Validators.IsArbitraryLength] ),
			/*
             * Text Underline Offset
             * See https://tailwindcss.com/docs/text-underline-offset
             */
			["underline-offset"] = new ClassGroup( "underline-offset", ["auto", Validators.IsLength, Validators.IsArbitraryValue] ),
			/*
             * Text Transform
             * See https://tailwindcss.com/docs/text-transform
             */
			["text-transform"] = new ClassGroup( ["uppercase", "lowercase", "capitalize", "normal-case"] ),
			/*
             * Text Overflow
             * See https://tailwindcss.com/docs/text-overflow
             */
			["text-overflow"] = new ClassGroup( ["truncate", "text-ellipsis", "text-clip"] ),
			/*
             * Text Wrap
             * See https://tailwindcss.com/docs/text-wrap
             */
			["text-wrap"] = new ClassGroup( "text", ["wrap", "nowrap", "balance", "pretty"] ),
			/*
             * Text Indent
             * See https://tailwindcss.com/docs/text-indent
             */
			["indent"] = new ClassGroup( "indent", spacingWithArbitrary ),
			/*
             * Vertical Alignment
             * See https://tailwindcss.com/docs/vertical-align
             */
			["vertical-align"] = new ClassGroup( "align", [
				"baseline",
				"top",
				"middle",
				"bottom",
				"text-top",
				"text-bottom",
				"sub",
				"super",
				Validators.IsArbitraryValue] ),
			/*
             * Whitespace
             * See https://tailwindcss.com/docs/whitespace
             */
			["whitespace"] = new ClassGroup( "whitespace", [
				"normal",
				"nowrap",
				"pre",
				"pre-line",
				"pre-wrap",
				"break-spaces"] ),
			/*
             * Work Break
             * See https://tailwindcss.com/docs/word-break
             */
			["break"] = new ClassGroup( "break", ["normal", "words", "all", "keep"] ),
			/*
             * Hyphens
             * See https://tailwindcss.com/docs/hyphens
             */
			["hyphens"] = new ClassGroup( "hyphens", ["none", "manual", "auto"] ),
			/*
             * Content
             * See https://tailwindcss.com/docs/content
             */
			["content"] = new ClassGroup( "content", ["none", Validators.IsArbitraryValue] ),
			/*
             * Background Attachment
             * See https://tailwindcss.com/docs/background-attachment
             */
			["bg-attachment"] = new ClassGroup( "bg", ["fixed", "local", "scroll"] ),
			/*
             * Background Clip
             * See https://tailwindcss.com/docs/background-clip
             */
			["bg-clip"] = new ClassGroup( "bg-clip", ["border", "padding", "content", "text"] ),
			/*
             * Background Origin
             * See https://tailwindcss.com/docs/background-origin
             */
			["bg-origin"] = new ClassGroup( "bg-origin", ["border", "padding", "content"] ),
			/*
             * Background Position
             * See https://tailwindcss.com/docs/background-position
             */
			["bg-position"] = new ClassGroup( "bg", [.. positions, Validators.IsArbitraryPosition] ),
			/*
             * Background Repeat
             * See https://tailwindcss.com/docs/background-repeat
             */
			["bg-repeat"] = new ClassGroup( "bg", [
				"no-repeat",
				new ClassGroup( "repeat", ["", "x", "y", "round", "space"] )] ),
			/*
             * Background Size
             * See https://tailwindcss.com/docs/background-size
             */
			["bg-size"] = new ClassGroup( "bg", ["auto", "cover", "contain", Validators.IsArbitrarySize] ),
			/*
             * Background Image
             * See https://tailwindcss.com/docs/background-image
             */
			["bg-image"] = new ClassGroup( "bg", [
				"none",
				new ClassGroup( "gradient-to", ["t", "tr", "r", "br", "b", "bl", "l", "tl"] ),
				Validators.IsArbitraryImage] ),
			/*
             * Background Color
             * See https://tailwindcss.com/docs/background-color
             */
			["bg-color"] = new ClassGroup( "bg", [colors] ),
			/*
             * Gradient Color Stops From Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-from-pos"] = new ClassGroup( "from", [gradientColorStopPositions] ),
			/*
             * Gradient Color Stops Via Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-via-pos"] = new ClassGroup( "via", [gradientColorStopPositions] ),
			/*
             * Gradient Color Stops To Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-to-pos"] = new ClassGroup( "to", [gradientColorStopPositions] ),
			/*
             * Gradient Color Stops From
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-from"] = new ClassGroup( "from", [gradientColorStops] ),
			/*
             * Gradient Color Stops Via
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-via"] = new ClassGroup( "via", [gradientColorStops] ),
			/*
             * Gradient Color Stops To
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-to"] = new ClassGroup( "to", [gradientColorStops] ),
			/*
             * Border Radius
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded"] = new ClassGroup( "rounded", [borderRadius] ),
			/*
             * Border Radius Start
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-s"] = new ClassGroup( "rounded-s", [borderRadius] ),
			/*
             * Border Radius End
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-e"] = new ClassGroup( "rounded-e", [borderRadius] ),
			/*
             * Border Radius Top
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-t"] = new ClassGroup( "rounded-t", [borderRadius] ),
			/*
             * Border Radius Right
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-r"] = new ClassGroup( "rounded-r", [borderRadius] ),
			/*
             * Border Radius Bottom
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-b"] = new ClassGroup( "rounded-b", [borderRadius] ),
			/*
             * Border Radius Left
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-l"] = new ClassGroup( "rounded-l", [borderRadius] ),
			/*
             * Border Radius Start Start
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-ss"] = new ClassGroup( "rounded-ss", [borderRadius] ),
			/*
             * Border Radius Start End
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-se"] = new ClassGroup( "rounded-se", [borderRadius] ),
			/*
             * Border Radius End End
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-ee"] = new ClassGroup( "rounded-ee", [borderRadius] ),
			/*
             * Border Radius End Start
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-es"] = new ClassGroup( "rounded-es", [borderRadius] ),
			/*
             * Border Radius Top Left
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-tl"] = new ClassGroup( "rounded-tl", [borderRadius] ),
			/*
             * Border Radius Top Right
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-tr"] = new ClassGroup( "rounded-tr", [borderRadius] ),
			/*
             * Border Radius Bottom Right
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-br"] = new ClassGroup( "rounded-br", [borderRadius] ),
			/*
             * Border Radius Bottom Left
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-bl"] = new ClassGroup( "rounded-bl", [borderRadius] ),
			/*
             * Border Width
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w"] = new ClassGroup( "border", [borderWidth] ),
			/*
             * Border Width X
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-x"] = new ClassGroup( "border-x", [borderWidth] ),
			/*
             * Border Width Y
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-y"] = new ClassGroup( "border-y", [borderWidth] ),
			/*
             * Border Width Start
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-s"] = new ClassGroup( "border-s", [borderWidth] ),
			/*
             * Border Width End
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-e"] = new ClassGroup( "border-e", [borderWidth] ),
			/*
             * Border Width Top
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-t"] = new ClassGroup( "border-t", [borderWidth] ),
			/*
             * Border Width Right
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-r"] = new ClassGroup( "border-r", [borderWidth] ),
			/*
             * Border Width Bottom
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-b"] = new ClassGroup( "border-b", [borderWidth] ),
			/*
             * Border Width Left
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-l"] = new ClassGroup( "border-l", [borderWidth] ),
			/*
             * Border Style
             * See https://tailwindcss.com/docs/border-style
             */
			["border-style"] = new ClassGroup( "border", ["hidden", .. lineStyles] ),
			/*
             * Divide Width X
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-x"] = new ClassGroup( "divide-x", [borderWidth] ),
			/*
             * Divide Width X Reverse
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-x-reverse"] = new ClassGroup( ["divide-x-reverse"] ),
			/*
             * Divide Width Y
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-y"] = new ClassGroup( "divide-y", [borderWidth] ),
			/*
             * Divide Width Y Reverse
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-y-reverse"] = new ClassGroup( ["divide-y-reverse"] ),
			/*
             * Divide Style
             * See https://tailwindcss.com/docs/divide-style
             */
			["divide-style"] = new ClassGroup( "divide", lineStyles ),
			/*
             * Border Color
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color"] = new ClassGroup( "border", [borderColor] ),
			/*
             * Border Color X
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-x"] = new ClassGroup( "border-x", [borderColor] ),
			/*
             * Border Color Y
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-y"] = new ClassGroup( "border-y", [borderColor] ),
			/*
             * Border Color Top
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-t"] = new ClassGroup( "border-t", [borderColor] ),
			/*
             * Border Color Right
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-r"] = new ClassGroup( "border-r", [borderColor] ),
			/*
             * Border Color Bottom
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-b"] = new ClassGroup( "border-b", [borderColor] ),
			/*
             * Border Color Left
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-l"] = new ClassGroup( "border-l", [borderColor] ),
			/*
             * Divide Color
             * See https://tailwindcss.com/docs/divide-color
             */
			["divide-color"] = new ClassGroup( "divide", [borderColor] ),
			/*
             * Outline Style
             * See https://tailwindcss.com/docs/outline-style
             */
			["outline-style"] = new ClassGroup( "outline", ["", .. lineStyles] ),
			/*
             * Outline Offset
             * See https://tailwindcss.com/docs/outline-offset
             */
			["outline-offset"] = new ClassGroup( "outline-offset", [Validators.IsLength, Validators.IsArbitraryLength] ),
			/*
             * Outline Width
             * See https://tailwindcss.com/docs/outline-width
             */
			["outline-w"] = new ClassGroup( "outline", [Validators.IsLength, Validators.IsArbitraryLength] ),
			/*
             * Outline Color
             * See https://tailwindcss.com/docs/outline-color
             */
			["outline-color"] = new ClassGroup( "outline", [colors] ),
			/*
             * Ring Width
             * See https://tailwindcss.com/docs/ring-width
             */
			["ring-w"] = new ClassGroup( "ring", lengthWithEmptyAndArbitrary ),
			/*
             * Ring Width Inset
             * See https://tailwindcss.com/docs/ring-width
             */
			["ring-w-inset"] = new ClassGroup( ["ring-inset"] ),
			/*
             * Ring Color
             * See https://tailwindcss.com/docs/ring-color
             */
			["ring-color"] = new ClassGroup( "ring", [colors] ),
			/*
             * Ring Offset Width
             * See https://tailwindcss.com/docs/ring-offset-width
             */
			["ring-offset-w"] = new ClassGroup( "ring-offset", [Validators.IsLength, Validators.IsArbitraryLength] ),
			/*
             * Ring Offset Color
             * See https://tailwindcss.com/docs/ring-offset-color
             */
			["ring-offset-color"] = new ClassGroup( "ring-offset", [colors] ),
			/*
             * Shadow
             * See https://tailwindcss.com/docs/shadow
             */
			["shadow"] = new ClassGroup( "shadow", ["", "inner", "none", Validators.IsTshirtSize, Validators.IsArbitraryShadow] ),
			/*
             * Shadow Color
             * See https://tailwindcss.com/docs/shadow-color
             */
			["shadow-color"] = new ClassGroup( "shadow", any ),
			/*
             * Opacity
             * See https://tailwindcss.com/docs/opacity
             */
			["opacity"] = new ClassGroup( "opacity", [opacity] ),
			/*
             * Mix Blend Mode
             * See https://tailwindcss.com/docs/mix-blend-mode
             */
			["mix-blend"] = new ClassGroup( "mix-blend", ["plus-lighter", "plus-darker", .. blendModes] ),
			/*
             * Background Blend Mode
             * See https://tailwindcss.com/docs/mix-blend-mode
             */
			["bg-blend"] = new ClassGroup( "bg-blend", blendModes ),
			/*
             * Blur
             * See https://tailwindcss.com/docs/blur
             */
			["blur"] = new ClassGroup( "blur", [blur] ),
			/*
             * Brightness
             * See https://tailwindcss.com/docs/brightness
             */
			["brightness"] = new ClassGroup( "brightness", [brightness] ),
			/*
             * Contrast
             * See https://tailwindcss.com/docs/contrast
             */
			["contrast"] = new ClassGroup( "contrast", [contrast] ),
			/*
             * Drop Shadow
             * See https://tailwindcss.com/docs/drop-shadow
             */
			["drop-shadow"] = new ClassGroup( "drop-shadow", ["none", "", Validators.IsTshirtSize, Validators.IsArbitraryValue] ),
			/*
             * Grayscale
             * See https://tailwindcss.com/docs/grayscale
             */
			["grayscale"] = new ClassGroup( "grayscale", [grayscale] ),
			/*
             * Hue Rotate
             * See https://tailwindcss.com/docs/hue-rotate
             */
			["hue-rotate"] = new ClassGroup( "hue-rotate", [hueRotate] ),
			/*
             * Invert
             * See https://tailwindcss.com/docs/invert
             */
			["invert"] = new ClassGroup( "invert", [invert] ),
			/*
             * Saturate
             * See https://tailwindcss.com/docs/saturate
             */
			["saturate"] = new ClassGroup( "saturate", [saturate] ),
			/*
             * Sepia
             * See https://tailwindcss.com/docs/sepia
             */
			["sepia"] = new ClassGroup( "sepia", [sepia] ),
			/*
             * Backdrop Blur
             * See https://tailwindcss.com/docs/backdrop-blur
             */
			["backdrop-blur"] = new ClassGroup( "backdrop-blur", [blur] ),
			/*
             * Backdrop Brightness
             * See https://tailwindcss.com/docs/backdrop-brightness
             */
			["backdrop-brightness"] = new ClassGroup( "backdrop-brightness", [brightness] ),
			/*
             * Backdrop Contrast
             * See https://tailwindcss.com/docs/backdrop-contrast
             */
			["backdrop-contrast"] = new ClassGroup( "backdrop-contrast", [contrast] ),
			/*
             * Backdrop Grayscale
             * See https://tailwindcss.com/docs/backdrop-grayscale
             */
			["backdrop-grayscale"] = new ClassGroup( "backdrop-grayscale", [grayscale] ),
			/*
             * Backdrop Hue Rotate
             * See https://tailwindcss.com/docs/backdrop-hue-rotate
             */
			["backdrop-hue-rotate"] = new ClassGroup( "backdrop-hue-rotate", [hueRotate] ),
			/*
             * Backdrop Invert
             * See https://tailwindcss.com/docs/backdrop-invert
             */
			["backdrop-invert"] = new ClassGroup( "backdrop-invert", [invert] ),
			/*
             * Backdrop Opacity
             * See https://tailwindcss.com/docs/backdrop-opacity
             */
			["backdrop-opacity"] = new ClassGroup( "backdrop-opacity", [opacity] ),
			/*
             * Backdrop Saturate
             * See https://tailwindcss.com/docs/backdrop-saturate
             */
			["backdrop-saturate"] = new ClassGroup( "backdrop-saturate", [saturate] ),
			/*
             * Backdrop Sepia
             * See https://tailwindcss.com/docs/backdrop-sepia
             */
			["backdrop-sepia"] = new ClassGroup( "backdrop-sepia", [sepia] ),
			/*
             * Border Collapse
             * See https://tailwindcss.com/docs/border-collapse
             */
			["border-collapse"] = new ClassGroup( "border", ["collapse", "separate"] ),
			/*
             * Border Spacing
             * See https://tailwindcss.com/docs/border-spacing
             */
			["border-spacing"] = new ClassGroup( "border-spacing", [borderSpacing] ),
			/*
             * Border Spacing X
             * See https://tailwindcss.com/docs/border-spacing
             */
			["border-spacing-x"] = new ClassGroup( "border-spacing-x", [borderSpacing] ),
			/*
             * Border Spacing Y
             * See https://tailwindcss.com/docs/border-spacing
             */
			["border-spacing-y"] = new ClassGroup( "border-spacing-y", [borderSpacing] ),
			/*
             * Table Layout
             * See https://tailwindcss.com/docs/table-layout
             */
			["table-layout"] = new ClassGroup( "table", ["auto", "fixed"] ),
			/*
             * Caption Side
             * See https://tailwindcss.com/docs/caption-side
             */
			["caption"] = new ClassGroup( "caption", ["top", "bottom"] ),
			/*
             * Transition Property
             * See https://tailwindcss.com/docs/transition-property
             */
			["transition"] = new ClassGroup( "transition", ["none", "all", "", "colors", "opacity", "shadow", "transform", Validators.IsArbitraryValue] ),
			/*
             * Transition Duration
             * See https://tailwindcss.com/docs/transition-duration
             */
			["duration"] = new ClassGroup( "duration", numberAndArbitrary ),
			/*
             * Transition Timing Function
             * See https://tailwindcss.com/docs/transition-timing-function
             */
			["ease"] = new ClassGroup( "ease", ["linear", "in", "out", "in-out", Validators.IsArbitraryValue] ),
			/*
             * Transition Delay
             * See https://tailwindcss.com/docs/transition-delay
             */
			["delay"] = new ClassGroup( "delay", numberAndArbitrary ),
			/*
             * Animation
             * See https://tailwindcss.com/docs/animation
             */
			["animate"] = new ClassGroup( "animate", ["none", "spin", "ping", "pulse", "bounce", Validators.IsArbitraryValue] ),
			/*
             * Perspective
             * See https://tailwindcss.com/docs/perspective
             */
			["perspective"] = new ClassGroup( "perspective", [
				"dramatic", 
				"near", 
				"normal", 
				"midrange", 
				"distant", 
				"none", 
				Validators.IsArbitraryValue] ),
			/*
             * Perspective Origin
             * See https://tailwindcss.com/docs/perspective-origin
             */
			["perspective-origin"] = new ClassGroup( "perspective-origin", [
				"center",
				"top",
				"top-right",
				"right",
				"bottom-right",
				"bottom", 
				"bottom-left", 
				"left", 
				"top-left", 
				Validators.IsArbitraryValue] ),
			/*
             * Transform
             * See https://tailwindcss.com/docs/transform
             */
			["transform"] = new ClassGroup( "transform", ["", "gpu", "none"] ),
			/*
             * Scale
             * See https://tailwindcss.com/docs/scale
             */
			["scale"] = new ClassGroup( "scale", [scale] ),
			/*
             * Scale X
             * See https://tailwindcss.com/docs/scale
             */
			["scale-x"] = new ClassGroup( "scale-x", [scale] ),
			/*
             * Scale Y
             * See https://tailwindcss.com/docs/scale
             */
			["scale-y"] = new ClassGroup( "scale-y", [scale] ),
			/*
             * Rotate
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate"] = new ClassGroup( "rotate", rotate ),
			/*
             * Rotate X
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate-x"] = new ClassGroup( "rotate-x", rotate ),
			/*
             * Rotate Y
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate-y"] = new ClassGroup( "rotate-y", rotate ),
			/*
             * Rotate Z
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate-z"] = new ClassGroup( "rotate-z", rotate ),
			/*
             * Translate X
             * See https://tailwindcss.com/docs/translate
             */
			["translate-x"] = new ClassGroup( "translate-x", [translate] ),
			/*
             * Translate Y
             * See https://tailwindcss.com/docs/translate
             */
			["translate-y"] = new ClassGroup( "translate-y", [translate] ),
			/*
             * Skew X
             * See https://tailwindcss.com/docs/skew
             */
			["skew-x"] = new ClassGroup( "skew-x", [skew] ),
			/*
             * Skew Y
             * See https://tailwindcss.com/docs/skew
             */
			["skew-y"] = new ClassGroup( "skew-y", [skew] ),
			/*
             * Transform Origin
             * See https://tailwindcss.com/docs/transform-origin
             */
			["transform-origin"] = new ClassGroup( "origin", [
				"center",
				"top",
				"top-right",
				"right",
				"bottom-right",
				"bottom",
				"bottom-left",
				"left",
				"top-left",
				Validators.IsArbitraryValue] ),
			/*
             * Transform Style
             * See https://tailwindcss.com/docs/transform-style
             */
			["transform-style"] = new ClassGroup( "transform", ["3d", "flat"] ),
			/*
             * Accent
             * See https://tailwindcss.com/docs/accent
             */
			["accent"] = new ClassGroup( "accent", ["auto", colors] ),
			/*
             * Appearance
             * See https://tailwindcss.com/docs/appearance
             */
			["appearance"] = new ClassGroup( "appearance", autoAndNone ),
			/*
             * Cursor
             * See https://tailwindcss.com/docs/cursor
             */
			["cursor"] = new ClassGroup( "cursor", [
				"auto",
				"default",
				"pointer",
				"wait",
				"text",
				"move",
				"help",
				"not-allowed",
				"none",
				"context-menu",
				"progress",
				"cell",
				"crosshair",
				"vertical-text",
				"alias",
				"copy",
				"no-drop",
				"grab",
				"grabbing",
				"all-scroll",
				"col-resize",
				"row-resize",
				"n-resize",
				"e-resize",
				"s-resize",
				"w-resize",
				"ne-resize",
				"nw-resize",
				"se-resize",
				"sw-resize",
				"ew-resize",
				"ns-resize",
				"nesw-resize",
				"nwse-resize",
				"zoom-in",
				"zoom-out",
				Validators.IsArbitraryValue] ),
			/*
             * Caret Color
             * See https://tailwindcss.com/docs/caret-color
             */
			["caret-color"] = new ClassGroup( "caret", [colors] ),
			/*
             * Pointer Events
             * See https://tailwindcss.com/docs/pointer-events
             */
			["pointer-events"] = new ClassGroup( "pointer-events", autoAndNone ),
			/*
             * Resize
             * See https://tailwindcss.com/docs/resize
             */
			["resize"] = new ClassGroup( "resize", ["none", "y", "x", ""] ),
			/*
             * Scroll Behavior
             * See https://tailwindcss.com/docs/scroll-behavior
             */
			["scroll-behavior"] = new ClassGroup( "scroll", ["auto", "smooth"] ),
			/*
             * Scroll Margin
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-m"] = new ClassGroup( "scroll-m", spacingWithArbitrary ),
			/*
             * Scroll Margin X
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mx"] = new ClassGroup( "scroll-mx", spacingWithArbitrary ),
			/*
             * Scroll Margin Y
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-my"] = new ClassGroup( "scroll-my", spacingWithArbitrary ),
			/*
             * Scroll Margin Start
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-ms"] = new ClassGroup( "scroll-ms", spacingWithArbitrary ),
			/*
             * Scroll Margin End
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-me"] = new ClassGroup( "scroll-me", spacingWithArbitrary ),
			/*
             * Scroll Margin Top
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mt"] = new ClassGroup( "scroll-mt", spacingWithArbitrary ),
			/*
             * Scroll Margin Right
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mr"] = new ClassGroup( "scroll-mr", spacingWithArbitrary ),
			/*
             * Scroll Margin Bottom
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mb"] = new ClassGroup( "scroll-mb", spacingWithArbitrary ),
			/*
             * Scroll Margin Left
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-ml"] = new ClassGroup( "scroll-ml", spacingWithArbitrary ),
			/*
             * Scroll Padding
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-p"] = new ClassGroup( "scroll-p", spacingWithArbitrary ),
			/*
             * Scroll Padding X
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-px"] = new ClassGroup( "scroll-px", spacingWithArbitrary ),
			/*
             * Scroll Padding Y
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-py"] = new ClassGroup( "scroll-py", spacingWithArbitrary ),
			/*
             * Scroll Padding Start
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-ps"] = new ClassGroup( "scroll-ps", spacingWithArbitrary ),
			/*
             * Scroll Padding End
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pe"] = new ClassGroup( "scroll-pe", spacingWithArbitrary ),
			/*
             * Scroll Padding Top
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pt"] = new ClassGroup( "scroll-pt", spacingWithArbitrary ),
			/*
             * Scroll Padding Right
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pr"] = new ClassGroup( "scroll-pr", spacingWithArbitrary ),
			/*
             * Scroll Padding Bottom
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pb"] = new ClassGroup( "scroll-pb", spacingWithArbitrary ),
			/*
             * Scroll Padding Left
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pl"] = new ClassGroup( "scroll-pl", spacingWithArbitrary ),
			/*
             * Scroll Snap Align
             * See https://tailwindcss.com/docs/scroll-snap-align
             */
			["snap-align"] = new ClassGroup( "snap", ["start", "end", "center", "align-none"] ),
			/*
             * Scroll Snap Stop
             * See https://tailwindcss.com/docs/scroll-snap-stop
             */
			["snap-stop"] = new ClassGroup( "snap", ["normal", "always"] ),
			/*
             * Scroll Snap Type
             * See https://tailwindcss.com/docs/scroll-snap-type
             */
			["snap-type"] = new ClassGroup( "snap", ["none", "x", "y", "both"] ),
			/*
             * Scroll Snap Type Strictness
             * See https://tailwindcss.com/docs/scroll-snap-type
             */
			["snap-strictness"] = new ClassGroup( "snap", ["mandatory", "proximity"] ),
			/*
             * Touch Action
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch"] = new ClassGroup( "touch", ["auto", "none", "manipulation"] ),
			/*
             * Touch Action X
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch-x"] = new ClassGroup( "touch-pan", ["x", "left", "right"] ),
			/*
             * Touch Action Y
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch-y"] = new ClassGroup( "touch-pan", ["y", "up", "down"] ),
			/*
             * Touch Action Pinch Zoom
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch-pz"] = new ClassGroup( ["touch-pinch-zoom"] ),
			/*
             * User Select
             * See https://tailwindcss.com/docs/user-select
             */
			["select"] = new ClassGroup( "select", ["none", "text", "all", "auto"] ),
			/*
             * Will Change
             * See https://tailwindcss.com/docs/will-change
             */
			["will-change"] = new ClassGroup( "will-change", ["auto", "scroll", "contents", "transform", Validators.IsArbitraryValue] ),
			/*
             * Fill
             * See https://tailwindcss.com/docs/fill
             */
			["fill"] = new ClassGroup( "fill", ["none", colors] ),
			/*
             * Stroke Width
             * See https://tailwindcss.com/docs/stroke-width
             */
			["stroke-w"] = new ClassGroup( "stroke", [Validators.IsLength, Validators.IsArbitraryLength, Validators.IsArbitraryNumber] ),
			/*
             * Stroke
             * See https://tailwindcss.com/docs/stroke
             */
			["stroke"] = new ClassGroup( "stroke", ["none", colors] ),
			/*
             * Screen Readers
             * See https://tailwindcss.com/docs/screen-readers
             */
			["sr"] = new ClassGroup( ["sr-only", "not-sr-only"] ),
			/*
             * Forced Color Adjust
             * See https://tailwindcss.com/docs/forced-color-adjust
             */
			["forced-color-adjust"] = new ClassGroup( "forced-color-adjust", autoAndNone )
		};

		ConflictingClassGroups = new( 46 )
		{
			["overflow"] = ["overflow-x", "overflow-y"],
			["overscroll"] = ["overscroll-x", "overscroll-y"],
			["inset"] = ["inset-x", "inset-y", "start", "end", "top", "right", "bottom", "left"],
			["inset-x"] = ["right", "left"],
			["inset-y"] = ["top", "bottom"],
			["flex"] = ["basis", "grow", "shrink"],
			["gap"] = ["gap-x", "gap-y"],
			["p"] = ["px", "py", "ps", "pe", "pt", "pr", "pb", "pl"],
			["px"] = ["pr", "pl"],
			["py"] = ["pt", "pb"],
			["m"] = ["mx", "my", "ms", "me", "mt", "mr", "mb", "ml"],
			["mx"] = ["mr", "ml"],
			["my"] = ["mt", "mb"],
			["size"] = ["w", "h"],
			["font-size"] = ["leading"],
			["fvn-normal"] = [
				"fvn-ordinal",
				"fvn-slashed-zero",
				"fvn-figure",
				"fvn-spacing",
				"fvn-fraction"
			],
			["fvn-ordinal"] = ["fvn-normal"],
			["fvn-slashed-zero"] = ["fvn-normal"],
			["fvn-figure"] = ["fvn-normal"],
			["fvn-spacing"] = ["fvn-normal"],
			["fvn-fraction"] = ["fvn-normal"],
			["line-clamp"] = ["display", "overflow"],
			["rounded"] = [
				"rounded-s",
				"rounded-e",
				"rounded-t",
				"rounded-r",
				"rounded-b",
				"rounded-l",
				"rounded-ss",
				"rounded-se",
				"rounded-ee",
				"rounded-es",
				"rounded-tl",
				"rounded-tr",
				"rounded-br",
				"rounded-bl"
			],
			["rounded-s"] = ["rounded-ss", "rounded-es"],
			["rounded-e"] = ["rounded-se", "rounded-ee"],
			["rounded-t"] = ["rounded-tl", "rounded-tr"],
			["rounded-r"] = ["rounded-tr", "rounded-br"],
			["rounded-b"] = ["rounded-br", "rounded-bl"],
			["rounded-l"] = ["rounded-tl", "rounded-bl"],
			["border-spacing"] = ["border-spacing-x", "border-spacing-y"],
			["border-w"] = [
				"border-w-s",
				"border-w-e",
				"border-w-t",
				"border-w-r",
				"border-w-b",
				"border-w-l"
			],
			["border-w-x"] = ["border-w-r", "border-w-l"],
			["border-w-y"] = ["border-w-t", "border-w-b"],
			["border-color"] = [
				"border-color-t",
				"border-color-r",
				"border-color-b",
				"border-color-l"
			],
			["border-color-x"] = ["border-color-r", "border-color-l"],
			["border-color-y"] = ["border-color-t", "border-color-b"],
			["scroll-m"] = [
				"scroll-mx",
				"scroll-my",
				"scroll-ms",
				"scroll-me",
				"scroll-mt",
				"scroll-mr",
				"scroll-mb",
				"scroll-ml"
			],
			["scroll-mx"] = ["scroll-mr", "scroll-ml"],
			["scroll-my"] = ["scroll-mt", "scroll-mb"],
			["scroll-p"] = [
				"scroll-px",
				"scroll-py",
				"scroll-ps",
				"scroll-pe",
				"scroll-pt",
				"scroll-pr",
				"scroll-pb",
				"scroll-pl"
			],
			["scroll-px"] = ["scroll-pr", "scroll-pl"],
			["scroll-py"] = ["scroll-pt", "scroll-pb"],
			["touch"] = ["touch-x", "touch-y", "touch-pz"],
			["touch-x"] = ["touch"],
			["touch-y"] = ["touch"],
			["touch-pz"] = ["touch"]
		};
		ConflictingClassGroupModifiers = new( 1 )
		{
			["font-size"] = ["leading"]
		};
	}

	/// <summary>
	/// Creates a new instance of the <see cref="TwMergeConfig"/> class with default settings.
	/// </summary>
	/// <returns>A <see cref="TwMergeConfig"/> instance.</returns>
	public static TwMergeConfig Default() => new();

	/// <summary>
	/// Extends the current configuration with the values from the provided <see cref="ExtendedConfig"/>.
	/// </summary>
	/// <param name="extendedConfig">The extended configuration.</param>
	public void Extend( ExtendedConfig extendedConfig )
	{
		// Extend the theme
		Extend( Theme, extendedConfig.Theme );

		// Extend the class groups
		ExtendClassGroups( extendedConfig.ClassGroups );

		// Extend the conflicting class groups
		Extend( ConflictingClassGroups, extendedConfig.ConflictingClassGroups );

		// Extend the conflicting class group modifiers
		Extend( ConflictingClassGroupModifiers, extendedConfig.ConflictingClassGroupModifiers );

		static void Extend<T2>(
			Dictionary<string, T2[]> originalDict,
			Dictionary<string, T2[]>? extendDict )
		{
			if( extendDict is not { Count: > 0 } )
			{
				return;
			}

			foreach( var (key, values) in extendDict )
			{
				originalDict[key] = originalDict.TryGetValue( key, out var initialValues )
					? MergeArrays( initialValues, values )
					: values;
			}
		}

		static T2[] MergeArrays<T2>( T2[] array1, T2[] array2 )
		{
			var mergedArray = new T2[array1.Length + array2.Length];
			array1.CopyTo( mergedArray, 0 );
			array2.CopyTo( mergedArray, array1.Length );
			return mergedArray;
		}

		void ExtendClassGroups( Dictionary<string, ClassGroup>? extendDict )
		{
			if( extendDict is not { Count: > 0 } )
			{
				return;
			}

			foreach( var (classGroupId, classGroup) in extendDict )
			{
				if( ClassGroups.TryGetValue( classGroupId, out var existingGroup ) )
				{
					var mergedDefinitions = MergeArrays( existingGroup.Definitions, classGroup.Definitions );
					ClassGroups[classGroupId] = new ClassGroup( classGroup.BaseClassName, mergedDefinitions );
				}
				else
				{
					ClassGroups[classGroupId] = classGroup;
				}
			}
		}
	}

	/// <summary>
	/// Overrides the current configuration with the values from the provided <see cref="ExtendedConfig"/>.
	/// </summary>
	/// <param name="extendedConfig">The extended configuration.</param>
	public void Override( ExtendedConfig extendedConfig )
	{
		// Override the theme
		Override( Theme, extendedConfig.Theme );

		// Override the class groups
		Override( ClassGroups, extendedConfig.ClassGroups );

		// Override the conflicting class groups
		Override( ConflictingClassGroups, extendedConfig.ConflictingClassGroups );

		// Override the conflicting class group modifiers
		Override( ConflictingClassGroupModifiers, extendedConfig.ConflictingClassGroupModifiers );

		static void Override<T2>(
			Dictionary<string, T2> originalDict,
			Dictionary<string, T2>? overrideDict )
		{
			if( overrideDict is not { Count: > 0 } )
			{
				return;
			}

			foreach( var (key, value) in overrideDict )
			{
				originalDict[key] = value;
			}
		}
	}
}
