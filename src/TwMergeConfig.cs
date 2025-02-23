using TailwindMerge.Common;
using TailwindMerge.Models;

using V = TailwindMerge.Common.Validators;

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
	/// Gets or sets the <seealso href="https://tailwindcss.com/docs/configuration#prefix">prefix</seealso> 
	/// that allows you to add a custom prefix to all of Tailwind’s generated utility classes.
	/// </summary>
	public string? Prefix { get; set; }

	/// <summary>
	/// Gets or sets the theme of the configuration.
	/// </summary>
	public Dictionary<string, object[]> Theme { get; set; }

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
	/// 
	/// </summary>
	public string[] OrderSensitiveModifiers { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TwMergeConfig"/> class.
	/// </summary>
	public TwMergeConfig()
	{
		// Theme getters for theme variable namespaces
		// See https://tailwindcss.com/docs/theme#theme-variable-namespaces

		var colorTheme = ThemeUtility.FromTheme( "color" );
		var fontTheme = ThemeUtility.FromTheme( "font" );
		var textTheme = ThemeUtility.FromTheme( "text" );
		var fontWeightTheme = ThemeUtility.FromTheme( "font-weight" );
		var trackingTheme = ThemeUtility.FromTheme( "tracking" );
		var leadingTheme = ThemeUtility.FromTheme( "leading" );
		var breakpointTheme = ThemeUtility.FromTheme( "breakpoint" );
		var containerTheme = ThemeUtility.FromTheme( "container" );
		var spacingTheme = ThemeUtility.FromTheme( "spacing" );
		var radiusTheme = ThemeUtility.FromTheme( "radius" );
		var shadowTheme = ThemeUtility.FromTheme( "shadow" );
		var insetShadowTheme = ThemeUtility.FromTheme( "inset-shadow" );
		var dropShadowTheme = ThemeUtility.FromTheme( "drop-shadow" );
		var blurTheme = ThemeUtility.FromTheme( "blur" );
		var perspectiveTheme = ThemeUtility.FromTheme( "perspective" );
		var aspectTheme = ThemeUtility.FromTheme( "aspect" );
		var easeTheme = ThemeUtility.FromTheme( "ease" );
		var animateTheme = ThemeUtility.FromTheme( "animate" );

		// Helpers to avoid repeating the same scales

		string[] breakScale = ["auto", "avoid", "all", "avoid-page", "page", "left", "right", "column"];
		string[] positionScale = [
			"bottom",
			"center",
			"left",
			"left-bottom",
			"left-top",
			"right",
			"right-bottom",
			"right-top",
			"top"
		];
		string[] overflowScale = ["auto", "hidden", "clip", "visible", "scroll"];
		string[] overscrollScale = ["auto", "contain", "none"];
		string[] alignPrimaryAxisScale = ["start", "end", "center", "between", "around", "evenly", "stretch", "baseline"];
		string[] alignSecondaryAxisScale = ["start", "end", "center", "stretch"];
		string[] lineStyleScale = ["solid", "dashed", "dotted", "double"];
		string[] blendModeScale = [
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

		object[] unambiguousSpacingScale = [V.IsArbitraryVariable, V.IsArbitraryValue, spacingTheme];
		object[] insetScale = ["full", "auto", V.IsFraction, .. unambiguousSpacingScale];
		object[] gridTemplateColsRowsScale = ["none", "subgrid", V.IsInteger, V.IsArbitraryValue, V.IsArbitraryVariable];
		object[] gridColRowStartAndEndScale = [
			"auto",
			new ClassGroup( "span", ["full", V.IsInteger, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			V.IsArbitraryValue,
			V.IsArbitraryVariable
		];
		object[] gridColRowStartOrEndScale = ["auto", V.IsInteger, V.IsArbitraryValue, V.IsArbitraryVariable];
		object[] gridAutoColsRowsScale = ["auto", "min", "max", "fr", V.IsArbitraryValue, V.IsArbitraryVariable];
		object[] marginScale = ["auto", .. unambiguousSpacingScale];
		object[] sizingScale = [
			"auto",
			"full",
			"dvw",
			"dvh",
			"lvw",
			"lvh",
			"svw",
			"svh",
			"min",
			"max",
			"fit",
			V.IsFraction,
			.. unambiguousSpacingScale
		];
		object[] colorScale = [colorTheme, V.IsArbitraryValue, V.IsArbitraryVariable];
		object[] gradientStopPositionScale = [V.IsPercent, V.IsArbitraryLength];
		object[] radiusScale = [
			// Deprecated since Tailwind CSS v4.0.0
			"",
			"none",
			"full",
			radiusTheme,
			V.IsArbitraryValue,
			V.IsArbitraryVariable
		];
		object[] borderWidthScale = ["", V.IsNumber, V.IsArbitraryLength, V.IsArbitraryVariableLength];
		object[] blurScale = [
			// Deprecated since Tailwind CSS v4.0.0
			"",
			"none",
			V.IsNumber,
			V.IsArbitraryLength,
			V.IsArbitraryVariableLength
		];
		object[] originScale = [
			"center",
			"top",
			"top-right",
			"right",
			"bottom-right",
			"bottom",
			"bottom-left",
			"left",
			"top-left",
			V.IsArbitraryValue,
			V.IsArbitraryVariable
		];
		object[] rotateScale = ["none", V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable];
		object[] scaleScale = ["none", V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable];
		object[] skewScale = [V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable];
		object[] traslateScale = ["full", V.IsFraction, .. unambiguousSpacingScale];

		CacheSize = 500;
		Theme = new( 18 )
		{
			["animate"] = ["spin", "ping", "pulse", "bounce"],
			["aspect"] = ["video"],
			["blur"] = [V.IsTshirtSize],
			["breakpoint"] = [V.IsTshirtSize],
			["color"] = [V.IsAny],
			["container"] = [V.IsTshirtSize],
			["drop-shadow"] = [V.IsTshirtSize],
			["ease"] = ["in", "out", "in-out"],
			["font"] = [V.IsAnyNonArbitrary],
			["font-weight"] = [
				"thin",
				"extralight",
				"light",
				"normal",
				"medium",
				"semibold",
				"bold",
				"extrabold",
				"black"
			],
			["inset-shadow"] = [V.IsTshirtSize],
			["leading"] = ["none", "tight", "snug", "normal", "relaxed", "loose"],
			["perspective"] = ["dramatic", "near", "normal", "midrange", "distant", "none"],
			["radius"] = [V.IsTshirtSize],
			["shadow"] = [V.IsTshirtSize],
			["spacing"] = ["px", V.IsNumber],
			["text"] = [V.IsTshirtSize],
			["tracking"] = ["tighter", "tight", "normal", "wide", "wider", "widest"],
		};

		ClassGroups = new( 270 )
		{
			// --------------
			// --- Layout ---
			// --------------

			/*
            * Aspect Ratio
            * See https://tailwindcss.com/docs/aspect-ratio
            */
			["aspect"] = new( "aspect", [
				"auto",
				"square",
				aspectTheme,
				V.IsFraction,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Container
             * See https://tailwindcss.com/docs/container
             * 
             * Deprecated since Tailwind CSS v4.0.0
             */
			["container"] = new( ["container"] ),
			/*
             * Columns
             * See https://tailwindcss.com/docs/columns
             */
			["columns"] = new( "columns", [
				containerTheme,
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Break After
             * See https://tailwindcss.com/docs/break-after
             */
			["break-after"] = new( "break-after", breakScale ),
			/*
             * Break Before
             * See https://tailwindcss.com/docs/break-before
             */
			["break-before"] = new( "break-before", breakScale ),
			/*
             * Break Inside
             * See https://tailwindcss.com/docs/break-inside
             */
			["break-inside"] = new( "break-inside", ["auto", "avoid", "avoid-page", "avoid-column"] ),
			/*
             * Box Decoration Break
             * See https://tailwindcss.com/docs/box-decoration-break
             */
			["box-decoration"] = new( "box-decoration", ["slice", "clone"] ),
			/*
             * Box Sizing
             * See https://tailwindcss.com/docs/box-sizing
             */
			["box"] = new( "box", ["border", "content"] ),
			/*
             * Display
             * See https://tailwindcss.com/docs/display
             */
			["display"] = new( [
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
				"hidden"
			] ),
			/*
             * Screen Reader Only
             * See https://tailwindcss.com/docs/display#screen-reader-only
             */
			["sr"] = new( ["sr-only", "not-sr-only"] ),
			/*
             * Floats
             * See https://tailwindcss.com/docs/float
             */
			["float"] = new( "float", ["right", "left", "none", "start", "end"] ),
			/*
             * Clear
             * See https://tailwindcss.com/docs/clear
             */
			["clear"] = new( "clear", ["left", "right", "both", "none", "start", "end"] ),
			/*
             * Isolation
             * See https://tailwindcss.com/docs/isolation
             */
			["isolation"] = new( ["isolate", "isolation-auto"] ),
			/*
             * Object Fit
             * See https://tailwindcss.com/docs/object-fit
             */
			["object-fit"] = new( "object", ["contain", "cover", "fill", "none", "scale-down"] ),
			/*
             * Object Position
             * See https://tailwindcss.com/docs/object-position
             */
			["object-position"] = new( "object", [
				.. positionScale,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Overflow
             * See https://tailwindcss.com/docs/overflow
             */
			["overflow"] = new( "overflow", overflowScale ),
			/*
             * Overflow X
             * See https://tailwindcss.com/docs/overflow
             */
			["overflow-x"] = new( "overflow-x", overflowScale ),
			/*
             * Overflow Y
             * See https://tailwindcss.com/docs/overflow
             */
			["overflow-y"] = new( "overflow-y", overflowScale ),
			/*
             * Overscroll Behavior
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
			["overscroll"] = new( "overscroll", overscrollScale ),
			/*
             * Overscroll Behavior X
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
			["overscroll-x"] = new( "overscroll-x", overscrollScale ),
			/*
             * Overscroll Behavior Y
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
			["overscroll-y"] = new( "overscroll-y", overscrollScale ),
			/*
             * Position
             * See https://tailwindcss.com/docs/position
             */
			["position"] = new( ["static", "fixed", "absolute", "relative", "sticky"] ),
			/*
             * Top / Right / Bottom / Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["inset"] = new( "inset", insetScale ),
			/*
             * Right / Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["inset-x"] = new( "inset-x", insetScale ),
			/*
             * Top / Bottom
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["inset-y"] = new( "inset-y", insetScale ),
			/*
             * Start
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["start"] = new( "start", insetScale ),
			/*
             * End
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["end"] = new( "end", insetScale ),
			/*
             * Top
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["top"] = new( "top", insetScale ),
			/*
             * Right
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["right"] = new( "right", insetScale ),
			/*
             * Bottom
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["bottom"] = new( "bottom", insetScale ),
			/*
             * Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
			["left"] = new( "left", insetScale ),
			/*
             * Visibility
             * See https://tailwindcss.com/docs/visibility
             */
			["visibility"] = new( ["visible", "invisible", "collapse"] ),
			/*
             * Z-Index
             * See https://tailwindcss.com/docs/z-index
             */
			["z"] = new( "z", ["auto", V.IsInteger, V.IsInteger, V.IsArbitraryValue] ),

			// ------------------------
			// --- Flexbox and Grid ---
			// ------------------------

			/*
             * Flex Basis
             * See https://tailwindcss.com/docs/flex-basis
             */
			["basis"] = new( "basis", [
				"full",
				"auto",
				containerTheme,
				.. unambiguousSpacingScale,
				V.IsFraction,
			] ),
			/*
             * Flex Direction
             * See https://tailwindcss.com/docs/flex-direction
             */
			["flex-direction"] = new( "flex", ["row", "row-reverse", "col", "col-reverse"] ),
			/*
             * Flex Wrap
             * See https://tailwindcss.com/docs/flex-wrap
             */
			["flex-wrap"] = new( "flex", ["nowrap", "wrap", "wrap-reverse"] ),
			/*
             * Flex
             * See https://tailwindcss.com/docs/flex
             */
			["flex"] = new( "flex", [
				"auto",
				"initial",
				"none",
				V.IsNumber,
				V.IsFraction,
				V.IsArbitraryValue
			] ),
			/*
             * Flex Grow
             * See https://tailwindcss.com/docs/flex-grow
             */
			["grow"] = new( "grow", ["", V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Flex Shrink
             * See https://tailwindcss.com/docs/flex-shrink
             */
			["shrink"] = new( "shrink", ["", V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Order
             * See https://tailwindcss.com/docs/order
             */
			["order"] = new( "order", [
				"first",
				"last",
				"none",
				V.IsInteger,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Grid Template Columns
             * See https://tailwindcss.com/docs/grid-template-columns
             */
			["grid-cols"] = new( "grid-cols", gridTemplateColsRowsScale ),
			/*
             * Grid Column Start / End
             * See https://tailwindcss.com/docs/grid-column
             */
			["col-start-end"] = new( "col", gridColRowStartAndEndScale ),
			/*
             * Grid Column Start
             * See https://tailwindcss.com/docs/grid-column
             */
			["col-start"] = new( "col-start", gridColRowStartOrEndScale ),
			/*
             * Grid Column End
             * See https://tailwindcss.com/docs/grid-column
             */
			["col-end"] = new( "col-end", gridColRowStartOrEndScale ),
			/*
             * Grid Template Rows
             * See https://tailwindcss.com/docs/grid-template-rows
             */
			["grid-rows"] = new( "grid-rows", gridTemplateColsRowsScale ),
			/*
             * Grid Row Start / End
             * See https://tailwindcss.com/docs/grid-row
             */
			["row-start-end"] = new( "row", gridColRowStartAndEndScale ),
			/*
             * Grid Row Start
             * See https://tailwindcss.com/docs/grid-row
             */
			["row-start"] = new( "row-start", gridColRowStartOrEndScale ),
			/*
             * Grid Row End
             * See https://tailwindcss.com/docs/grid-row
             */
			["row-end"] = new( "row-end", gridColRowStartOrEndScale ),
			/*
             * Grid Auto Flow
             * See https://tailwindcss.com/docs/grid-auto-flow
             */
			["grid-flow"] = new( "grid-flow", ["row", "col", "dense", "row-dense", "col-dense"] ),
			/*
             * Grid Auto Columns
             * See https://tailwindcss.com/docs/grid-auto-columns
             */
			["auto-cols"] = new( "auto-cols", gridAutoColsRowsScale ),
			/*
             * Grid Auto Rows
             * See https://tailwindcss.com/docs/grid-auto-rows
             */
			["auto-rows"] = new( "auto-rows", gridAutoColsRowsScale ),
			/*
             * Gap
             * See https://tailwindcss.com/docs/gap
             */
			["gap"] = new( "gap", unambiguousSpacingScale ),
			/*
             * Gap X
             * See https://tailwindcss.com/docs/gap
             */
			["gap-x"] = new( "gap-x", unambiguousSpacingScale ),
			/*
             * Gap Y
             * See https://tailwindcss.com/docs/gap
             */
			["gap-y"] = new( "gap-y", unambiguousSpacingScale ),
			/*
             * Justify Content
             * See https://tailwindcss.com/docs/justify-content
             */
			["justify-content"] = new( "justify", ["normal", .. alignPrimaryAxisScale] ),
			/*
             * Justify Items
             * See https://tailwindcss.com/docs/justify-items
             */
			["justify-items"] = new( "justify-items", ["normal", .. alignSecondaryAxisScale] ),
			/*
             * Justify Self
             * See https://tailwindcss.com/docs/justify-self
             */
			["justify-self"] = new( "justify-self", ["auto", .. alignSecondaryAxisScale] ),
			/*
             * Align Content
             * See https://tailwindcss.com/docs/align-content
             */
			["align-content"] = new( "content", ["normal", .. alignPrimaryAxisScale] ),
			/*
             * Align Items
             * See https://tailwindcss.com/docs/align-items
             */
			["align-items"] = new( "items", ["baseline", .. alignSecondaryAxisScale] ),
			/*
             * Align Self
             * See https://tailwindcss.com/docs/align-self
             */
			["align-self"] = new( "self", ["auto", "baseline", .. alignSecondaryAxisScale] ),
			/*
             * Place Content
             * See https://tailwindcss.com/docs/place-content
             */
			["place-content"] = new( "place-content", ["baseline", .. alignPrimaryAxisScale] ),
			/*
             * Place Items
             * See https://tailwindcss.com/docs/place-items
             */
			["place-items"] = new( "place-items", ["baseline", .. alignSecondaryAxisScale] ),
			/*
             * Place Self
             * See https://tailwindcss.com/docs/place-self
             */
			["place-self"] = new( "place-self", ["auto", .. alignSecondaryAxisScale] ),
			/*
             * Padding
             * See https://tailwindcss.com/docs/padding
             */
			["p"] = new( "p", unambiguousSpacingScale ),
			/*
             * Padding X
             * See https://tailwindcss.com/docs/padding
             */
			["px"] = new( "px", unambiguousSpacingScale ),
			/*
             * Padding Y
             * See https://tailwindcss.com/docs/padding
             */
			["py"] = new( "py", unambiguousSpacingScale ),
			/*
             * Padding Start
             * See https://tailwindcss.com/docs/padding
             */
			["ps"] = new( "ps", unambiguousSpacingScale ),
			/*
             * Padding End
             * See https://tailwindcss.com/docs/padding
             */
			["pe"] = new( "pe", unambiguousSpacingScale ),
			/*
             * Padding Top
             * See https://tailwindcss.com/docs/padding
             */
			["pt"] = new( "pt", unambiguousSpacingScale ),
			/*
             * Padding Right
             * See https://tailwindcss.com/docs/padding
             */
			["pr"] = new( "pr", unambiguousSpacingScale ),
			/*
             * Padding Bottom
             * See https://tailwindcss.com/docs/padding
             */
			["pb"] = new( "pb", unambiguousSpacingScale ),
			/*
             * Padding Left
             * See https://tailwindcss.com/docs/padding
             */
			["pl"] = new( "pl", unambiguousSpacingScale ),
			/*
             * Margin
             * See https://tailwindcss.com/docs/margin
             */
			["m"] = new( "m", marginScale ),
			/*
             * Margin X
             * See https://tailwindcss.com/docs/margin
             */
			["mx"] = new( "mx", marginScale ),
			/*
             * Margin Y
             * See https://tailwindcss.com/docs/margin
             */
			["my"] = new( "my", marginScale ),
			/*
             * Margin Start
             * See https://tailwindcss.com/docs/margin
             */
			["ms"] = new( "ms", marginScale ),
			/*
             * Margin End
             * See https://tailwindcss.com/docs/margin
             */
			["me"] = new( "me", marginScale ),
			/*
             * Margin Top
             * See https://tailwindcss.com/docs/margin
             */
			["mt"] = new( "mt", marginScale ),
			/*
             * Margin Right
             * See https://tailwindcss.com/docs/margin
             */
			["mr"] = new( "mr", marginScale ),
			/*
             * Margin Bottom
             * See https://tailwindcss.com/docs/margin
             */
			["mb"] = new( "mb", marginScale ),
			/*
             * Margin Left
             * See https://tailwindcss.com/docs/margin
             */
			["ml"] = new( "ml", marginScale ),
			/*
             * Space Between X
             * See https://tailwindcss.com/docs/space
             */
			["space-x"] = new( "space-x", unambiguousSpacingScale ),
			/*
             * Space Between X Reverse
             * See https://tailwindcss.com/docs/space
             */
			["space-x-reverse"] = new( ["space-x-reverse"] ),
			/*
             * Space Between Y
             * See https://tailwindcss.com/docs/space
             */
			["space-y"] = new( "space-y", unambiguousSpacingScale ),
			/*
             * Space Between Y Reverse
             * See https://tailwindcss.com/docs/space
             */
			["space-y-reverse"] = new( ["space-y-reverse"] ),

			// --------------
			// --- Sizing ---
			// --------------

			/*
             * Size
             * See https://tailwindcss.com/docs/width#setting-both-width-and-height
             */
			["size"] = new( "size", sizingScale ),
			/*
             * Width
             * See https://tailwindcss.com/docs/width
             */
			["w"] = new( "w", ["screen", containerTheme, .. sizingScale] ),
			/*
             * Min-Width
             * See https://tailwindcss.com/docs/min-width
             */
			["min-w"] = new( "min-w", [
				"screen",
				containerTheme,
				// Deprecated since Tailwind CSS v4.0.0. See https://github.com/tailwindlabs/tailwindcss.com/issues/2027#issuecomment-2620152757
				"none",
				.. sizingScale
			] ),
			/*
             * Max-Width
             * See https://tailwindcss.com/docs/max-width
             */
			["max-w"] = new( "max-w", [
				"screen",
				"none",
				// Deprecated since Tailwind CSS v4.0.0. See https://github.com/tailwindlabs/tailwindcss.com/issues/2027#issuecomment-2620152757
				"prose",
				// Deprecated since Tailwind CSS v4.0.0. See https://github.com/tailwindlabs/tailwindcss.com/issues/2027#issuecomment-2620152757
				new ClassGroup( "screen", [breakpointTheme] ),
				.. sizingScale
			] ),
			/*
             * Height
             * See https://tailwindcss.com/docs/height
             */
			["h"] = new( "h", ["screen", .. sizingScale] ),
			/*
             * Min-Height
             * See https://tailwindcss.com/docs/min-height
             */
			["min-h"] = new( "min-h", ["screen", "none", .. sizingScale] ),
			/*
             * Max-Height
             * See https://tailwindcss.com/docs/max-height
             */
			["max-h"] = new( "max-h", ["screen", .. sizingScale] ),

			// ------------------
			// --- Typography ---
			// ------------------

			/*
             * Font Size
             * See https://tailwindcss.com/docs/font-size
             */
			["font-size"] = new( "text", [
				"base",
				textTheme,
				V.IsArbitraryLength,
				V.IsArbitraryVariableLength
			] ),
			/*
             * Font Smoothing
             * See https://tailwindcss.com/docs/font-smoothing
             */
			["font-smoothing"] = new( ["antialiased", "subpixel-antialiased"] ),
			/*
             * Font Style
             * See https://tailwindcss.com/docs/font-style
             */
			["font-style"] = new( ["italic", "not-italic"] ),
			/*
             * Font Weight
             * See https://tailwindcss.com/docs/font-weight
             */
			["font-weight"] = new( "font", [fontWeightTheme, V.IsArbitraryNumber, V.IsArbitraryVariable] ),
			/*
             * Font Stretch
             * See https://tailwindcss.com/docs/font-stretch
             */
			["font-stretch"] = new( "font-stretch", [
				"ultra-condensed",
				"extra-condensed",
				"condensed",
				"semi-condensed",
				"normal",
				"semi-expanded",
				"expanded",
				"extra-expanded",
				"ultra-expanded",
				V.IsPercent,
				V.IsArbitraryValue
			] ),
			/*
             * Font Family
             * See https://tailwindcss.com/docs/font-family
             */
			["font-family"] = new( "font", [
				fontTheme,
				V.IsArbitraryValue,
				V.IsArbitraryVariableFamilyName
			] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-normal"] = new( ["normal-nums"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-ordinal"] = new( ["ordinal"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-slashed-zero"] = new( ["slashed-zero"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-figure"] = new( ["lining-nums", "oldstyle-nums"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-spacing"] = new( ["proportional-nums", "tabular-nums"] ),
			/*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
			["fvn-fraction"] = new( ["diagonal-fractions", "stacked-fractions"] ),
			/*
             * Letter Spacing
             * See https://tailwindcss.com/docs/letter-spacing
             */
			["tracking"] = new( "tracking", [trackingTheme, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Line Clamp
             * See https://tailwindcss.com/docs/line-clamp
             */
			["line-clamp"] = new( "line-clamp", [
				"none",
				V.IsNumber,
				V.IsArbitraryNumber,
				V.IsArbitraryVariable
			] ),
			/*
             * Line Height
             * See https://tailwindcss.com/docs/line-height
             */
			["leading"] = new( "leading", [
				// Deprecated since Tailwind CSS v4.0.0. See https://github.com/tailwindlabs/tailwindcss.com/issues/2027#issuecomment-2620152757
				leadingTheme,
				.. unambiguousSpacingScale
			] ),
			/*
             * List Style Image
             * See https://tailwindcss.com/docs/list-style-image
             */
			["list-image"] = new( "list-image", ["none", V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * List Style Position
             * See https://tailwindcss.com/docs/list-style-position
             */
			["list-style-position"] = new( "list", ["inside", "outside"] ),
			/*
             * List Style Type
             * See https://tailwindcss.com/docs/list-style-type
             */
			["list-style-type"] = new( "list", [
				"disc",
				"decimal",
				"none",
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Text Alignment
             * See https://tailwindcss.com/docs/text-align
             */
			["text-alignment"] = new( "text", ["left", "center", "right", "justify", "start", "end"] ),
			/*
             * Text Color
             * See https://tailwindcss.com/docs/text-color
             */
			["text-color"] = new( "text", colorScale ),
			/*
             * Text Decoration
             * See https://tailwindcss.com/docs/text-decoration
             */
			["text-decoration"] = new( ["underline", "overline", "line-through", "no-underline"] ),
			/*
             * Text Decoration Style
             * See https://tailwindcss.com/docs/text-decoration-style
             */
			["text-decoration-style"] = new( "decoration", ["wavy", .. lineStyleScale] ),
			/*
             * Text Decoration Thickness
             * See https://tailwindcss.com/docs/text-decoration-thickness
             */
			["text-decoration-thickness"] = new( "decoration", [
				"from-font",
				"auto",
				V.IsNumber,
				V.IsArbitraryLength,
				V.IsArbitraryVariable
			] ),
			/*
             * Text Decoration Color
             * See https://tailwindcss.com/docs/text-decoration-color
             */
			["text-decoration-color"] = new( "decoration", colorScale ),
			/*
             * Text Underline Offset
             * See https://tailwindcss.com/docs/text-underline-offset
             */
			["underline-offset"] = new( "underline-offset", [
				"auto",
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Text Transform
             * See https://tailwindcss.com/docs/text-transform
             */
			["text-transform"] = new( ["uppercase", "lowercase", "capitalize", "normal-case"] ),
			/*
             * Text Overflow
             * See https://tailwindcss.com/docs/text-overflow
             */
			["text-overflow"] = new( ["truncate", "text-ellipsis", "text-clip"] ),
			/*
             * Text Wrap
             * See https://tailwindcss.com/docs/text-wrap
             */
			["text-wrap"] = new( "text", ["wrap", "nowrap", "balance", "pretty"] ),
			/*
             * Text Indent
             * See https://tailwindcss.com/docs/text-indent
             */
			["indent"] = new( "indent", unambiguousSpacingScale ),
			/*
             * Vertical Alignment
             * See https://tailwindcss.com/docs/vertical-align
             */
			["vertical-align"] = new( "align", [
				"baseline",
				"top",
				"middle",
				"bottom",
				"text-top",
				"text-bottom",
				"sub",
				"super",
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Whitespace
             * See https://tailwindcss.com/docs/whitespace
             */
			["whitespace"] = new( "whitespace", [
				"normal",
				"nowrap",
				"pre",
				"pre-line",
				"pre-wrap",
				"break-spaces"
			] ),
			/*
             * Work Break
             * See https://tailwindcss.com/docs/word-break
             */
			["break"] = new( "break", ["normal", "words", "all", "keep"] ),
			/*
             * Hyphens
             * See https://tailwindcss.com/docs/hyphens
             */
			["hyphens"] = new( "hyphens", ["none", "manual", "auto"] ),
			/*
             * Content
             * See https://tailwindcss.com/docs/content
             */
			["content"] = new( "content", ["none", V.IsArbitraryValue, V.IsArbitraryVariable] ),

			// -------------------
			// --- Backgrounds ---
			// -------------------

			/*
             * Background Attachment
             * See https://tailwindcss.com/docs/background-attachment
             */
			["bg-attachment"] = new( "bg", ["fixed", "local", "scroll"] ),
			/*
             * Background Clip
             * See https://tailwindcss.com/docs/background-clip
             */
			["bg-clip"] = new( "bg-clip", ["border", "padding", "content", "text"] ),
			/*
             * Background Origin
             * See https://tailwindcss.com/docs/background-origin
             */
			["bg-origin"] = new( "bg-origin", ["border", "padding", "content"] ),
			/*
             * Background Position
             * See https://tailwindcss.com/docs/background-position
             */
			["bg-position"] = new( "bg", [
				.. positionScale,
				V.IsArbitraryPosition,
				V.IsArbitraryVariablePosition
			] ),
			/*
             * Background Repeat
             * See https://tailwindcss.com/docs/background-repeat
             */
			["bg-repeat"] = new( "bg", [
				"no-repeat",
				new ClassGroup( "repeat", ["", "x", "y", "round", "space"] )
			] ),
			/*
             * Background Size
             * See https://tailwindcss.com/docs/background-size
             */
			["bg-size"] = new( "bg", [
				"auto",
				"cover",
				"contain",
				V.IsArbitrarySize,
				V.IsArbitraryVariableSize
			] ),
			/*
             * Background Image
             * See https://tailwindcss.com/docs/background-image
             */
			["bg-image"] = new( "bg", [
				"none",
				new ClassGroup( "linear", [
					new ClassGroup( "to", [
						"t",
						"tr",
						"r",
						"br",
						"b",
						"bl",
						"l",
						"tl"
					] ),
					V.IsInteger,
					V.IsArbitraryValue
				] ),
				new ClassGroup( "radial", ["", V.IsArbitraryValue] ),
				new ClassGroup( "conic", [V.IsInteger, V.IsArbitraryValue] ),
				V.IsArbitraryImage
			] ),
			/*
             * Background Color
             * See https://tailwindcss.com/docs/background-color
             */
			["bg-color"] = new( "bg", colorScale ),
			/*
             * Gradient Color Stops From Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-from-pos"] = new( "from", gradientStopPositionScale ),
			/*
             * Gradient Color Stops Via Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-via-pos"] = new( "via", gradientStopPositionScale ),
			/*
             * Gradient Color Stops To Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-to-pos"] = new( "to", gradientStopPositionScale ),
			/*
             * Gradient Color Stops From
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-from"] = new( "from", colorScale ),
			/*
             * Gradient Color Stops Via
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-via"] = new( "via", colorScale ),
			/*
             * Gradient Color Stops To
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
			["gradient-to"] = new( "to", colorScale ),

			// ---------------
			// --- Borders ---
			// ---------------

			/*
             * Border Radius
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded"] = new( "rounded", radiusScale ),
			/*
             * Border Radius Start
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-s"] = new( "rounded-s", radiusScale ),
			/*
             * Border Radius End
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-e"] = new( "rounded-e", radiusScale ),
			/*
             * Border Radius Top
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-t"] = new( "rounded-t", radiusScale ),
			/*
             * Border Radius Right
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-r"] = new( "rounded-r", radiusScale ),
			/*
             * Border Radius Bottom
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-b"] = new( "rounded-b", radiusScale ),
			/*
             * Border Radius Left
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-l"] = new( "rounded-l", radiusScale ),
			/*
             * Border Radius Start Start
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-ss"] = new( "rounded-ss", radiusScale ),
			/*
             * Border Radius Start End
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-se"] = new( "rounded-se", radiusScale ),
			/*
             * Border Radius End End
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-ee"] = new( "rounded-ee", radiusScale ),
			/*
             * Border Radius End Start
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-es"] = new( "rounded-es", radiusScale ),
			/*
             * Border Radius Top Left
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-tl"] = new( "rounded-tl", radiusScale ),
			/*
             * Border Radius Top Right
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-tr"] = new( "rounded-tr", radiusScale ),
			/*
             * Border Radius Bottom Right
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-br"] = new( "rounded-br", radiusScale ),
			/*
             * Border Radius Bottom Left
             * See https://tailwindcss.com/docs/border-radius
             */
			["rounded-bl"] = new( "rounded-bl", radiusScale ),
			/*
             * Border Width
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w"] = new( "border", borderWidthScale ),
			/*
             * Border Width X
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-x"] = new( "border-x", borderWidthScale ),
			/*
             * Border Width Y
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-y"] = new( "border-y", borderWidthScale ),
			/*
             * Border Width Start
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-s"] = new( "border-s", borderWidthScale ),
			/*
             * Border Width End
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-e"] = new( "border-e", borderWidthScale ),
			/*
             * Border Width Top
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-t"] = new( "border-t", borderWidthScale ),
			/*
             * Border Width Right
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-r"] = new( "border-r", borderWidthScale ),
			/*
             * Border Width Bottom
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-b"] = new( "border-b", borderWidthScale ),
			/*
             * Border Width Left
             * See https://tailwindcss.com/docs/border-width
             */
			["border-w-l"] = new( "border-l", borderWidthScale ),
			/*
             * Divide Width X
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-x"] = new( "divide-x", borderWidthScale ),
			/*
             * Divide Width X Reverse
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-x-reverse"] = new( ["divide-x-reverse"] ),
			/*
             * Divide Width Y
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-y"] = new( "divide-y", borderWidthScale ),
			/*
             * Divide Width Y Reverse
             * See https://tailwindcss.com/docs/divide-width
             */
			["divide-y-reverse"] = new( ["divide-y-reverse"] ),
			/*
             * Border Style
             * See https://tailwindcss.com/docs/border-style
             */
			["border-style"] = new( "border", ["hidden", "none", .. lineStyleScale] ),
			/*
             * Divide Style
             * See https://tailwindcss.com/docs/divide-style
             */
			["divide-style"] = new( "divide", ["hidden", "none", .. lineStyleScale] ),
			/*
             * Border Color
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color"] = new( "border", colorScale ),
			/*
             * Border Color X
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-x"] = new( "border-x", colorScale ),
			/*
             * Border Color Y
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-y"] = new( "border-y", colorScale ),
			/*
             * Border Color Start
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-s"] = new( "border-s", colorScale ),
			/*
             * Border Color End
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-e"] = new( "border-e", colorScale ),
			/*
             * Border Color Top
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-t"] = new( "border-t", colorScale ),
			/*
             * Border Color Right
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-r"] = new( "border-r", colorScale ),
			/*
             * Border Color Bottom
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-b"] = new( "border-b", colorScale ),
			/*
             * Border Color Left
             * See https://tailwindcss.com/docs/border-color
             */
			["border-color-l"] = new( "border-l", colorScale ),
			/*
             * Divide Color
             * See https://tailwindcss.com/docs/divide-color
             */
			["divide-color"] = new( "divide", colorScale ),
			/*
             * Outline Style
             * See https://tailwindcss.com/docs/outline-style
             */
			["outline-style"] = new( "outline", ["none", "hidden", .. lineStyleScale] ),
			/*
             * Outline Offset
             * See https://tailwindcss.com/docs/outline-offset
             */
			["outline-offset"] = new( "outline-offset", [
				V.IsNumber,
				V.IsArbitraryLength,
				V.IsArbitraryVariableLength
			] ),
			/*
             * Outline Width
             * See https://tailwindcss.com/docs/outline-width
             */
			["outline-w"] = new( "outline", [
				"",
				V.IsNumber,
				V.IsArbitraryLength,
				V.IsArbitraryVariableLength
			] ),
			/*
             * Outline Color
             * See https://tailwindcss.com/docs/outline-color
             */
			["outline-color"] = new( "outline", [colorTheme] ),

			// ---------------
			// --- Effects ---
			// ---------------

			/*
             * Box Shadow
             * See https://tailwindcss.com/docs/box-shadow
             */
			["shadow"] = new( "shadow", [
				// Deprecated since Tailwind CSS v4.0.0
				"",
				"none",
				shadowTheme,
				V.IsArbitraryShadow,
				V.IsArbitraryVariableShadow
			] ),
			/*
             * Box Shadow Color
             * See https://tailwindcss.com/docs/box-shadow#setting-the-shadow-color
             */
			["shadow-color"] = new( "shadow", colorScale ),
			/*
             * Inset Box Shadow
             * See https://tailwindcss.com/docs/box-shadow#adding-an-inset-shadow
             */
			["inset-shadow"] = new( "inset-shadow", [
				"none",
				insetShadowTheme,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Inset Box Shadow Color
             * See https://tailwindcss.com/docs/box-shadow#setting-the-inset-shadow-color
             */
			["inset-shadow-color"] = new( "inset-shadow", colorScale ),
			/*
             * Ring Width
             * See https://tailwindcss.com/docs/box-shadow#adding-a-ring
             */
			["ring-w"] = new( "ring", borderWidthScale ),
			/*
             * Ring Width Inset
             * See https://v3.tailwindcss.com/docs/ring-width#inset-rings
             * 
             * Deprecated since Tailwind CSS v4.0.0. 
             * See https://github.com/tailwindlabs/tailwindcss/blob/v4.0.0/packages/tailwindcss/src/utilities.ts#L4158
             */
			["ring-w-inset"] = new( ["ring-inset"] ),
			/*
             * Ring Color
             * See https://tailwindcss.com/docs/box-shadow#setting-the-ring-color
             */
			["ring-color"] = new( "ring", colorScale ),
			/*
             * Ring Offset Width
             * See https://v3.tailwindcss.com/docs/ring-offset-width
             * 
             * Deprecated since Tailwind CSS v4.0.0. 
             * See https://github.com/tailwindlabs/tailwindcss/blob/v4.0.0/packages/tailwindcss/src/utilities.ts#L4158
             */
			["ring-offset-w"] = new( "ring-offset", [V.IsNumber, V.IsArbitraryLength] ),
			/*
             * Ring Offset Color
             * See https://v3.tailwindcss.com/docs/ring-offset-color
             * 
             * Deprecated since Tailwind CSS v4.0.0. 
             * See https://github.com/tailwindlabs/tailwindcss/blob/v4.0.0/packages/tailwindcss/src/utilities.ts#L4158
             */
			["ring-offset-color"] = new( "ring-offset", colorScale ),
			/*
             * Inset Ring Width
             * See https://tailwindcss.com/docs/box-shadow#adding-an-inset-ring
             */
			["inset-ring-w"] = new( "inset-ring", borderWidthScale ),
			/*
             * Inset Ring Color
             * See https://tailwindcss.com/docs/box-shadow#setting-the-inset-ring-color
             */
			["inset-ring-color"] = new( "inset-ring", colorScale ),
			/*
             * Opacity
             * See https://tailwindcss.com/docs/opacity
             */
			["opacity"] = new( "opacity", [V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Mix Blend Mode
             * See https://tailwindcss.com/docs/mix-blend-mode
             */
			["mix-blend"] = new( "mix-blend", ["plus-darker", "plus-lighter", .. blendModeScale] ),
			/*
             * Background Blend Mode
             * See https://tailwindcss.com/docs/mix-blend-mode
             */
			["bg-blend"] = new( "bg-blend", blendModeScale ),

			// ---------------
			// --- Filters ---
			// ---------------

			/*
             * Filter
             * See https://tailwindcss.com/docs/filter
             */
			["filter"] = new( "filter", ["none", V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Blur
             * See https://tailwindcss.com/docs/blur
             */
			["blur"] = new( "blur", blurScale ),
			/*
             * Brightness
             * See https://tailwindcss.com/docs/brightness
             */
			["brightness"] = new( "brightness", [V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Contrast
             * See https://tailwindcss.com/docs/contrast
             */
			["contrast"] = new( "contrast", [V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Drop Shadow
             * See https://tailwindcss.com/docs/drop-shadow
             */
			["drop-shadow"] = new( "drop-shadow", [
				// Deprecated since Tailwind CSS v4.0.0
				"",
				"none",
				dropShadowTheme,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Grayscale
             * See https://tailwindcss.com/docs/grayscale
             */
			["grayscale"] = new( "grayscale", ["", V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Hue Rotate
             * See https://tailwindcss.com/docs/hue-rotate
             */
			["hue-rotate"] = new( "hue-rotate", [V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Invert
             * See https://tailwindcss.com/docs/invert
             */
			["invert"] = new( "invert", ["", V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Saturate
             * See https://tailwindcss.com/docs/saturate
             */
			["saturate"] = new( "saturate", [V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Sepia
             * See https://tailwindcss.com/docs/sepia
             */
			["sepia"] = new( "sepia", ["", V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Backdrop Filter
             * See https://tailwindcss.com/docs/backdrop-blur
             */
			["backdrop-filter"] = new( "backdrop-filter", [
				"none",
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Blur
             * See https://tailwindcss.com/docs/backdrop-blur
             */
			["backdrop-blur"] = new( "backdrop-blur", blurScale ),
			/*
             * Backdrop Brightness
             * See https://tailwindcss.com/docs/backdrop-brightness
             */
			["backdrop-brightness"] = new( "backdrop-brightness", [
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Contrast
             * See https://tailwindcss.com/docs/backdrop-contrast
             */
			["backdrop-contrast"] = new( "backdrop-contrast", [
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Grayscale
             * See https://tailwindcss.com/docs/backdrop-grayscale
             */
			["backdrop-grayscale"] = new( "backdrop-grayscale", [
				"",
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Hue Rotate
             * See https://tailwindcss.com/docs/backdrop-hue-rotate
             */
			["backdrop-hue-rotate"] = new( "backdrop-hue-rotate", [
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Invert
             * See https://tailwindcss.com/docs/backdrop-invert
             */
			["backdrop-invert"] = new( "backdrop-invert", [
				"",
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Opacity
             * See https://tailwindcss.com/docs/backdrop-opacity
             */
			["backdrop-opacity"] = new( "backdrop-opacity", [
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Saturate
             * See https://tailwindcss.com/docs/backdrop-saturate
             */
			["backdrop-saturate"] = new( "backdrop-saturate", [
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Backdrop Sepia
             * See https://tailwindcss.com/docs/backdrop-sepia
             */
			["backdrop-sepia"] = new( "backdrop-sepia", [
				"",
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),

			// --------------
			// --- Tables ---
			// --------------

			/*
             * Border Collapse
             * See https://tailwindcss.com/docs/border-collapse
             */
			["border-collapse"] = new( "border", ["collapse", "separate"] ),
			/*
             * Border Spacing
             * See https://tailwindcss.com/docs/border-spacing
             */
			["border-spacing"] = new( "border-spacing", unambiguousSpacingScale ),
			/*
             * Border Spacing X
             * See https://tailwindcss.com/docs/border-spacing
             */
			["border-spacing-x"] = new( "border-spacing-x", unambiguousSpacingScale ),
			/*
             * Border Spacing Y
             * See https://tailwindcss.com/docs/border-spacing
             */
			["border-spacing-y"] = new( "border-spacing-y", unambiguousSpacingScale ),
			/*
             * Table Layout
             * See https://tailwindcss.com/docs/table-layout
             */
			["table-layout"] = new( "table", ["auto", "fixed"] ),
			/*
             * Caption Side
             * See https://tailwindcss.com/docs/caption-side
             */
			["caption"] = new( "caption", ["top", "bottom"] ),

			// ---------------------------------
			// --- Transitions and Animation ---
			// ---------------------------------

			/*
             * Transition Property
             * See https://tailwindcss.com/docs/transition-property
             */
			["transition"] = new( "transition", [
				"",
				"all",
				"colors",
				"opacity",
				"shadow",
				"transform",
				"none",
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Transition Behavior
             * See https://tailwindcss.com/docs/transition-behavior
             */
			["transition-behavior"] = new( "transition", ["normal", "discrete"] ),
			/*
             * Transition Duration
             * See https://tailwindcss.com/docs/transition-duration
             */
			["duration"] = new( "duration", [
				"initial",
				V.IsNumber,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Transition Timing Function
             * See https://tailwindcss.com/docs/transition-timing-function
             */
			["ease"] = new( "ease", [
				"linear",
				"initial",
				easeTheme,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Transition Delay
             * See https://tailwindcss.com/docs/transition-delay
             */
			["delay"] = new( "delay", [V.IsNumber, V.IsArbitraryValue, V.IsArbitraryVariable] ),
			/*
             * Animation
             * See https://tailwindcss.com/docs/animation
             */
			["animate"] = new( "animate", [
				"none",
				animateTheme,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),

			// ------------------
			// --- Transforms ---
			// ------------------

			/*
             * Backface Visibility
             * See https://tailwindcss.com/docs/backface-visibility
             */
			["backface"] = new( "backface", ["hidden", "visible"] ),
			/*
             * Perspective
             * See https://tailwindcss.com/docs/perspective
             */
			["perspective"] = new( "perspective", [
				perspectiveTheme,
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Perspective Origin
             * See https://tailwindcss.com/docs/perspective-origin
             */
			["perspective-origin"] = new( "perspective-origin", originScale ),
			/*
             * Rotate
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate"] = new( "rotate", rotateScale ),
			/*
             * Rotate X
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate-x"] = new( "rotate-x", rotateScale ),
			/*
             * Rotate Y
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate-y"] = new( "rotate-y", rotateScale ),
			/*
             * Rotate Z
             * See https://tailwindcss.com/docs/rotate
             */
			["rotate-z"] = new( "rotate-z", rotateScale ),
			/*
             * Scale
             * See https://tailwindcss.com/docs/scale
             */
			["scale"] = new( "scale", scaleScale ),
			/*
             * Scale X
             * See https://tailwindcss.com/docs/scale
             */
			["scale-x"] = new( "scale-x", scaleScale ),
			/*
             * Scale Y
             * See https://tailwindcss.com/docs/scale
             */
			["scale-y"] = new( "scale-y", scaleScale ),
			/*
             * Scale Z
             * See https://tailwindcss.com/docs/scale
             */
			["scale-z"] = new( "scale-z", scaleScale ),
			/*
             * Scale 3D
             * See https://tailwindcss.com/docs/scale
             */
			["scale-3d"] = new( ["scale-3d"] ),
			/*
             * Skew
             * See https://tailwindcss.com/docs/skew
             */
			["skew"] = new( "skew", skewScale ),
			/*
             * Skew X
             * See https://tailwindcss.com/docs/skew
             */
			["skew-x"] = new( "skew-x", skewScale ),
			/*
             * Skew Y
             * See https://tailwindcss.com/docs/skew
             */
			["skew-y"] = new( "skew-y", skewScale ),
			/*
             * Transform
             * See https://tailwindcss.com/docs/transform
             */
			["transform"] = new( "transform", [
				"",
				"none",
				"gpu",
				"cpu",
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Transform Origin
             * See https://tailwindcss.com/docs/transform-origin
             */
			["transform-origin"] = new( "origin", originScale ),
			/*
             * Transform Style
             * See https://tailwindcss.com/docs/transform-style
             */
			["transform-style"] = new( "transform", ["3d", "flat"] ),
			/*
             * Translate X
             * See https://tailwindcss.com/docs/translate
             */
			["translate"] = new( "translate", traslateScale ),
			/*
             * Translate X
             * See https://tailwindcss.com/docs/translate
             */
			["translate-x"] = new( "translate-x", traslateScale ),
			/*
             * Translate Y
             * See https://tailwindcss.com/docs/translate
             */
			["translate-y"] = new( "translate-y", traslateScale ),
			/*
             * Translate Z
             * See https://tailwindcss.com/docs/translate
             */
			["translate-z"] = new( "translate-z", traslateScale ),
			/*
             * Translate None
             * See https://tailwindcss.com/docs/translate
             */
			["translate-none"] = new( ["translate-none"] ),

			// ---------------------
			// --- Interactivity ---
			// ---------------------

			/*
             * Accent Color
             * See https://tailwindcss.com/docs/accent-color
             */
			["accent"] = new( "accent", colorScale ),
			/*
             * Appearance
             * See https://tailwindcss.com/docs/appearance
             */
			["appearance"] = new( "appearance", ["none", "auto"] ),
			/*
             * Caret Color
             * See https://tailwindcss.com/docs/caret-color
             */
			["caret-color"] = new( "caret", colorScale ),
			/*
             * Color Scheme
             * See https://tailwindcss.com/docs/color-scheme
             */
			["color-scheme"] = new( "scheme", [
				"normal",
				"dark",
				"light",
				"light-dark",
				"only-dark",
				"only-light"
			] ),
			/*
             * Cursor
             * See https://tailwindcss.com/docs/cursor
             */
			["cursor"] = new( "cursor", [
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
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),
			/*
             * Field Sizing
             * See https://tailwindcss.com/docs/field-sizing
             */
			["field-sizing"] = new( "field-sizing", ["fixed", "content"] ),
			/*
             * Pointer Events
             * See https://tailwindcss.com/docs/pointer-events
             */
			["pointer-events"] = new( "pointer-events", ["auto", "none"] ),
			/*
             * Resize
             * See https://tailwindcss.com/docs/resize
             */
			["resize"] = new( "resize", ["none", "", "x", "y"] ),
			/*
             * Scroll Behavior
             * See https://tailwindcss.com/docs/scroll-behavior
             */
			["scroll-behavior"] = new( "scroll", ["auto", "smooth"] ),
			/*
             * Scroll Margin
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-m"] = new( "scroll-m", unambiguousSpacingScale ),
			/*
             * Scroll Margin X
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mx"] = new( "scroll-mx", unambiguousSpacingScale ),
			/*
             * Scroll Margin Y
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-my"] = new( "scroll-my", unambiguousSpacingScale ),
			/*
             * Scroll Margin Start
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-ms"] = new( "scroll-ms", unambiguousSpacingScale ),
			/*
             * Scroll Margin End
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-me"] = new( "scroll-me", unambiguousSpacingScale ),
			/*
             * Scroll Margin Top
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mt"] = new( "scroll-mt", unambiguousSpacingScale ),
			/*
             * Scroll Margin Right
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mr"] = new( "scroll-mr", unambiguousSpacingScale ),
			/*
             * Scroll Margin Bottom
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-mb"] = new( "scroll-mb", unambiguousSpacingScale ),
			/*
             * Scroll Margin Left
             * See https://tailwindcss.com/docs/scroll-margin
             */
			["scroll-ml"] = new( "scroll-ml", unambiguousSpacingScale ),
			/*
             * Scroll Padding
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-p"] = new( "scroll-p", unambiguousSpacingScale ),
			/*
             * Scroll Padding X
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-px"] = new( "scroll-px", unambiguousSpacingScale ),
			/*
             * Scroll Padding Y
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-py"] = new( "scroll-py", unambiguousSpacingScale ),
			/*
             * Scroll Padding Start
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-ps"] = new( "scroll-ps", unambiguousSpacingScale ),
			/*
             * Scroll Padding End
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pe"] = new( "scroll-pe", unambiguousSpacingScale ),
			/*
             * Scroll Padding Top
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pt"] = new( "scroll-pt", unambiguousSpacingScale ),
			/*
             * Scroll Padding Right
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pr"] = new( "scroll-pr", unambiguousSpacingScale ),
			/*
             * Scroll Padding Bottom
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pb"] = new( "scroll-pb", unambiguousSpacingScale ),
			/*
             * Scroll Padding Left
             * See https://tailwindcss.com/docs/scroll-padding
             */
			["scroll-pl"] = new( "scroll-pl", unambiguousSpacingScale ),
			/*
             * Scroll Snap Align
             * See https://tailwindcss.com/docs/scroll-snap-align
             */
			["snap-align"] = new( "snap", ["start", "end", "center", "align-none"] ),
			/*
             * Scroll Snap Stop
             * See https://tailwindcss.com/docs/scroll-snap-stop
             */
			["snap-stop"] = new( "snap", ["normal", "always"] ),
			/*
             * Scroll Snap Type
             * See https://tailwindcss.com/docs/scroll-snap-type
             */
			["snap-type"] = new( "snap", ["none", "x", "y", "both"] ),
			/*
             * Scroll Snap Type Strictness
             * See https://tailwindcss.com/docs/scroll-snap-type
             */
			["snap-strictness"] = new( "snap", ["mandatory", "proximity"] ),
			/*
             * Touch Action
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch"] = new( "touch", ["auto", "none", "manipulation"] ),
			/*
             * Touch Action X
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch-x"] = new( "touch-pan", ["x", "left", "right"] ),
			/*
             * Touch Action Y
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch-y"] = new( "touch-pan", ["y", "up", "down"] ),
			/*
             * Touch Action Pinch Zoom
             * See https://tailwindcss.com/docs/touch-action
             */
			["touch-pz"] = new( ["touch-pinch-zoom"] ),
			/*
             * User Select
             * See https://tailwindcss.com/docs/user-select
             */
			["select"] = new( "select", ["none", "text", "all", "auto"] ),
			/*
             * Will Change
             * See https://tailwindcss.com/docs/will-change
             */
			["will-change"] = new( "will-change", [
				"auto",
				"scroll",
				"contents",
				"transform",
				V.IsArbitraryValue,
				V.IsArbitraryVariable
			] ),

			// -----------
			// --- SVG ---
			// -----------

			/*
             * Fill
             * See https://tailwindcss.com/docs/fill
             */
			["fill"] = new( "fill", ["none", .. colorScale] ),
			/*
             * Stroke Width
             * See https://tailwindcss.com/docs/stroke-width
             */
			["stroke-w"] = new( "stroke", [
				V.IsNumber,
				V.IsArbitraryNumber,
				V.IsArbitraryLength,
				V.IsArbitraryVariableLength
			] ),
			/*
             * Stroke
             * See https://tailwindcss.com/docs/stroke
             */
			["stroke"] = new( "stroke", ["none", .. colorScale] ),

			// ---------------------
			// --- Accessibility ---
			// ---------------------

			/*
             * Forced Color Adjust
             * See https://tailwindcss.com/docs/forced-color-adjust
             */
			["forced-color-adjust"] = new( "forced-color-adjust", ["auto", "none"] )
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
				"border-color-s",
				"border-color-e",
				"border-color-t",
				"border-color-r",
				"border-color-b",
				"border-color-l"
			],
			["border-color-x"] = ["border-color-r", "border-color-l"],
			["border-color-y"] = ["border-color-t", "border-color-b"],
			["translate"] = ["translate-x", "translate-y", "translate-none"],
			["translate-none"] = ["translate", "translate-x", "translate-y", "translate-z"],
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

		OrderSensitiveModifiers = [
			"before",
			"after",
			"placeholder",
			"file",
			"marker",
			"selection",
			"first-line",
			"first-letter",
			"backdrop",
			"*",
			"**"
		];
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
