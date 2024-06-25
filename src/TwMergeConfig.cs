using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

/// <summary>
/// Represents the configuration settings for the <see cref="TwMerge"/>.
/// </summary>
public class TwMergeConfig
{
    private static readonly Dictionary<string, string[]> _conflictingClassGroups = new( 46 )
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

    private static readonly Dictionary<string, string[]> _conflictingClassGroupModifiers = new( 1 )
    {
        ["font-size"] = ["leading"]
    };

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
    /// Gets or sets the theme configuration.
    /// </summary>
    public Dictionary<string, object[]> Theme { get; set; }

    internal ClassGroup[] ClassGroups { get; init; }
    internal IReadOnlyDictionary<string, string[]> ConflictingClassGroups { get; }
    internal IReadOnlyDictionary<string, string[]> ConflictingClassGroupModifiers { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TwMergeConfig"/> class.
    /// </summary>
    public TwMergeConfig()
    {
        CacheSize = 500;
        Separator = ":";

        var colors = () => Theme?["colors"];
        var spacing = () => Theme?["spacing"];
        var blur = () => Theme?["blur"];
        var brightness = () => Theme?["brightness"];
        var borderColor = () => Theme?["borderColor"];
        var borderRadius = () => Theme?["borderRadius"];
        var borderSpacing = () => Theme?["borderSpacing"];
        var borderWidth = () => Theme?["borderWidth"];
        var contrast = () => Theme?["contrast"];
        var grayscale = () => Theme?["grayscale"];
        var hueRotate = () => Theme?["hueRotate"];
        var invert = () => Theme?["invert"];
        var gap = () => Theme?["gap"];
        var gradientColorStops = () => Theme?["gradientColorStops"];
        var gradientColorStopPositions = () => Theme?["gradientColorStopPositions"];
        var inset = () => Theme?["inset"];
        var margin = () => Theme?["margin"];
        var opacity = () => Theme?["opacity"];
        var padding = () => Theme?["padding"];
        var saturate = () => Theme?["saturate"];
        var scale = () => Theme?["scale"];
        var sepia = () => Theme?["sepia"];
        var skew = () => Theme?["skew"];
        var space = () => Theme?["space"];
        var translate = () => Theme?["translate"];

        object[] any = [Validators.IsAny];
        object[] number = [Validators.IsNumber, Validators.IsArbitraryNumber];
        object[] numberAndArbitrary = [.. number, Validators.IsArbitraryValue];
        object[] spacingWithArbitrary = [Validators.IsArbitraryValue, spacing];
        object[] spacingWithAutoAndArbitrary = ["auto", Validators.IsArbitraryValue, spacing];
        object[] lengthWithEmptyAndArbitrary = ["", Validators.IsLength, Validators.IsArbitraryLength];
        object[] zeroAndEmpty = ["", "0", Validators.IsArbitraryValue];

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

        Theme = new()
        {
            ["colors"] = [Validators.IsAny],
            ["spacing"] = [Validators.IsLength, Validators.IsArbitraryLength],
            ["blur"] = ["none", "", Validators.IsTshirtSize, Validators.IsArbitraryValue],
            ["brightness"] = number,
            ["borderRadius"] = ["none", "", "full", Validators.IsTshirtSize, Validators.IsArbitraryValue],
            ["borderSpacing"] = spacingWithArbitrary,
            ["borderWidth"] = lengthWithEmptyAndArbitrary,
            ["contrast"] = number,
            ["grayscale"] = zeroAndEmpty,
            ["hueRotate"] = numberAndArbitrary,
            ["invert"] = zeroAndEmpty,
            ["gap"] = spacingWithArbitrary,
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

        // No way to reference the same object during initialization
        Theme["borderColor"] = colors();
        Theme["gradientColorStops"] = colors();

        ClassGroups = [
            /*
            * Aspect Ratio
            * See https://tailwindcss.com/docs/aspect-ratio
            */
            new ClassGroup( "aspect", "aspect", ["auto", "square", "video", Validators.IsArbitraryValue] ),
            /*
             * Container
             * See https://tailwindcss.com/docs/container
             */
            new ClassGroup( "container", ["container"] ),
            /*
             * Columns
             * See https://tailwindcss.com/docs/columns
             */
            new ClassGroup( "columns", "columns", [Validators.IsTshirtSize] ),
            /*
             * Break After
             * See https://tailwindcss.com/docs/break-after
             */
            new ClassGroup( "break-after", "break-after", breaks ),
            /*
             * Break Before
             * See https://tailwindcss.com/docs/break-before
             */
            new ClassGroup( "break-before", "break-before", breaks ),
            /*
             * Break Inside
             * See https://tailwindcss.com/docs/break-inside
             */
            new ClassGroup( "break-inside", "break-inside", ["auto", "avoid", "avoid-page", "avoid-column"] ),
            /*
             * Box Decoration Break
             * See https://tailwindcss.com/docs/box-decoration-break
             */
            new ClassGroup( "box-decoration", "box-decoration", ["slice", "clone"] ),
            /*
             * Box Sizing
             * See https://tailwindcss.com/docs/box-sizing
             */
            new ClassGroup( "box", "box", ["border", "content"] ),
            /*
             * Display
             * See https://tailwindcss.com/docs/display
             */
            new ClassGroup( "display", [
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
            new ClassGroup( "float", "float", ["right", "left", "none", "start", "end"] ),
            /*
             * Clear
             * See https://tailwindcss.com/docs/clear
             */
            new ClassGroup( "clear", "clear", ["left", "right", "both", "none", "start", "end"] ),
            /*
             * Isolation
             * See https://tailwindcss.com/docs/isolation
             */
            new ClassGroup( "isolation", ["isolate", "isolation-auto"] ),
            /*
             * Object Fit
             * See https://tailwindcss.com/docs/object-fit
             */
            new ClassGroup( "object-fit", "object", ["contain", "cover", "fill", "none", "scale-down"] ),
            /*
             * Object Position
             * See https://tailwindcss.com/docs/object-position
             */
            new ClassGroup( "object-position", "object", [.. positions, Validators.IsArbitraryValue] ),
            /*
             * Overflow
             * See https://tailwindcss.com/docs/overflow
             */
            new ClassGroup( "overflow", "overflow", overflow ),
            /*
             * Overflow X
             * See https://tailwindcss.com/docs/overflow
             */
            new ClassGroup( "overflow-x", "overflow-x", overflow ),
            /*
             * Overflow Y
             * See https://tailwindcss.com/docs/overflow
             */
            new ClassGroup( "overflow-y", "overflow-y", overflow ),
            /*
             * Overscroll Behavior
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
            new ClassGroup( "overscroll", "overscroll", overscroll ),
            /*
             * Overscroll Behavior X
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
            new ClassGroup( "overscroll-x", "overscroll-x", overscroll ),
            /*
             * Overscroll Behavior Y
             * See https://tailwindcss.com/docs/overscroll-behavior
             */
            new ClassGroup( "overscroll-y", "overscroll-y", overscroll ),
            /*
             * Position
             * See https://tailwindcss.com/docs/position
             */
            new ClassGroup( "position", ["static", "fixed", "absolute", "relative", "sticky"] ),
            /*
             * Top / Right / Bottom / Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "inset", "inset", [inset] ),
            /*
             * Right / Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "inset-x", "inset-x", [inset] ),
            /*
             * Top / Bottom
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "inset-y", "inset-y", [inset] ),
            /*
             * Start
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "start", "start", [inset] ),
            /*
             * End
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "end", "end", [inset] ),
            /*
             * Top
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "top", "top", [inset] ),
            /*
             * Right
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "right", "right", [inset] ),
            /*
             * Bottom
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "bottom", "bottom", [inset] ),
            /*
             * Left
             * See https://tailwindcss.com/docs/top-right-bottom-left
             */
            new ClassGroup( "left", "left", [inset] ),
            /*
             * Visibility
             * See https://tailwindcss.com/docs/visibility
             */
            new ClassGroup( "visibility", ["visible", "invisible", "collapse"] ),
            /*
             * Z-Index
             * See https://tailwindcss.com/docs/z-index
             */
            new ClassGroup( "z", "z", ["auto", Validators.IsInteger, Validators.IsArbitraryValue] ),
            /*
             * Flex Basis
             * See https://tailwindcss.com/docs/flex-basis
             */
            new ClassGroup( "basis", "basis", spacingWithAutoAndArbitrary ),
            /*
             * Flex Direction
             * See https://tailwindcss.com/docs/flex-direction
             */
            new ClassGroup( "flex-direction", "flex", ["row", "row-reverse", "col", "col-reverse"] ),
            /*
             * Flex Wrap
             * See https://tailwindcss.com/docs/flex-wrap
             */
            new ClassGroup( "flex-wrap", "flex", ["wrap", "wrap-reverse", "nowrap"] ),
            /*
             * Flex
             * See https://tailwindcss.com/docs/flex
             */
            new ClassGroup( "flex", "flex", ["1", "auto", "initial", "none", Validators.IsArbitraryValue] ),
            /*
             * Flex Grow
             * See https://tailwindcss.com/docs/flex-grow
             */
            new ClassGroup( "grow", "grow", zeroAndEmpty ),
            /*
             * Flex Shrink
             * See https://tailwindcss.com/docs/flex-shrink
             */
            new ClassGroup( "shrink", "shrink", zeroAndEmpty ),
            /*
             * Order
             * See https://tailwindcss.com/docs/order
             */
            new ClassGroup( "order", "order", ["first", "last", "none", Validators.IsInteger, Validators.IsArbitraryValue] ),
            /*
             * Grid Template Columns
             * See https://tailwindcss.com/docs/grid-template-columns
             */
            new ClassGroup( "grid-cols", "grid-cols", any ),
            /*
             * Grid Column Start / End
             * See https://tailwindcss.com/docs/grid-column
             */
            new ClassGroup( "col-start-end", "col", ["auto", Validators.IsArbitraryValue] ),
            /*
             * Grid Column Span
             * See https://tailwindcss.com/docs/grid-column
             */
            new ClassGroup( "col-start-end", "col-span", ["full", .. numberAndArbitrary] ),
            /*
             * Grid Column Start
             * See https://tailwindcss.com/docs/grid-column
             */
            new ClassGroup( "col-start", "col-start", ["auto", .. numberAndArbitrary] ),
            /*
             * Grid Column End
             * See https://tailwindcss.com/docs/grid-column
             */
            new ClassGroup( "col-end", "col-end", ["auto", .. numberAndArbitrary] ),
            /*
             * Grid Template Rows
             * See https://tailwindcss.com/docs/grid-template-rows
             */
            new ClassGroup( "grid-rows", "grid-rows", any ),
            /*
             * Grid Row Start / End
             * See https://tailwindcss.com/docs/grid-row
             */
            new ClassGroup( "row-start-end", "row", ["auto", Validators.IsArbitraryValue] ),
            /*
             * Grid Row Span
             * See https://tailwindcss.com/docs/grid-row
             */
            new ClassGroup( "row-start-end", "row-span", numberAndArbitrary ),
            /*
             * Grid Row Start
             * See https://tailwindcss.com/docs/grid-row
             */
            new ClassGroup( "row-start", "row-start", ["auto", .. numberAndArbitrary] ),
            /*
             * Grid Row End
             * See https://tailwindcss.com/docs/grid-row
             */
            new ClassGroup( "row-end", "row-end", ["auto", .. numberAndArbitrary] ),
            /*
             * Grid Auto Flow
             * See https://tailwindcss.com/docs/grid-auto-flow
             */
            new ClassGroup( "grid-flow", "grid-flow", ["row", "col", "dense", "row-dense", "col-dense"] ),
            /*
             * Grid Auto Columns
             * See https://tailwindcss.com/docs/grid-auto-columns
             */
            new ClassGroup( "auto-cols", "auto-cols", ["auto", "min", "max", "fr", Validators.IsArbitraryValue] ),
            /*
             * Grid Auto Rows
             * See https://tailwindcss.com/docs/grid-auto-rows
             */
            new ClassGroup( "auto-rows", "auto-rows", ["auto", "min", "max", "fr", Validators.IsArbitraryValue] ),
            /*
             * Gap
             * See https://tailwindcss.com/docs/gap
             */
            new ClassGroup( "gap", "gap", [gap] ),
            /*
             * Gap X
             * See https://tailwindcss.com/docs/gap
             */
            new ClassGroup( "gap-x", "gap-x", [gap] ),
            /*
             * Gap Y
             * See https://tailwindcss.com/docs/gap
             */
            new ClassGroup( "gap-y", "gap-y", [gap] ),
            /*
             * Justify Content
             * See https://tailwindcss.com/docs/justify-content
             */
            new ClassGroup( "justify-content", "justify", ["normal", .. align] ),
            /*
             * Justify Items
             * See https://tailwindcss.com/docs/justify-items
             */
            new ClassGroup( "justify-items", "justify-items", ["start", "end", "center", "stretch"] ),
            /*
             * Justify Self
             * See https://tailwindcss.com/docs/justify-self
             */
            new ClassGroup( "justify-self", "justify-self", ["auto", "start", "end", "center", "stretch"] ),
            /*
             * Align Content
             * See https://tailwindcss.com/docs/align-content
             */
            new ClassGroup( "align-content", "content", ["normal", "baseline", .. align] ),
            /*
             * Align Items
             * See https://tailwindcss.com/docs/align-items
             */
            new ClassGroup( "align-items", "items", ["start", "end", "center", "baseline", "stretch"] ),
            /*
             * Align Self
             * See https://tailwindcss.com/docs/align-self
             */
            new ClassGroup( "align-self", "self", ["auto", "start", "end", "center", "baseline", "stretch"] ),
            /*
             * Place Content
             * See https://tailwindcss.com/docs/place-content
             */
            new ClassGroup( "place-content", "place-content", ["baseline", .. align] ),
            /*
             * Place Items
             * See https://tailwindcss.com/docs/place-items
             */
            new ClassGroup( "place-items", "place-items", ["start", "end", "center", "baseline", "stretch"] ),
            /*
             * Place Self
             * See https://tailwindcss.com/docs/place-self
             */
            new ClassGroup( "place-self", "place-self", ["auto", "start", "end", "center", "stretch"] ),
            /*
             * Padding
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "p", "p", [padding] ),
            /*
             * Padding X
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "px", "px", [padding] ),
            /*
             * Padding Y
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "py", "py", [padding] ),
            /*
             * Padding Start
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "ps", "ps", [padding] ),
            /*
             * Padding End
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "pe", "pe", [padding] ),
            /*
             * Padding Top
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "pt", "pt", [padding] ),
            /*
             * Padding Right
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "pr", "pr", [padding] ),
            /*
             * Padding Bottom
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "pb", "pb", [padding] ),
            /*
             * Padding Left
             * See https://tailwindcss.com/docs/padding
             */
            new ClassGroup( "pl", "pl", [padding] ),
            /*
             * Margin
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "m", "m", [margin] ),
            /*
             * Margin X
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "mx", "mx", [margin] ),
            /*
             * Margin Y
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "my", "my", [margin] ),
            /*
             * Margin Start
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "ms", "ms", [margin] ),
            /*
             * Margin End
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "me", "me", [margin] ),
            /*
             * Margin Top
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "mt", "mt", [margin] ),
            /*
             * Margin Right
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "mr", "mr", [margin] ),
            /*
             * Margin Bottom
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "mb", "mb", [margin] ),
            /*
             * Margin Left
             * See https://tailwindcss.com/docs/margin
             */
            new ClassGroup( "ml", "ml", [margin] ),
            /*
             * Space Between X
             * See https://tailwindcss.com/docs/space
             */
            new ClassGroup( "space-x", "space-x", [space] ),
            /*
             * Space Between X Reverse
             * See https://tailwindcss.com/docs/space
             */
            new ClassGroup( "space-x-reverse", ["space-x-reverse"] ),
            /*
             * Space Between Y
             * See https://tailwindcss.com/docs/space
             */
            new ClassGroup( "space-y", "space-y", [space] ),
            /*
             * Space Between Y Reverse
             * See https://tailwindcss.com/docs/space
             */
            new ClassGroup( "space-y-reverse", ["space-y-reverse"] ),
            /*
             * Width
             * See https://tailwindcss.com/docs/width
             */
            new ClassGroup( "w", "w", ["auto", "min", "max", "fit", "svw", "lvw", "dvw", spacing, Validators.IsArbitraryValue] ),
            /*
             * Min-Width
             * See https://tailwindcss.com/docs/min-width
             */
            new ClassGroup( "min-w", "min-w", ["min", "max", "fit", spacing, Validators.IsArbitraryValue] ),
            /*
             * Max-Width
             * See https://tailwindcss.com/docs/max-width
             */
            new ClassGroup( "max-w", "max-w", ["none", "full", "min", "max", "fit", "prose", spacing, Validators.IsTshirtSize] ),
            /*
             * Max-Width Screen
             * See https://tailwindcss.com/docs/max-width
             */
            new ClassGroup( "max-w", "max-w-screen", [Validators.IsTshirtSize] ),
            /*
             * Height
             * See https://tailwindcss.com/docs/height
             */
            new ClassGroup( "h", "h", ["auto", "min", "max", "fit", "svh", "lvh", "dvh", spacing, Validators.IsArbitraryValue] ),
            /*
             * Min-Height
             * See https://tailwindcss.com/docs/min-height
             */
            new ClassGroup( "min-h", "min-h", ["min", "max", "fit", "svh", "lvh", "dvh", spacing, Validators.IsArbitraryValue] ),
            /*
             * Max-Height
             * See https://tailwindcss.com/docs/max-height
             */
            new ClassGroup( "max-h", "max-h", ["min", "max", "fit", "svh", "lvh", "dvh", spacing, Validators.IsArbitraryValue] ),
            /*
             * Size
             * See https://tailwindcss.com/docs/size
             */
            new ClassGroup( "size", "size", ["auto", "min", "max", "fit", spacing, Validators.IsArbitraryValue] ),
            /*
             * Font Size
             * See https://tailwindcss.com/docs/font-size
             */
            new ClassGroup( "font-size", "text", ["base", Validators.IsTshirtSize, Validators.IsArbitraryLength] ),
            /*
             * Font Smoothing
             * See https://tailwindcss.com/docs/font-smoothing
             */
            new ClassGroup( "font-smoothing", ["antialiased", "subpixel-antialiased"] ),
            /*
             * Font Style
             * See https://tailwindcss.com/docs/font-style
             */
            new ClassGroup( "font-style", ["italic", "not-italic"] ),
            /*
             * Font Weight
             * See https://tailwindcss.com/docs/font-weight
             */
            new ClassGroup( "font-weight", "font", [
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
            new ClassGroup( "font-family", "font", any ),
            /*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
            new ClassGroup( "fvn-normal", ["normal-nums"] ),
            /*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
            new ClassGroup( "fvn-ordinal", ["ordinal"] ),
            /*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
            new ClassGroup( "fvn-slashed-zero", ["slashed-zero"] ),
            /*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
            new ClassGroup( "fvn-figure", ["lining-nums", "oldstyle-nums"] ),
            /*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
            new ClassGroup( "fvn-spacing", ["proportional-nums", "tabular-nums"] ),
            /*
             * Font Variant Numeric
             * See https://tailwindcss.com/docs/font-variant-numeric
             */
            new ClassGroup( "fvn-fraction", ["diagonal-fractions", "stacked-fractions"] ),
            /*
             * Letter Spacing
             * See https://tailwindcss.com/docs/letter-spacing
             */
            new ClassGroup( "tracking", "tracking", [
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
            new ClassGroup( "line-clamp", "line-clamp", ["none", .. number] ),
            /*
             * Line Height
             * See https://tailwindcss.com/docs/line-height
             */
            new ClassGroup( "leading", "leading", [
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
            new ClassGroup( "list-image", "list-image", ["none", Validators.IsArbitraryValue] ),
            /*
             * List Style Type
             * See https://tailwindcss.com/docs/list-style-type
             */
            new ClassGroup( "list-style-type", "list", ["none", "disc", "decimal", Validators.IsArbitraryValue] ),
            /*
             * List Style Position
             * See https://tailwindcss.com/docs/list-style-position
             */
            new ClassGroup( "list-style-position", "list", ["inside", "outside"] ),
            /*
             * Placeholder Color
             * See https://tailwindcss.com/docs/placeholder-color
             */
            new ClassGroup( "placeholder-color", "placeholder", [colors] ),
            /*
             * Text Alignment
             * See https://tailwindcss.com/docs/text-align
             */
            new ClassGroup( "text-alignment", "text", ["left", "center", "right", "justify", "start", "end"] ),
            /*
             * Text Color
             * See https://tailwindcss.com/docs/text-color
             */
            new ClassGroup( "text-color", "text", [colors] ),
            /*
             * Text Decoration
             * See https://tailwindcss.com/docs/text-decoration
             */
            new ClassGroup( "text-decoration", ["underline", "overline", "line-through", "no-underline"] ),
            /*
             * Text Decoration Style
             * See https://tailwindcss.com/docs/text-decoration-style
             */
            new ClassGroup( "text-decoration-style", "decoration", ["wavy", .. lineStyles] ),
            /*
             * Text Decoration Color
             * See https://tailwindcss.com/docs/text-decoration-color
             */
            new ClassGroup( "text-decoration-color", "decoration", [colors] ),
            /*
             * Text Decoration Thickness
             * See https://tailwindcss.com/docs/text-decoration-thickness
             */
            new ClassGroup( "text-decoration-thickness", "decoration", ["auto", "from-font", Validators.IsLength, Validators.IsArbitraryLength] ),
            /*
             * Text Underline Offset
             * See https://tailwindcss.com/docs/text-underline-offset
             */
            new ClassGroup( "underline-offset", "underline-offset", ["auto", Validators.IsLength, Validators.IsArbitraryValue] ),
            /*
             * Text Transform
             * See https://tailwindcss.com/docs/text-transform
             */
            new ClassGroup( "text-transform", ["uppercase", "lowercase", "capitalize", "normal-case"] ),
            /*
             * Text Overflow
             * See https://tailwindcss.com/docs/text-overflow
             */
            new ClassGroup( "text-overflow", ["truncate", "text-ellipsis", "text-clip"] ),
            /*
             * Text Wrap
             * See https://tailwindcss.com/docs/text-wrap
             */
            new ClassGroup( "text-wrap", "text", ["wrap", "nowrap", "balance", "pretty"] ),
            /*
             * Text Indent
             * See https://tailwindcss.com/docs/text-indent
             */
            new ClassGroup( "indent", "indent", spacingWithArbitrary ),
            /*
             * Vertical Alignment
             * See https://tailwindcss.com/docs/vertical-align
             */
            new ClassGroup( "vertical-align", "align", [
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
            new ClassGroup( "whitespace", "whitespace", [
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
            new ClassGroup( "break", "break", ["normal", "words", "all", "keep"] ),
            /*
             * Hyphens
             * See https://tailwindcss.com/docs/hyphens
             */
            new ClassGroup( "hyphens", "hyphens", ["none", "manual", "auto"] ),
            /*
             * Content
             * See https://tailwindcss.com/docs/content
             */
            new ClassGroup( "content", "content", ["none", Validators.IsArbitraryValue] ),
            /*
             * Background Attachment
             * See https://tailwindcss.com/docs/background-attachment
             */
            new ClassGroup( "bg-attachment", "bg", ["fixed", "local", "scroll"] ),
            /*
             * Background Clip
             * See https://tailwindcss.com/docs/background-clip
             */
            new ClassGroup( "bg-clip", "bg-clip", ["border", "padding", "content", "text"] ),
            /*
             * Background Origin
             * See https://tailwindcss.com/docs/background-origin
             */
            new ClassGroup( "bg-origin", "bg-origin", ["border", "padding", "content"] ),
            /*
             * Background Position
             * See https://tailwindcss.com/docs/background-position
             */
            new ClassGroup( "bg-position", "bg", [.. positions, Validators.IsArbitraryPosition] ),
            /*
             * Background Repeat
             * See https://tailwindcss.com/docs/background-repeat
             */
            new ClassGroup( "bg-repeat", "bg", ["no-repeat"] ),
            /*
             * Background Repeat
             * See https://tailwindcss.com/docs/background-repeat
             */
            new ClassGroup( "bg-repeat", "bg-repeat", ["", "x", "y", "round", "space"] ),
            /*
             * Background Size
             * See https://tailwindcss.com/docs/background-size
             */
            new ClassGroup( "bg-size", "bg", ["auto", "cover", "contain", Validators.IsArbitrarySize] ),
            /*
             * Background Image
             * See https://tailwindcss.com/docs/background-image
             */
            new ClassGroup( "bg-image", "bg", ["none", Validators.IsArbitraryImage] ),
            /*
             * Background Image Gradient To
             * See https://tailwindcss.com/docs/background-image
             */
            new ClassGroup( "bg-image", "bg-gradient-to", ["t", "tr", "r", "br", "b", "bl", "l", "tl"] ),
            /*
             * Background Color
             * See https://tailwindcss.com/docs/background-color
             */
            new ClassGroup( "bg-color", "bg", [colors] ),
            /*
             * Gradient Color Stops From Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
            new ClassGroup( "gradient-from-pos", "from", [gradientColorStopPositions] ),
            /*
             * Gradient Color Stops Via Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
            new ClassGroup( "gradient-via-pos", "via", [gradientColorStopPositions] ),
            /*
             * Gradient Color Stops To Position
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
            new ClassGroup( "gradient-to-pos", "to", [gradientColorStopPositions] ),
            /*
             * Gradient Color Stops From
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
            new ClassGroup( "gradient-from", "from", [gradientColorStops] ),
            /*
             * Gradient Color Stops Via
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
            new ClassGroup( "gradient-via", "via", [gradientColorStops] ),
            /*
             * Gradient Color Stops To
             * See https://tailwindcss.com/docs/gradient-color-stops
             */
            new ClassGroup( "gradient-to", "to", [gradientColorStops] ),
            /*
             * Border Radius
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded", "rounded", [borderRadius] ),
            /*
             * Border Radius Start
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-s", "rounded-s", [borderRadius] ),
            /*
             * Border Radius End
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-e", "rounded-e", [borderRadius] ),
            /*
             * Border Radius Top
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-t", "rounded-t", [borderRadius] ),
            /*
             * Border Radius Right
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-r", "rounded-r", [borderRadius] ),
            /*
             * Border Radius Bottom
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-b", "rounded-b", [borderRadius] ),
            /*
             * Border Radius Left
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-l", "rounded-l", [borderRadius] ),
            /*
             * Border Radius Start Start
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-ss", "rounded-ss", [borderRadius] ),
            /*
             * Border Radius Start End
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-se", "rounded-se", [borderRadius] ),
            /*
             * Border Radius End End
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-ee", "rounded-ee", [borderRadius] ),
            /*
             * Border Radius End Start
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-es", "rounded-es", [borderRadius] ),
            /*
             * Border Radius Top Left
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-tl", "rounded-tl", [borderRadius] ),
            /*
             * Border Radius Top Right
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-tr", "rounded-tr", [borderRadius] ),
            /*
             * Border Radius Bottom Right
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-br", "rounded-br", [borderRadius] ),
            /*
             * Border Radius Bottom Left
             * See https://tailwindcss.com/docs/border-radius
             */
            new ClassGroup( "rounded-bl", "rounded-bl", [borderRadius] ),
            /*
             * Border Width
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w", "border", [borderWidth] ),
            /*
             * Border Width X
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-x", "border-x", [borderWidth] ),
            /*
             * Border Width Y
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-y", "border-y", [borderWidth] ),
            /*
             * Border Width Start
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-s", "border-s", [borderWidth] ),
            /*
             * Border Width End
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-e", "border-e", [borderWidth] ),
            /*
             * Border Width Top
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-t", "border-t", [borderWidth] ),
            /*
             * Border Width Right
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-r", "border-r", [borderWidth] ),
            /*
             * Border Width Bottom
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-b", "border-b", [borderWidth] ),
            /*
             * Border Width Left
             * See https://tailwindcss.com/docs/border-width
             */
            new ClassGroup( "border-w-l", "border-l", [borderWidth] ),
            /*
             * Border Style
             * See https://tailwindcss.com/docs/border-style
             */
            new ClassGroup( "border-style", "border", ["hidden", .. lineStyles] ),
            /*
             * Divide Width X
             * See https://tailwindcss.com/docs/divide-width
             */
            new ClassGroup( "divide-x", "divide-x", [borderWidth] ),
            /*
             * Divide Width X Reverse
             * See https://tailwindcss.com/docs/divide-width
             */
            new ClassGroup( "divide-x-reverse", ["divide-x-reverse"] ),
            /*
             * Divide Width Y
             * See https://tailwindcss.com/docs/divide-width
             */
            new ClassGroup( "divide-y", "divide-y", [borderWidth] ),
            /*
             * Divide Width Y Reverse
             * See https://tailwindcss.com/docs/divide-width
             */
            new ClassGroup( "divide-y-reverse", ["divide-y-reverse"] ),
            /*
             * Divide Style
             * See https://tailwindcss.com/docs/divide-style
             */
            new ClassGroup( "divide-style", "divide", lineStyles ),
            /*
             * Border Color
             * See https://tailwindcss.com/docs/border-color
             */
            new ClassGroup( "border-color", "border", [borderColor] ),
            /*
             * Border Color X
             * See https://tailwindcss.com/docs/border-color
             */
            new ClassGroup( "border-color-x", "border-x", [borderColor] ),
            /*
             * Border Color Y
             * See https://tailwindcss.com/docs/border-color
             */
            new ClassGroup( "border-color-y", "border-y", [borderColor] ),
            /*
             * Border Color Top
             * See https://tailwindcss.com/docs/border-color
             */
            new ClassGroup( "border-color-t", "border-t", [borderColor] ),
            /*
             * Border Color Right
             * See https://tailwindcss.com/docs/border-color
             */
            new ClassGroup( "border-color-r", "border-r", [borderColor] ),
            /*
             * Border Color Bottom
             * See https://tailwindcss.com/docs/border-color
             */
            new ClassGroup( "border-color-b", "border-b", [borderColor] ),
            /*
             * Border Color Left
             * See https://tailwindcss.com/docs/border-color
             */
            new ClassGroup( "border-color-l", "border-l", [borderColor] ),
            /*
             * Divide Color
             * See https://tailwindcss.com/docs/divide-color
             */
            new ClassGroup( "divide-color", "divide", [borderColor] ),
            /*
             * Outline Style
             * See https://tailwindcss.com/docs/outline-style
             */
            new ClassGroup( "outline-style", "outline", ["", .. lineStyles] ),
            /*
             * Outline Offset
             * See https://tailwindcss.com/docs/outline-offset
             */
            new ClassGroup( "outline-offset", "outline-offset", [Validators.IsLength, Validators.IsArbitraryLength] ),
            /*
             * Outline Width
             * See https://tailwindcss.com/docs/outline-width
             */
            new ClassGroup( "outline-w", "outline", [Validators.IsLength, Validators.IsArbitraryLength] ),
            /*
             * Outline Color
             * See https://tailwindcss.com/docs/outline-color
             */
            new ClassGroup( "outline-color", "outline", [colors] ),
            /*
             * Ring Width
             * See https://tailwindcss.com/docs/ring-width
             */
            new ClassGroup( "ring-w", "ring", lengthWithEmptyAndArbitrary ),
            /*
             * Ring Width Inset
             * See https://tailwindcss.com/docs/ring-width
             */
            new ClassGroup( "ring-w-inset", ["ring-inset"] ),
            /*
             * Ring Color
             * See https://tailwindcss.com/docs/ring-color
             */
            new ClassGroup( "ring-color", "ring", [colors] ),
            /*
             * Ring Offset Width
             * See https://tailwindcss.com/docs/ring-offset-width
             */
            new ClassGroup( "ring-offset-w", "ring-offset", [Validators.IsLength, Validators.IsArbitraryLength] ),
            /*
             * Ring Offset Color
             * See https://tailwindcss.com/docs/ring-offset-color
             */
            new ClassGroup( "ring-offset-color", "ring-offset", [colors] ),
            /*
             * Shadow
             * See https://tailwindcss.com/docs/shadow
             */
            new ClassGroup( "shadow", "shadow", ["", "inner", "none", Validators.IsTshirtSize, Validators.IsArbitraryShadow] ),
            /*
             * Shadow Color
             * See https://tailwindcss.com/docs/shadow-color
             */
            new ClassGroup( "shadow-color", "shadow", any ),
            /*
             * Opacity
             * See https://tailwindcss.com/docs/opacity
             */
            new ClassGroup( "opacity", "opacity", [opacity] ),
            /*
             * Mix Blend Mode
             * See https://tailwindcss.com/docs/mix-blend-mode
             */
            new ClassGroup( "mix-blend", "mix-blend", ["plus-lighter", "plus-darker", .. blendModes] ),
            /*
             * Background Blend Mode
             * See https://tailwindcss.com/docs/mix-blend-mode
             */
            new ClassGroup( "bg-blend", "bg-blend", blendModes ),
            /*
             * Blur
             * See https://tailwindcss.com/docs/blur
             */
            new ClassGroup( "blur", "blur", [blur] ),
            /*
             * Brightness
             * See https://tailwindcss.com/docs/brightness
             */
            new ClassGroup( "brightness", "brightness", [brightness] ),
            /*
             * Contrast
             * See https://tailwindcss.com/docs/contrast
             */
            new ClassGroup( "contrast", "contrast", [contrast] ),
            /*
             * Drop Shadow
             * See https://tailwindcss.com/docs/drop-shadow
             */
            new ClassGroup( "drop-shadow", "drop-shadow", ["none", "", Validators.IsTshirtSize, Validators.IsArbitraryValue] ),
            /*
             * Grayscale
             * See https://tailwindcss.com/docs/grayscale
             */
            new ClassGroup( "grayscale", "grayscale", [grayscale] ),
            /*
             * Hue Rotate
             * See https://tailwindcss.com/docs/hue-rotate
             */
            new ClassGroup( "hue-rotate", "hue-rotate", [hueRotate] ),
            /*
             * Invert
             * See https://tailwindcss.com/docs/invert
             */
            new ClassGroup( "invert", "invert", [invert] ),
            /*
             * Saturate
             * See https://tailwindcss.com/docs/saturate
             */
            new ClassGroup( "saturate", "saturate", [saturate] ),
            /*
             * Sepia
             * See https://tailwindcss.com/docs/sepia
             */
            new ClassGroup( "sepia", "sepia", [sepia] ),
            /*
             * Backdrop Blur
             * See https://tailwindcss.com/docs/backdrop-blur
             */
            new ClassGroup( "backdrop-blur", "backdrop-blur", [blur] ),
            /*
             * Backdrop Brightness
             * See https://tailwindcss.com/docs/backdrop-brightness
             */
            new ClassGroup( "backdrop-brightness", "backdrop-brightness", [brightness] ),
            /*
             * Backdrop Contrast
             * See https://tailwindcss.com/docs/backdrop-contrast
             */
            new ClassGroup( "backdrop-contrast", "backdrop-contrast", [contrast] ),
            /*
             * Backdrop Grayscale
             * See https://tailwindcss.com/docs/backdrop-grayscale
             */
            new ClassGroup( "backdrop-grayscale", "backdrop-grayscale", [grayscale] ),
            /*
             * Backdrop Hue Rotate
             * See https://tailwindcss.com/docs/backdrop-hue-rotate
             */
            new ClassGroup( "backdrop-hue-rotate", "backdrop-hue-rotate", [hueRotate] ),
            /*
             * Backdrop Invert
             * See https://tailwindcss.com/docs/backdrop-invert
             */
            new ClassGroup( "backdrop-invert", "backdrop-invert", [invert] ),
            /*
             * Backdrop Opacity
             * See https://tailwindcss.com/docs/backdrop-opacity
             */
            new ClassGroup( "backdrop-opacity", "backdrop-opacity", [opacity] ),
            /*
             * Backdrop Saturate
             * See https://tailwindcss.com/docs/backdrop-saturate
             */
            new ClassGroup( "backdrop-saturate", "backdrop-saturate", [saturate] ),
            /*
             * Backdrop Sepia
             * See https://tailwindcss.com/docs/backdrop-sepia
             */
            new ClassGroup( "backdrop-sepia", "backdrop-sepia", [sepia] ),
            /*
             * Border Collapse
             * See https://tailwindcss.com/docs/border-collapse
             */
            new ClassGroup( "border-collapse", "border", ["collapse", "separate"] ),
            /*
             * Border Spacing
             * See https://tailwindcss.com/docs/border-spacing
             */
            new ClassGroup( "border-spacing", "border-spacing", [borderSpacing] ),
            /*
             * Border Spacing X
             * See https://tailwindcss.com/docs/border-spacing
             */
            new ClassGroup( "border-spacing-x", "border-spacing-x", [borderSpacing] ),
            /*
             * Border Spacing Y
             * See https://tailwindcss.com/docs/border-spacing
             */
            new ClassGroup( "border-spacing-y", "border-spacing-y", [borderSpacing] ),
            /*
             * Table Layout
             * See https://tailwindcss.com/docs/table-layout
             */
            new ClassGroup( "table-layout", "table", ["auto", "fixed"] ),
            /*
             * Caption Side
             * See https://tailwindcss.com/docs/caption-side
             */
            new ClassGroup( "caption", "caption", ["top", "bottom"] ),
            /*
             * Transition Property
             * See https://tailwindcss.com/docs/transition-property
             */
            new ClassGroup( "transition", "transition", ["none", "all", "", "colors", "opacity", "shadow", "transform", Validators.IsArbitraryValue] ),
            /*
             * Transition Duration
             * See https://tailwindcss.com/docs/transition-duration
             */
            new ClassGroup( "duration", "duration", numberAndArbitrary ),
            /*
             * Transition Timing Function
             * See https://tailwindcss.com/docs/transition-timing-function
             */
            new ClassGroup( "ease", "ease", ["linear", "in", "out", "in-out", Validators.IsArbitraryValue] ),
            /*
             * Transition Delay
             * See https://tailwindcss.com/docs/transition-delay
             */
            new ClassGroup( "delay", "delay", numberAndArbitrary ),
            /*
             * Animation
             * See https://tailwindcss.com/docs/animation
             */
            new ClassGroup( "animate", "animate", ["none", "spin", "ping", "pulse", "bounce", Validators.IsArbitraryValue] ),
            /*
             * Transform
             * See https://tailwindcss.com/docs/transform
             */
            new ClassGroup( "transform", "transform", ["", "gpu", "none"] ),
            /*
             * Scale
             * See https://tailwindcss.com/docs/scale
             */
            new ClassGroup( "scale", "scale", [scale] ),
            /*
             * Scale X
             * See https://tailwindcss.com/docs/scale
             */
            new ClassGroup( "scale-x", "scale-x", [scale] ),
            /*
             * Scale Y
             * See https://tailwindcss.com/docs/scale
             */
            new ClassGroup( "scale-y", "scale-y", [scale] ),
            /*
             * Rotate
             * See https://tailwindcss.com/docs/rotate
             */
            new ClassGroup( "rotate", "rotate", [Validators.IsInteger, Validators.IsArbitraryValue] ),
            /*
             * Translate X
             * See https://tailwindcss.com/docs/translate
             */
            new ClassGroup( "translate-x", "translate-x", [translate] ),
            /*
             * Translate Y
             * See https://tailwindcss.com/docs/translate
             */
            new ClassGroup( "translate-y", "translate-y", [translate] ),
            /*
             * Skew X
             * See https://tailwindcss.com/docs/skew
             */
            new ClassGroup( "skew-x", "skew-x", [skew] ),
            /*
             * Skew Y
             * See https://tailwindcss.com/docs/skew
             */
            new ClassGroup( "skew-y", "skew-y", [skew] ),
            /*
             * Transform Origin
             * See https://tailwindcss.com/docs/transform-origin
             */
            new ClassGroup( "transform-origin", "origin", [
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
             * Accent
             * See https://tailwindcss.com/docs/accent
             */
            new ClassGroup( "accent", "accent", ["auto", colors] ),
            /*
             * Appearance
             * See https://tailwindcss.com/docs/appearance
             */
            new ClassGroup( "appearance", "appearance", autoAndNone ),
            /*
             * Cursor
             * See https://tailwindcss.com/docs/cursor
             */
            new ClassGroup( "cursor", "cursor", [
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
            new ClassGroup( "caret-color", "caret", [colors] ),
            /*
             * Pointer Events
             * See https://tailwindcss.com/docs/pointer-events
             */
            new ClassGroup( "pointer-events", "pointer-events", autoAndNone ),
            /*
             * Resize
             * See https://tailwindcss.com/docs/resize
             */
            new ClassGroup( "resize", "resize", ["none", "y", "x", ""] ),
            /*
             * Scroll Behavior
             * See https://tailwindcss.com/docs/scroll-behavior
             */
            new ClassGroup( "scroll-behavior", "scroll", ["auto", "smooth"] ),
            /*
             * Scroll Margin
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-m", "scroll-m", spacingWithArbitrary ),
            /*
             * Scroll Margin X
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-mx", "scroll-mx", spacingWithArbitrary ),
            /*
             * Scroll Margin Y
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-my", "scroll-my", spacingWithArbitrary ),
            /*
             * Scroll Margin Start
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-ms", "scroll-ms", spacingWithArbitrary ),
            /*
             * Scroll Margin End
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-me", "scroll-me", spacingWithArbitrary ),
            /*
             * Scroll Margin Top
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-mt", "scroll-mt", spacingWithArbitrary ),
            /*
             * Scroll Margin Right
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-mr", "scroll-mr", spacingWithArbitrary ),
            /*
             * Scroll Margin Bottom
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-mb", "scroll-mb", spacingWithArbitrary ),
            /*
             * Scroll Margin Left
             * See https://tailwindcss.com/docs/scroll-margin
             */
            new ClassGroup( "scroll-ml", "scroll-ml", spacingWithArbitrary ),
            /*
             * Scroll Padding
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-p", "scroll-p", spacingWithArbitrary ),
            /*
             * Scroll Padding X
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-px", "scroll-px", spacingWithArbitrary ),
            /*
             * Scroll Padding Y
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-py", "scroll-py", spacingWithArbitrary ),
            /*
             * Scroll Padding Start
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-ps", "scroll-ps", spacingWithArbitrary ),
            /*
             * Scroll Padding End
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-pe", "scroll-pe", spacingWithArbitrary ),
            /*
             * Scroll Padding Top
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-pt", "scroll-pt", spacingWithArbitrary ),
            /*
             * Scroll Padding Right
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-pr", "scroll-pr", spacingWithArbitrary ),
            /*
             * Scroll Padding Bottom
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-pb", "scroll-pb", spacingWithArbitrary ),
            /*
             * Scroll Padding Left
             * See https://tailwindcss.com/docs/scroll-padding
             */
            new ClassGroup( "scroll-pl", "scroll-pl", spacingWithArbitrary ),
            /*
             * Scroll Snap Align
             * See https://tailwindcss.com/docs/scroll-snap-align
             */
            new ClassGroup( "snap-align", "snap", ["start", "end", "center", "align-none"] ),
            /*
             * Scroll Snap Stop
             * See https://tailwindcss.com/docs/scroll-snap-stop
             */
            new ClassGroup( "snap-stop", "snap", ["normal", "always"] ),
            /*
             * Scroll Snap Type
             * See https://tailwindcss.com/docs/scroll-snap-type
             */
            new ClassGroup( "snap-type", "snap", ["none", "x", "y", "both"] ),
            /*
             * Scroll Snap Type Strictness
             * See https://tailwindcss.com/docs/scroll-snap-type
             */
            new ClassGroup( "snap-strictness", "snap", ["mandatory", "proximity"] ),
            /*
             * Touch Action
             * See https://tailwindcss.com/docs/touch-action
             */
            new ClassGroup( "touch", "touch", ["auto", "none", "manipulation"] ),
            /*
             * Touch Action X
             * See https://tailwindcss.com/docs/touch-action
             */
            new ClassGroup( "touch-x", "touch-pan", ["x", "left", "right"] ),
            /*
             * Touch Action Y
             * See https://tailwindcss.com/docs/touch-action
             */
            new ClassGroup( "touch-y", "touch-pan", ["y", "up", "down"] ),
            /*
             * Touch Action Pinch Zoom
             * See https://tailwindcss.com/docs/touch-action
             */
            new ClassGroup( "touch-pz", ["touch-pinch-zoom"] ),
            /*
             * User Select
             * See https://tailwindcss.com/docs/user-select
             */
            new ClassGroup( "select", "select", ["none", "text", "all", "auto"] ),
            /*
             * Will Change
             * See https://tailwindcss.com/docs/will-change
             */
            new ClassGroup( "will-change", "will-change", ["auto", "scroll", "contents", "transform", Validators.IsArbitraryValue] ),
            /*
             * Fill
             * See https://tailwindcss.com/docs/fill
             */
            new ClassGroup( "fill", "fill", ["none", colors] ),
            /*
             * Stroke Width
             * See https://tailwindcss.com/docs/stroke-width
             */
            new ClassGroup( "stroke-w", "stroke", [Validators.IsLength, Validators.IsArbitraryLength, Validators.IsArbitraryNumber] ),
            /*
             * Stroke
             * See https://tailwindcss.com/docs/stroke
             */
            new ClassGroup( "stroke", "stroke", ["none", colors] ),
            /*
             * Screen Readers
             * See https://tailwindcss.com/docs/screen-readers
             */
            new ClassGroup( "sr", ["sr-only", "not-sr-only"] ),
            /*
             * Forced Color Adjust
             * See https://tailwindcss.com/docs/forced-color-adjust
             */
            new ClassGroup( "forced-color-adjust", "forced-color-adjust", autoAndNone )
        ];

        ConflictingClassGroups = _conflictingClassGroups.AsReadOnly();
        ConflictingClassGroupModifiers = _conflictingClassGroupModifiers.AsReadOnly();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TwMergeConfig"/> class with default settings.
    /// </summary>
    /// <returns>A <see cref="TwMergeConfig"/> instance.</returns>
    public static TwMergeConfig Default() => new();

    /// <summary>
    /// Extends the current configuration with additional class groups.
    /// </summary>
    /// <param name="classGroups">An array of class groups to be added to the configuration.</param>
    /// <returns>A <see cref="TwMergeConfig"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown if any class group Id does not exist in the list of supported class group keys.</exception>
    public void Extend( ExtendedConfig extendedConfig )
    {
        if( extendedConfig.Theme is not null )
        {
            foreach( var (key, values) in extendedConfig.Theme )
            {
                if( Theme.TryGetValue( key, out var initialValues ) )
                {
                    Theme[key] = [.. initialValues, .. values];
                }
            }
        }
    }
}
