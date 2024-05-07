using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

internal class TwMergeConfig
{
    private static readonly Func<string, bool>[] _any = [Validators.IsAny];
    private static readonly Func<string, bool>[] _arbitraryValue = [Validators.IsArbitraryValue];
    private static readonly Func<string, bool>[] _lengthAndArbitrary = [Validators.IsLength, Validators.IsArbitraryLength];
    private static readonly Func<string, bool>[] _numberAndArbitrary = [Validators.IsNumber, Validators.IsArbitraryNumber];
    private static readonly Func<string, bool>[] _numberAndArbitraryValue = [Validators.IsNumber, Validators.IsArbitraryValue];
    private static readonly Func<string, bool>[] _integerAndArbitraryValue = [Validators.IsInteger, Validators.IsArbitraryValue];
    private static readonly Func<string, bool>[] _tShirtSizeAndArbitraryValue = [Validators.IsTshirtSize, Validators.IsArbitraryValue];

    private static readonly Func<string, bool>[] _spacings = [.. _lengthAndArbitrary, Validators.IsArbitraryValue];
    private static readonly Func<string, bool>[] _gradientColorStopPositions = [Validators.IsPercent, Validators.IsArbitraryLength];

    private static readonly string[] _auto = ["auto"];
    private static readonly string[] _none = ["none"];
    private static readonly string[] _autoAndNone = [.. _auto, .. _none];
    private static readonly string[] _zeroAndEmpty = ["0", ""];

