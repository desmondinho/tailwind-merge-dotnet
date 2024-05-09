using TailwindMerge.Models;

using Xunit;

namespace TailwindMerge.Tests;

public class ClassMapFactoryTests
{
    [Fact]
    public void Create_DefaultConfig_HasCorrectClassGroupsAtFirstNode()
    {
        // Arrange
        var config = TwMergeConfig.Default();
        var classMap = TwMergeMapFactory.Create( config );

        // Act
        var classGroupsByFirstNode = new SortedDictionary<string, List<string>>(
            classMap.Next!.ToDictionary(
                entry => entry.Key,
                entry => GetClassGroups( entry.Value ).OrderBy( x => x ).ToList()
            )
        );

        var expected = new Dictionary<string, List<string>>()
        {
            ["absolute"] = ["position"],
            ["accent"] = ["accent"],
            ["align"] = ["vertical-align"],
            ["animate"] = ["animate"],
            ["antialiased"] = ["font-smoothing"],
            ["appearance"] = ["appearance"],
            ["aspect"] = ["aspect"],
            ["auto"] = ["auto-cols", "auto-rows"],
            ["backdrop"] = [
                "backdrop-blur",
                "backdrop-brightness",
                "backdrop-contrast",
                "backdrop-grayscale",
                "backdrop-hue-rotate",
                "backdrop-invert",
                "backdrop-opacity",
                "backdrop-saturate",
                "backdrop-sepia",
            ],
            ["basis"] = ["basis"],
            ["bg"] = [
                "bg-attachment",
                "bg-blend",
                "bg-clip",
                "bg-color",
                "bg-image",
                "bg-origin",
                "bg-position",
                "bg-repeat",
                "bg-size",
            ],
            ["block"] = ["display"],
            ["blur"] = ["blur"],
            ["border"] = [
                "border-collapse",
                "border-color",
                "border-color-b",
                "border-color-l",
                "border-color-r",
                "border-color-t",
                "border-color-x",
                "border-color-y",
                "border-spacing",
                "border-spacing-x",
                "border-spacing-y",
                "border-style",
                "border-w",
                "border-w-b",
                "border-w-e",
                "border-w-l",
                "border-w-r",
                "border-w-s",
                "border-w-t",
                "border-w-x",
                "border-w-y",
            ],
            ["bottom"] = ["bottom"],
            ["box"] = ["box", "box-decoration"],
            ["break"] = ["break", "break-after", "break-before", "break-inside"],
            ["brightness"] = ["brightness"],
            ["capitalize"] = ["text-transform"],
            ["caption"] = ["caption"],
            ["caret"] = ["caret-color"],
            ["clear"] = ["clear"],
            ["col"] = ["col-end", "col-start", "col-start-end"],
            ["collapse"] = ["visibility"],
            ["container"] = ["container"],
            ["columns"] = ["columns"],
            ["content"] = ["align-content", "content"],
            ["contents"] = ["display"],
            ["contrast"] = ["contrast"],
            ["cursor"] = ["cursor"],
            ["decoration"] = ["text-decoration-color", "text-decoration-style", "text-decoration-thickness"],
            ["delay"] = ["delay"],
            ["diagonal"] = ["fvn-fraction"],
            ["divide"] = [
                "divide-color",
                "divide-style",
                "divide-x",
                "divide-x-reverse",
                "divide-y",
                "divide-y-reverse",
            ],
            ["drop"] = ["drop-shadow"],
            ["duration"] = ["duration"],
            ["ease"] = ["ease"],
            ["end"] = ["end"],
            ["fill"] = ["fill"],
            ["fixed"] = ["position"],
            ["flex"] = ["display", "flex", "flex-direction", "flex-wrap"],
            ["float"] = ["float"],
            ["flow"] = ["display"],
            ["font"] = ["font-family", "font-weight"],
            ["forced"] = ["forced-color-adjust"],
            ["from"] = ["gradient-from", "gradient-from-pos"],
            ["gap"] = ["gap", "gap-x", "gap-y"],
            ["grayscale"] = ["grayscale"],
            ["grid"] = ["display", "grid-cols", "grid-flow", "grid-rows"],
            ["grow"] = ["grow"],
            ["h"] = ["h"],
            ["hidden"] = ["display"],
            ["hue"] = ["hue-rotate"],
            ["hyphens"] = ["hyphens"],
            ["indent"] = ["indent"],
            ["inline"] = ["display"],
            ["inset"] = ["inset", "inset-x", "inset-y"],
            ["invert"] = ["invert"],
            ["invisible"] = ["visibility"],
            ["isolate"] = ["isolation"],
            ["isolation"] = ["isolation"],
            ["italic"] = ["font-style"],
            ["items"] = ["align-items"],
            ["justify"] = ["justify-content", "justify-items", "justify-self"],
            ["leading"] = ["leading"],
            ["left"] = ["left"],
            ["line"] = ["line-clamp", "text-decoration"],
            ["lining"] = ["fvn-figure"],
            ["list"] = ["display", "list-image", "list-style-position", "list-style-type"],
            ["lowercase"] = ["text-transform"],
            ["m"] = ["m"],
            ["max"] = ["max-h", "max-w"],
            ["mb"] = ["mb"],
            ["me"] = ["me"],
            ["min"] = ["min-h", "min-w"],
            ["mix"] = ["mix-blend"],
            ["ml"] = ["ml"],
            ["mr"] = ["mr"],
            ["ms"] = ["ms"],
            ["mt"] = ["mt"],
            ["mx"] = ["mx"],
            ["my"] = ["my"],
            ["no"] = ["text-decoration"],
            ["normal"] = ["fvn-normal", "text-transform"],
            ["not"] = ["font-style", "sr"],
            ["object"] = ["object-fit", "object-position"],
            ["oldstyle"] = ["fvn-figure"],
            ["opacity"] = ["opacity"],
            ["order"] = ["order"],
            ["ordinal"] = ["fvn-ordinal"],
            ["origin"] = ["transform-origin"],
            ["outline"] = ["outline-color", "outline-offset", "outline-style", "outline-w"],
            ["overflow"] = ["overflow", "overflow-x", "overflow-y"],
            ["overline"] = ["text-decoration"],
            ["overscroll"] = ["overscroll", "overscroll-x", "overscroll-y"],
            ["p"] = ["p"],
            ["pb"] = ["pb"],
            ["pe"] = ["pe"],
            ["pl"] = ["pl"],
            ["place"] = ["place-content", "place-items", "place-self"],
            ["placeholder"] = ["placeholder-color"],
            ["pointer"] = ["pointer-events"],
            ["pr"] = ["pr"],
            ["proportional"] = ["fvn-spacing"],
            ["ps"] = ["ps"],
            ["pt"] = ["pt"],
            ["px"] = ["px"],
            ["py"] = ["py"],
            ["relative"] = ["position"],
            ["resize"] = ["resize"],
            ["right"] = ["right"],
            ["ring"] = [
                "ring-color",
                "ring-offset-color",
                "ring-offset-w",
                "ring-w",
                "ring-w-inset",
            ],
            ["rotate"] = ["rotate"],
            ["rounded"] = [
                "rounded",
                "rounded-b",
                "rounded-bl",
                "rounded-br",
                "rounded-e",
                "rounded-ee",
                "rounded-es",
                "rounded-l",
                "rounded-r",
                "rounded-s",
                "rounded-se",
                "rounded-ss",
                "rounded-t",
                "rounded-tl",
                "rounded-tr",
            ],
            ["row"] = ["row-end", "row-start", "row-start-end"],
            ["saturate"] = ["saturate"],
            ["scale"] = ["scale", "scale-x", "scale-y"],
            ["scroll"] = [
                "scroll-behavior",
                "scroll-m",
                "scroll-mb",
                "scroll-me",
                "scroll-ml",
                "scroll-mr",
                "scroll-ms",
                "scroll-mt",
                "scroll-mx",
                "scroll-my",
                "scroll-p",
                "scroll-pb",
                "scroll-pe",
                "scroll-pl",
                "scroll-pr",
                "scroll-ps",
                "scroll-pt",
                "scroll-px",
                "scroll-py",
            ],
            ["select"] = ["select"],
            ["self"] = ["align-self"],
            ["sepia"] = ["sepia"],
            ["shadow"] = ["shadow", "shadow-color"],
            ["shrink"] = ["shrink"],
            ["size"] = ["size"],
            ["skew"] = ["skew-x", "skew-y"],
            ["slashed"] = ["fvn-slashed-zero"],
            ["snap"] = ["snap-align", "snap-stop", "snap-strictness", "snap-type"],
            ["space"] = ["space-x", "space-x-reverse", "space-y", "space-y-reverse"],
            ["sr"] = ["sr"],
            ["stacked"] = ["fvn-fraction"],
            ["start"] = ["start"],
            ["static"] = ["position"],
            ["sticky"] = ["position"],
            ["stroke"] = ["stroke", "stroke-w"],
            ["subpixel"] = ["font-smoothing"],
            ["table"] = ["display", "table-layout"],
            ["tabular"] = ["fvn-spacing"],
            ["text"] = [
                "font-size",
                "text-alignment",
                "text-color",
                "text-overflow",
                "text-wrap",
            ],
            ["to"] = ["gradient-to", "gradient-to-pos"],
            ["top"] = ["top"],
            ["touch"] = ["touch", "touch-pz", "touch-x", "touch-y"],
            ["tracking"] = ["tracking"],
            ["transform"] = ["transform"],
            ["transition"] = ["transition"],
            ["translate"] = ["translate-x", "translate-y"],
            ["truncate"] = ["text-overflow"],
            ["underline"] = ["text-decoration", "underline-offset"],
            ["uppercase"] = ["text-transform"],
            ["via"] = ["gradient-via", "gradient-via-pos"],
            ["visible"] = ["visibility"],
            ["w"] = ["w"],
            ["whitespace"] = ["whitespace"],
            ["will"] = ["will-change"],
            ["z"] = ["z"]
        };

        // Assert
        Assert.Null( classMap.ClassGroupId );
        Assert.Null( classMap.Validators );
        Assert.Equal( expected, classGroupsByFirstNode );
    }

    private HashSet<string> GetClassGroups( ClassNameNode node )
    {
        var classGroups = new HashSet<string>();
        AddClassGroups( node, classGroups );

        return classGroups;
    }

    private void AddClassGroups( ClassNameNode node, HashSet<string> classGroups )
    {
        // Add class group ID if not null or empty
        if( !string.IsNullOrEmpty( node.ClassGroupId ) )
        {
            classGroups.Add( node.ClassGroupId );
        }

        // Add class group IDs from validators
        if( node.Validators != null )
        {
            foreach( var validator in node.Validators )
            {
                classGroups.Add( validator.ClassGroupId );
            }
        }

        // Recursively add class group IDs from next nodes
        if( node.Next != null )
        {
            foreach( var value in node.Next.Values )
            {
                AddClassGroups( value, classGroups );
            }
        }
    }
}