    private static readonly string[] _align = ["start", "end", "center", "between", "around", "evenly", "stretch"];
    private static readonly string[] _breaks = ["auto", "avoid", "all", "avoid-page", "page", "left", "right", "column"];
    private static readonly string[] _lineStyles = ["solid", "dashed", "dotted", "double", "none"];
    private static readonly string[] _overscroll = ["auto", "contain", "none"];
    private static readonly string[] _overflow = ["auto", "hidden", "clip", "visible", "scroll"];
    private static readonly string[] _blendModes = [
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
    private static readonly string[] _positions = [
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

    private static readonly (string[] Items, Func<string, bool>[] Validators) _borderWidth =
        new( [""], _lengthAndArbitrary );

    private static readonly (string[] Items, Func<string, bool>[] Validators) _borderRadius =
        new( ["none", "full", ""], _tShirtSizeAndArbitraryValue );

    private static readonly (string[] Items, Func<string, bool>[] Validators) _blur =
        new( ["none", ""], _tShirtSizeAndArbitraryValue );

    private static readonly ClassGroup[] _classGroups = [
        /*
         * Aspect Ratio
         * See https://tailwindcss.com/docs/aspect-ratio
         */
        new ClassGroup( "aspect", "aspect", ["auto", "square", "video"], _arbitraryValue ),
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
        new ClassGroup( "break-after", "break-after", _breaks ),
        /*
         * Break Before
         * See https://tailwindcss.com/docs/break-before
         */
        new ClassGroup( "break-before", "break-before", _breaks ),
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
            "hidden"
        ] ),
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
        new ClassGroup( "object-position", "object", _positions, _arbitraryValue ),
        /*
         * Overflow
         * See https://tailwindcss.com/docs/overflow
         */
        new ClassGroup( "overflow", "overflow", _overflow ),
        /*
         * Overflow X
         * See https://tailwindcss.com/docs/overflow
         */
        new ClassGroup( "overflow-x", "overflow-x", _overflow ),
        /*
         * Overflow Y
         * See https://tailwindcss.com/docs/overflow
         */
        new ClassGroup( "overflow-y", "overflow-y", _overflow ),
        /*
         * Overscroll Behavior
         * See https://tailwindcss.com/docs/overscroll-behavior
         */
        new ClassGroup( "overscroll", "overscroll", _overscroll ),
        /*
         * Overscroll Behavior X
         * See https://tailwindcss.com/docs/overscroll-behavior
         */
        new ClassGroup( "overscroll-x", "overscroll-x", _overscroll ),
        /*
         * Overscroll Behavior Y
         * See https://tailwindcss.com/docs/overscroll-behavior
         */
        new ClassGroup( "overscroll-y", "overscroll-y", _overscroll ),
        /*
         * Position
         * See https://tailwindcss.com/docs/position
         */
        new ClassGroup( "position", "position", ["static", "fixed", "absolute", "relative", "sticky"] ),
        /*
         * Top / Right / Bottom / Left
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "inset", "inset", _auto, _spacings ),
        /*
         * Right / Left
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "inset-x", "inset-x", _auto, _spacings ),
        /*
         * Top / Bottom
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "inset-y", "inset-y", _auto, _spacings ),
        /*
         * Start
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "start", "start", _auto, _spacings ),
        /*
         * End
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "end", "end", _auto, _spacings ),
        /*
         * Top
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "top", "top", _auto, _spacings ),
        /*
         * Right
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "right", "right", _auto, _spacings ),
        /*
         * Bottom
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "bottom", "bottom", _auto, _spacings ),
        /*
         * Left
         * See https://tailwindcss.com/docs/top-right-bottom-left
         */
        new ClassGroup( "left", "left", _auto, _spacings ),
        /*
         * Visibility
         * See https://tailwindcss.com/docs/visibility
         */
        new ClassGroup( "visibility", ["visible", "invisible", "collapse"] ),
        /*
         * Z-Index
         * See https://tailwindcss.com/docs/z-index
         */
        new ClassGroup( "z", "z", _auto, _integerAndArbitraryValue ),
        /*
         * Flex Basis
         * See https://tailwindcss.com/docs/flex-basis
         */
        new ClassGroup( "flex-basis", "basis", _auto, _spacings ),
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
        new ClassGroup( "flex", "flex", ["1", "auto", "initial", "none"], _arbitraryValue ),
        /*
         * Flex Grow
         * See https://tailwindcss.com/docs/flex-grow
         */
        new ClassGroup( "grow", "grow", _zeroAndEmpty, _arbitraryValue ),
        /*
         * Flex Shrink
         * See https://tailwindcss.com/docs/flex-shrink
         */
        new ClassGroup( "shrink", "shrink", _zeroAndEmpty, _arbitraryValue ),
        /*
         * Order
         * See https://tailwindcss.com/docs/order
         */
        new ClassGroup( "order", "order", ["first", "last", "none"], _integerAndArbitraryValue ),
        /*
         * Grid Template Columns
         * See https://tailwindcss.com/docs/grid-template-columns
         */
        new ClassGroup( "grid-cols", "grid-cols", _any ),
        /*
         * Grid Column Start / End
         * See https://tailwindcss.com/docs/grid-column
         */
        new ClassGroup( "col-start-end", "col", _auto, _arbitraryValue ),
        /*
         * Grid Column Span
         * See https://tailwindcss.com/docs/grid-column
         */
        new ClassGroup( "col-start-end", "col-span", ["full"], _numberAndArbitraryValue ),
        /*
         * Grid Column Start
         * See https://tailwindcss.com/docs/grid-column
         */
        new ClassGroup( "col-start", "col-start", _auto, _numberAndArbitraryValue ),
        /*
         * Grid Column End
         * See https://tailwindcss.com/docs/grid-column
         */
        new ClassGroup( "col-end", "col-end", _auto, _numberAndArbitraryValue ),
        /*
         * Grid Template Rows
         * See https://tailwindcss.com/docs/grid-template-rows
         */
        new ClassGroup( "grid-rows", "grid-rows", _any ),
        /*
         * Grid Row Start / End
         * See https://tailwindcss.com/docs/grid-row
         */
        new ClassGroup( "row-start-end", "row", _auto, _arbitraryValue ),
        /*
         * Grid Row Span
         * See https://tailwindcss.com/docs/grid-row
         */
        new ClassGroup( "row-start-end", "row-span", _numberAndArbitraryValue ),
        /*
         * Grid Row Start
         * See https://tailwindcss.com/docs/grid-row
         */
        new ClassGroup( "row-start", "row-start", _auto, _numberAndArbitraryValue ),
        /*
         * Grid Row End
         * See https://tailwindcss.com/docs/grid-row
         */
        new ClassGroup( "row-end", "row-end", _auto, _numberAndArbitraryValue ),
        /*
         * Grid Auto Flow
         * See https://tailwindcss.com/docs/grid-auto-flow
         */
        new ClassGroup( "grid-flow", "grid-flow", ["row", "col", "dense", "row-dense", "col-dense"] ),
        /*
         * Grid Auto Columns
         * See https://tailwindcss.com/docs/grid-auto-columns
         */
        new ClassGroup( "auto-cols", "auto-cols", ["auto", "min", "max", "fr"], _arbitraryValue ),
        /*
         * Grid Auto Rows
         * See https://tailwindcss.com/docs/grid-auto-rows
         */
        new ClassGroup( "auto-rows", "auto-rows", ["auto", "min", "max", "fr"], _arbitraryValue ),
        /*
         * Gap
         * See https://tailwindcss.com/docs/gap
         */
        new ClassGroup( "gap", "gap", _spacings ),
        /*
         * Gap X
         * See https://tailwindcss.com/docs/gap
         */
        new ClassGroup( "gap-x", "gap-x", _spacings ),
        /*
         * Gap Y
         * See https://tailwindcss.com/docs/gap
         */
        new ClassGroup( "gap-y", "gap-y", _spacings ),
        /*
         * Justify Content
         * See https://tailwindcss.com/docs/justify-content
         */
        new ClassGroup( "justify-content", "justify", ["normal", .. _align] ),
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
        new ClassGroup( "align-content", "content", ["normal", "baseline", .. _align] ),
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
        new ClassGroup( "place-content", "place-content", ["baseline", .. _align] ),
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
        new ClassGroup( "p", "p", _spacings ),
        /*
         * Padding X
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "px", "px", _spacings ),
        /*
         * Padding Y
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "py", "py", _spacings ),
        /*
         * Padding Start
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "ps", "ps", _spacings ),
        /*
         * Padding End
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "pe", "pe", _spacings ),
        /*
         * Padding Top
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "pt", "pt", _spacings ),
        /*
         * Padding Right
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "pr", "pr", _spacings ),
        /*
         * Padding Bottom
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "pb", "pb", _spacings ),
        /*
         * Padding Left
         * See https://tailwindcss.com/docs/padding
         */
        new ClassGroup( "pl", "pl", _auto, _spacings ),
        /*
         * Margin
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "m", "m", _auto, _spacings ),
        /*
         * Margin X
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "mx", "mx", _auto, _spacings ),
        /*
         * Margin Y
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "my", "my", _auto, _spacings ),
        /*
         * Margin Start
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "ms", "ms", _auto, _spacings ),
        /*
         * Margin End
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "me", "me", _auto, _spacings ),
        /*
         * Margin Top
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "mt", "mt", _auto, _spacings ),
        /*
         * Margin Right
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "mr", "mr", _auto, _spacings ),
        /*
         * Margin Bottom
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "mb", "mb", _auto, _spacings ),
        /*
         * Margin Left
         * See https://tailwindcss.com/docs/margin
         */
        new ClassGroup( "ml", "ml", _auto, _spacings ),
        /*
         * Space Between X
         * See https://tailwindcss.com/docs/space
         */
        new ClassGroup( "space-x", "space-x", _spacings ),
        /*
         * Space Between X Reverse
         * See https://tailwindcss.com/docs/space
         */
        new ClassGroup( "space-x-reverse", ["space-x-reverse"] ),
        /*
         * Space Between Y
         * See https://tailwindcss.com/docs/space
         */
        new ClassGroup( "space-y", "space-y", _spacings ),
        /*
         * Space Between Y Reverse
         * See https://tailwindcss.com/docs/space
         */
        new ClassGroup( "space-y-reverse", ["space-y-reverse"] ),
        /*
         * Width
         * See https://tailwindcss.com/docs/width
         */
        new ClassGroup( "w", "w", ["auto", "min", "max", "fit", "svw", "lvw", "dvw"], _spacings ),
        /*
         * Min-Width
         * See https://tailwindcss.com/docs/min-width
         */
        new ClassGroup( "min-w", "min-w", ["min", "max", "fit"], _spacings ),
        /*
         * Max-Width
         * See https://tailwindcss.com/docs/max-width
         */
        new ClassGroup( "max-w", "max-w", ["none", "full", "min", "max", "fit", "prose"], [.. _spacings, Validators.IsTshirtSize] ),
        /*
         * Max-Width Screen
         * See https://tailwindcss.com/docs/max-width
         */
        new ClassGroup( "max-w", "max-w-screen", [Validators.IsTshirtSize] ),
        /*
         * Height
         * See https://tailwindcss.com/docs/height
         */
        new ClassGroup( "h", "h", ["auto", "min", "max", "fit", "svh", "lvh", "dvh"], _spacings ),
        /*
         * Min-Height
         * See https://tailwindcss.com/docs/min-height
         */
        new ClassGroup( "min-h", "min-h", ["min", "max", "fit", "svh", "lvh", "dvh"], _spacings ),
        /*
         * Max-Height
         * See https://tailwindcss.com/docs/max-height
         */
        new ClassGroup( "max-h", "max-h", ["min", "max", "fit", "svh", "lvh", "dvh"], _spacings ),
        /*
         * Size
         * See https://tailwindcss.com/docs/size
         */
        new ClassGroup( "size", "size", ["auto", "min", "max", "fit"], _spacings ),
        /*
         * Font Size
         * See https://tailwindcss.com/docs/font-size
         */
        new ClassGroup( "font-size", "text", ["base"], [Validators.IsTshirtSize, Validators.IsArbitraryLength] ),
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
            "bold"],
            [Validators.IsArbitraryNumber] ),
        /*
         * Font Family
         * See https://tailwindcss.com/docs/font-family
         */
        new ClassGroup( "font-family", "font", _any ),
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
            "widest"],
            _arbitraryValue ),
        /*
         * Line Clamp
         * See https://tailwindcss.com/docs/line-clamp
         */
        new ClassGroup( "line-clamp", "line-clamp", _none, _numberAndArbitrary ),
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
            "loose"],
            [Validators.IsLength, Validators.IsArbitraryValue] ),
        /*
         * List Style Image
         * See https://tailwindcss.com/docs/list-style-image
         */
        new ClassGroup( "list-image", "list-image", _none, _arbitraryValue ),
        /*
         * List Style Type
         * See https://tailwindcss.com/docs/list-style-type
         */
        new ClassGroup( "list-style-type", "list", ["none", "disc", "decimal"], _arbitraryValue ),
        /*
         * List Style Position
         * See https://tailwindcss.com/docs/list-style-position
         */
        new ClassGroup( "list-style-position", "list", ["inside", "outside"] ),
        /*
         * Placeholder Color
         * See https://tailwindcss.com/docs/placeholder-color
         */
        new ClassGroup( "placeholder-color", "placeholder", _any ),
        /*
         * Text Alignment
         * See https://tailwindcss.com/docs/text-align
         */
        new ClassGroup( "text-align", "text", ["left", "center", "right", "justify", "start", "end"] ),
        /*
         * Text Color
         * See https://tailwindcss.com/docs/text-color
         */
        new ClassGroup( "text-color", "text", _any ),
        /*
         * Text Decoration
         * See https://tailwindcss.com/docs/text-decoration
         */
        new ClassGroup( "text-decoration", ["underline", "overline", "line-through", "no-underline"] ),
        /*
         * Text Decoration Style
         * See https://tailwindcss.com/docs/text-decoration-style
         */
        new ClassGroup( "text-decoration-style", "decoration", ["wavy", .. _lineStyles] ),
        /*
         * Text Decoration Color
         * See https://tailwindcss.com/docs/text-decoration-color
         */
        new ClassGroup( "text-decoration-color", "decoration", _any ),
        /*
         * Text Decoration Thickness
         * See https://tailwindcss.com/docs/text-decoration-thickness
         */
        new ClassGroup( "text-decoration-thickness", "decoration", ["auto", "from-font"], _lengthAndArbitrary ),
        /*
         * Text Underline Offset
         * See https://tailwindcss.com/docs/text-underline-offset
         */
        new ClassGroup( "underline-offset", "underline-offset", _auto, [Validators.IsLength, Validators.IsArbitraryValue] ),
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
        new ClassGroup( "indent", "indent", _spacings ),
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
            "super"],
            _arbitraryValue ),
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
        new ClassGroup( "content", "content", _none, _arbitraryValue ),
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
        new ClassGroup( "bg-position", "bg", _positions, [Validators.IsArbitraryPosition] ),
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
        new ClassGroup( "bg-size", "bg", ["auto", "cover", "contain"], [Validators.IsArbitrarySize] ),
        /*
         * Background Image
         * See https://tailwindcss.com/docs/background-image
         */
        new ClassGroup( "bg-image", "bg", _none, [Validators.IsArbitraryImage] ),
        /*
         * Background Image Gradient To
         * See https://tailwindcss.com/docs/background-image
         */
        new ClassGroup( "bg-image", "bg-gradient-to", ["t", "tr", "r", "br", "b", "bl", "l", "tl"] ),
        /*
         * Background Color
         * See https://tailwindcss.com/docs/background-color
         */
        new ClassGroup( "bg-color", "bg", _any ),
        /*
         * Gradient Color Stops From Position
         * See https://tailwindcss.com/docs/gradient-color-stops
         */
        new ClassGroup( "gradient-from-pos", "from", _gradientColorStopPositions ),
        /*
         * Gradient Color Stops Via Position
         * See https://tailwindcss.com/docs/gradient-color-stops
         */
        new ClassGroup( "gradient-via-pos", "via", _gradientColorStopPositions ),
        /*
         * Gradient Color Stops To Position
         * See https://tailwindcss.com/docs/gradient-color-stops
         */
        new ClassGroup( "gradient-to-pos", "to", _gradientColorStopPositions ),
        /*
         * Gradient Color Stops From
         * See https://tailwindcss.com/docs/gradient-color-stops
         */
        new ClassGroup( "gradient-from", "from", _any ),
        /*
         * Gradient Color Stops Via
         * See https://tailwindcss.com/docs/gradient-color-stops
         */
        new ClassGroup( "gradient-via", "via", _any ),
        /*
         * Gradient Color Stops To
         * See https://tailwindcss.com/docs/gradient-color-stops
         */
        new ClassGroup( "gradient-to", "to", _any ),
        /*
         * Border Radius
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded", "rounded", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Start
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-s", "rounded-s", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius End
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-e", "rounded-e", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Top
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-t", "rounded-t", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Right
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-r", "rounded-r", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Bottom
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-b", "rounded-b", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Left
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-l", "rounded-l", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Start Start
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-ss", "rounded-ss", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Start End
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-se", "rounded-se", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius End End
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-ee", "rounded-ee", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius End Start
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-es", "rounded-es", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Top Left
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-tl", "rounded-tl", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Top Right
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-tr", "rounded-tr", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Bottom Right
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-br", "rounded-br", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Radius Bottom Left
         * See https://tailwindcss.com/docs/border-radius
         */
        new ClassGroup( "rounded-bl", "rounded-bl", _borderRadius.Items, _borderRadius.Validators ),
        /*
         * Border Width
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w", "border", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width X
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-x", "border-x", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width Y
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-y", "border-y", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width Start
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-s", "border-s", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width End
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-e", "border-e", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width Top
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-t", "border-t", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width Right
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-r", "border-r", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width Bottom
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-b", "border-b", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Width Left
         * See https://tailwindcss.com/docs/border-width
         */
        new ClassGroup( "border-w-l", "border-l", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Border Style
         * See https://tailwindcss.com/docs/border-style
         */
        new ClassGroup( "border-style", "border", ["hidden", .. _lineStyles] ),
        /*
         * Divide Width X
         * See https://tailwindcss.com/docs/divide-width
         */
        new ClassGroup( "divide-x", "divide-x", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Divide Width X Reverse
         * See https://tailwindcss.com/docs/divide-width
         */
        new ClassGroup( "divide-x-reverse", ["divide-x-reverse"] ),
        /*
         * Divide Width Y
         * See https://tailwindcss.com/docs/divide-width
         */
        new ClassGroup( "divide-y", "divide-y", _borderWidth.Items, _borderWidth.Validators ),
        /*
         * Divide Width Y Reverse
         * See https://tailwindcss.com/docs/divide-width
         */
        new ClassGroup( "divide-y-reverse", ["divide-y-reverse"] ),
        /*
         * Divide Style
         * See https://tailwindcss.com/docs/divide-style
         */
        new ClassGroup( "divide-style", "divide", _lineStyles ),
        /*
         * Border Color
         * See https://tailwindcss.com/docs/border-color
         */
        new ClassGroup( "border-color", "border", _any ),
        /*
         * Border Color X
         * See https://tailwindcss.com/docs/border-color
         */
        new ClassGroup( "border-color-x", "border-x", _any ),
        /*
         * Border Color Y
         * See https://tailwindcss.com/docs/border-color
         */
        new ClassGroup( "border-color-y", "border-y", _any ),
        /*
         * Border Color Top
         * See https://tailwindcss.com/docs/border-color
         */
        new ClassGroup( "border-color-t", "border-t", _any ),
        /*
         * Border Color Right
         * See https://tailwindcss.com/docs/border-color
         */
        new ClassGroup( "border-color-r", "border-r", _any ),
        /*
         * Border Color Bottom
         * See https://tailwindcss.com/docs/border-color
         */
        new ClassGroup( "border-color-b", "border-b", _any ),
        /*
         * Border Color Left
         * See https://tailwindcss.com/docs/border-color
         */
        new ClassGroup( "border-color-l", "border-l", _any ),
        /*
         * Divide Color
         * See https://tailwindcss.com/docs/divide-color
         */
        new ClassGroup( "divide-color", "divide", _any ),
        /*
         * Outline Style
         * See https://tailwindcss.com/docs/outline-style
         */
        new ClassGroup( "outline-style", "outline", ["", .. _lineStyles] ),
        /*
         * Outline Offset
         * See https://tailwindcss.com/docs/outline-offset
         */
        new ClassGroup( "outline-offset", "outline-offset", _lengthAndArbitrary ),
        /*
         * Outline Width
         * See https://tailwindcss.com/docs/outline-width
         */
        new ClassGroup( "outline-width", "outline", _lengthAndArbitrary ),
        /*
         * Outline Color
         * See https://tailwindcss.com/docs/outline-color
         */
        new ClassGroup( "outline-color", "outline", _any ),
        /*
         * Ring Width
         * See https://tailwindcss.com/docs/ring-width
         */
        new ClassGroup( "ring-w", "ring", [""], _lengthAndArbitrary ),
        /*
         * Ring Width Inset
         * See https://tailwindcss.com/docs/ring-width
         */
        new ClassGroup( "ring-w-inset", ["ring-inset"] ),
        /*
         * Ring Color
         * See https://tailwindcss.com/docs/ring-color
         */
        new ClassGroup( "ring-color", "ring", _any ),
        /*
         * Ring Offset Width
         * See https://tailwindcss.com/docs/ring-offset-width
         */
        new ClassGroup( "ring-offset-inset", "ring-offset", _lengthAndArbitrary ),
        /*
         * Ring Offset Color
         * See https://tailwindcss.com/docs/ring-offset-color
         */
        new ClassGroup( "ring-offset-color", "ring-offset", _any ),
        /*
         * Shadow
         * See https://tailwindcss.com/docs/shadow
         */
        new ClassGroup( "shadow", "shadow", ["", "inner", "none"], [Validators.IsTshirtSize, Validators.IsArbitraryShadow] ),
        /*
         * Shadow Color
         * See https://tailwindcss.com/docs/shadow-color
         */
        new ClassGroup( "shadow-color", "shadow", _any ),
        /*
         * Opacity
         * See https://tailwindcss.com/docs/opacity
         */
        new ClassGroup( "opacity", "opacity", _numberAndArbitrary ),
        /*
         * Mix Blend Mode
         * See https://tailwindcss.com/docs/mix-blend-mode
         */
        new ClassGroup( "mix-blend", "mix-blend", ["plus-lighter", "plus-darker", .. _blendModes] ),
        /*
         * Background Blend Mode
         * See https://tailwindcss.com/docs/mix-blend-mode
         */
        new ClassGroup( "bg-blend", "bg-blend", _blendModes ),
        /*
         * Blur
         * See https://tailwindcss.com/docs/blur
         */
        new ClassGroup( "blur", "blur", _blur.Items, _blur.Validators ),
        /*
         * Brightness
         * See https://tailwindcss.com/docs/brightness
         */
        new ClassGroup( "brightness", "brightness", _numberAndArbitrary ),
        /*
         * Contrast
         * See https://tailwindcss.com/docs/contrast
         */
        new ClassGroup( "contrast", "contrast", _numberAndArbitrary ),
        /*
         * Drop Shadow
         * See https://tailwindcss.com/docs/drop-shadow
         */
        new ClassGroup( "drop-shadow", "drop-shadow", ["none", ""], _tShirtSizeAndArbitraryValue ),
        /*
         * Grayscale
         * See https://tailwindcss.com/docs/grayscale
         */
        new ClassGroup( "grayscale", "grayscale", _zeroAndEmpty ),
        /*
         * Hue Rotate
         * See https://tailwindcss.com/docs/hue-rotate
         */
        new ClassGroup( "hue-rotate", "hue-rotate", _numberAndArbitraryValue ),
        /*
         * Invert
         * See https://tailwindcss.com/docs/invert
         */
        new ClassGroup( "invert", "invert", _zeroAndEmpty ),
        /*
         * Saturate
         * See https://tailwindcss.com/docs/saturate
         */
        new ClassGroup( "saturate", "saturate", _numberAndArbitrary ),
        /*
         * Sepia
         * See https://tailwindcss.com/docs/sepia
         */
        new ClassGroup( "sepia", "sepia", _zeroAndEmpty ),
        /*
         * Backdrop Blur
         * See https://tailwindcss.com/docs/backdrop-blur
         */
        new ClassGroup( "backdrop-blur", "backdrop-blur", _blur.Items, _blur.Validators ),
        /*
         * Backdrop Brightness
         * See https://tailwindcss.com/docs/backdrop-brightness
         */
        new ClassGroup( "backdrop-brightness", "backdrop-brightness", _numberAndArbitrary ),
        /*
         * Backdrop Contrast
         * See https://tailwindcss.com/docs/backdrop-contrast
         */
        new ClassGroup( "backdrop-contrast", "backdrop-contrast", _numberAndArbitrary ),
        /*
         * Backdrop Grayscale
         * See https://tailwindcss.com/docs/backdrop-grayscale
         */
        new ClassGroup( "backdrop-grayscale", "backdrop-grayscale", _zeroAndEmpty ),
        /*
         * Backdrop Hue Rotate
         * See https://tailwindcss.com/docs/backdrop-hue-rotate
         */
        new ClassGroup( "backdrop-hue-rotate", "backdrop-hue-rotate", _numberAndArbitraryValue ),
        /*
         * Backdrop Invert
         * See https://tailwindcss.com/docs/backdrop-invert
         */
        new ClassGroup( "backdrop-invert", "backdrop-invert", _zeroAndEmpty ),
        /*
         * Backdrop Opacity
         * See https://tailwindcss.com/docs/backdrop-opacity
         */
        new ClassGroup( "backdrop-opacity", "backdrop-opacity", _numberAndArbitrary ),
        /*
         * Backdrop Saturate
         * See https://tailwindcss.com/docs/backdrop-saturate
         */
        new ClassGroup( "backdrop-saturate", "backdrop-saturate", _numberAndArbitrary ),
        /*
         * Backdrop Sepia
         * See https://tailwindcss.com/docs/backdrop-sepia
         */
        new ClassGroup( "backdrop-sepia", "backdrop-sepia", _zeroAndEmpty ),
        /*
         * Border Collapse
         * See https://tailwindcss.com/docs/border-collapse
         */
        new ClassGroup( "border-collapse", "border", ["collapse", "separate"] ),
        /*
         * Border Spacing
         * See https://tailwindcss.com/docs/border-spacing
         */
        new ClassGroup( "border-spacing", "border-spacing", _spacings ),
        /*
         * Border Spacing X
         * See https://tailwindcss.com/docs/border-spacing
         */
        new ClassGroup( "border-spacing-x", "border-spacing-x", _spacings ),
        /*
         * Border Spacing Y
         * See https://tailwindcss.com/docs/border-spacing
         */
        new ClassGroup( "border-spacing-y", "border-spacing-y", _spacings ),
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
        new ClassGroup( "transition", "transition", ["none", "all", "", "colors", "opacity", "shadow", "transform"], _arbitraryValue ),
        /*
         * Transition Duration
         * See https://tailwindcss.com/docs/transition-duration
         */
        new ClassGroup( "duration", "duration", _numberAndArbitraryValue ),
        /*
         * Transition Timing Function
         * See https://tailwindcss.com/docs/transition-timing-function
         */
        new ClassGroup( "ease", "ease", ["linear", "in", "out", "in-out"], _arbitraryValue ),
        /*
         * Transition Delay
         * See https://tailwindcss.com/docs/transition-delay
         */
        new ClassGroup( "delay", "delay", _numberAndArbitraryValue ),
        /*
         * Animation
         * See https://tailwindcss.com/docs/animation
         */
        new ClassGroup( "animate", "animate", ["none", "spin", "ping", "pulse", "bounce"], _arbitraryValue ),
        /*
         * Transform
         * See https://tailwindcss.com/docs/transform
         */
        new ClassGroup( "transform", "transform", ["", "gpu", "none"] ),
        /*
         * Scale
         * See https://tailwindcss.com/docs/scale
         */
        new ClassGroup( "scale", "scale", _numberAndArbitraryValue ),
        /*
         * Scale X
         * See https://tailwindcss.com/docs/scale
         */
        new ClassGroup( "scale-x", "scale-x", _numberAndArbitraryValue ),
        /*
         * Scale Y
         * See https://tailwindcss.com/docs/scale
         */
        new ClassGroup( "scale-y", "scale-y", _numberAndArbitraryValue ),
        /*
         * Rotate
         * See https://tailwindcss.com/docs/rotate
         */
        new ClassGroup( "rotate", "rotate", _integerAndArbitraryValue ),
        /*
         * Translate X
         * See https://tailwindcss.com/docs/translate
         */
        new ClassGroup( "translate-x", "translate-x", _numberAndArbitraryValue ),
        /*
         * Translate Y
         * See https://tailwindcss.com/docs/translate
         */
        new ClassGroup( "translate-y", "translate-y", _numberAndArbitraryValue ),
        /*
         * Skew X
         * See https://tailwindcss.com/docs/skew
         */
        new ClassGroup( "skew-x", "skew-x", _numberAndArbitraryValue ),
        /*
         * Skew Y
         * See https://tailwindcss.com/docs/skew
         */
        new ClassGroup( "skew-y", "skew-y", _numberAndArbitraryValue ),
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
            "top-left"],
            _arbitraryValue ),
        /*
         * Accent
         * See https://tailwindcss.com/docs/accent
         */
        new ClassGroup( "accent", "accent", _auto, _any ),
        /*
         * Appearance
         * See https://tailwindcss.com/docs/appearance
         */
        new ClassGroup( "appearance", "appearance", _autoAndNone ),
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
            "zoom-out"],
            _arbitraryValue ),
        /*
         * Caret Color
         * See https://tailwindcss.com/docs/caret-color
         */
        new ClassGroup( "caret-color", "caret", _any ),
        /*
         * Pointer Events
         * See https://tailwindcss.com/docs/pointer-events
         */
        new ClassGroup( "pointer-events", "pointer-events", _autoAndNone ),
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
        new ClassGroup( "scroll-m", "scroll-m", _spacings ),
        /*
         * Scroll Margin X
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-mx", "scroll-mx", _spacings ),
        /*
         * Scroll Margin Y
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-my", "scroll-my", _spacings ),
        /*
         * Scroll Margin Start
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-ms", "scroll-ms", _spacings ),
        /*
         * Scroll Margin End
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-me", "scroll-me", _spacings ),
        /*
         * Scroll Margin Top
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-mt", "scroll-mt", _spacings ),
        /*
         * Scroll Margin Right
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-mr", "scroll-mr", _spacings ),
        /*
         * Scroll Margin Bottom
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-mb", "scroll-mb", _spacings ),
        /*
         * Scroll Margin Left
         * See https://tailwindcss.com/docs/scroll-margin
         */
        new ClassGroup( "scroll-ml", "scroll-ml", _spacings ),
        /*
         * Scroll Padding
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-p", "scroll-p", _spacings ),
        /*
         * Scroll Padding X
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-px", "scroll-px", _spacings ),
        /*
         * Scroll Padding Y
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-py", "scroll-py", _spacings ),
        /*
         * Scroll Padding Start
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-ps", "scroll-ps", _spacings ),
        /*
         * Scroll Padding End
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-pe", "scroll-pe", _spacings ),
        /*
         * Scroll Padding Top
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-pt", "scroll-pt", _spacings ),
        /*
         * Scroll Padding Right
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-pr", "scroll-pr", _spacings ),
        /*
         * Scroll Padding Bottom
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-pb", "scroll-pb", _spacings ),
        /*
         * Scroll Padding Left
         * See https://tailwindcss.com/docs/scroll-padding
         */
        new ClassGroup( "scroll-pl", "scroll-pl", _spacings ),
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
        new ClassGroup( "will-change", "will-change", ["auto", "scroll", "contents", "transform"], _arbitraryValue ),
        /*
         * Fill
         * See https://tailwindcss.com/docs/fill
         */
        new ClassGroup( "fill", "fill", _none, _any ),
        /*
         * Stroke Width
         * See https://tailwindcss.com/docs/stroke-width
         */
        new ClassGroup( "stroke-w", "stroke", [Validators.IsLength, Validators.IsArbitraryLength, Validators.IsArbitraryNumber] ),
        /*
         * Stroke
         * See https://tailwindcss.com/docs/stroke
         */
        new ClassGroup( "stroke", "stroke", _none, _any ),
        /*
         * Screen Readers
         * See https://tailwindcss.com/docs/screen-readers
         */
        new ClassGroup( "sr", ["sr-only", "not-sr-only"] ),
        /*
         * Forced Color Adjust
         * See https://tailwindcss.com/docs/forced-color-adjust
         */
        new ClassGroup( "forced-color-adjust", "forced-color-adjust", _autoAndNone ),
    ];

    private static readonly Dictionary<string, string[]> _conflictingClassGroups = new()
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
        ["touch"] =["touch-x", "touch-y", "touch-pz"],
        ["touch-x"] = ["touch"],
        ["touch-y"] = ["touch"],
        ["touch-pz"] = ["touch"]
    };

    private static readonly Dictionary<string, string[]> _conflictingClassGroupModifiers = new()
    {
        ["font-size"] = ["leading"]
    };

    internal string Separator { get; }
    internal ClassGroup[] ClassGroups { get; }
    internal IReadOnlyDictionary<string, string[]> ConflictingClassGroups { get; }
    internal IReadOnlyDictionary<string, string[]> ConflictingClassGroupModifiers { get; }

    internal TwMergeConfig()
    {
        Separator = ":";
        ClassGroups = _classGroups;
        ConflictingClassGroups = _conflictingClassGroups;
        ConflictingClassGroupModifiers = _conflictingClassGroupModifiers;
    }

    internal static TwMergeConfig Default()
    {
        return new();
    }
}
