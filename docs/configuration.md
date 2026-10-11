# Configuration

## Installation

The TailwindMerge.NET package is hosted on NuGet under the name [`TailwindMerge.NET`](https://www.nuget.org/packages/TailwindMerge.NET). Here are installation instructions:

```sh
dotnet add package TailwindMerge.NET
```

```csharp
// Program.cs

using TailwindMerge.Extensions;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddTailwindMerge();
```

## Basic usage

If you're using Tailwind CSS without any extra config, you can use [`Merge`](./api-reference.md#methods) right away.

```razor
@* Page.razor *@

@inject TwMerge TwMerge

@TwMerge.Merge("px-2 py-1 bg-red hover:bg-dark-red", "p-3 bg-[#B91C1C]")
@* → "hover:bg-dark-red p-3 bg-[#B91C1C]" *@
```

You can safely stop reading the documentation here.

## Usage with custom Tailwind config

If you're using a custom Tailwind config, you may need to configure tailwind-merge-dotnet as well to merge classes properly.

The default configuration still works if all the following points apply to your Tailwind config:

- Only using color names which don't clash with other Tailwind class names
- Only deviating by number values from number-based Tailwind classes
- Only using font-family classes which don't clash with default font-weight classes
- Sticking to default Tailwind config for everything else

Otherwise, extend or override the default configuration when registering the service:

```csharp
// Program.cs

using TailwindMerge.Common;
using TailwindMerge.Extensions;
using TailwindMerge.Models;

builder.Services.AddTailwindMerge( options =>
{
    options.Extend( new ExtendedConfig
    {
        Theme = new()
        {
            [ThemeKeys.Text] = ["huge"]
        }
    } );
} );
```

Without dependency injection, pass the configuration to the constructor:

```csharp
var config = new TwMergeConfig();
config.Extend( new ExtendedConfig { /* ... */ } );

var twMerge = new TwMerge( Options.Create( config ) );
```

`TwMerge` builds its lookup structures from the configuration once, when it's created, so changes made to the configuration afterwards have no effect.

### Extending and overriding

- `Extend` adds values to the existing ones. The values of theme scales and class groups are appended, and keys which don't exist yet are added.
- `Override` replaces the values of the given keys completely. Keys which aren't mentioned keep their default values.

If you use both, call `Override` before `Extend`.

### Theme

In your Tailwind config you can modify the theme variable namespaces to add classes with custom values. tailwind-merge-dotnet follows the same naming scheme as Tailwind CSS for its theme scales, and the `ThemeKeys` class provides a constant for each of them:

| Tailwind CSS namespace | Theme key | Constant |
| --- | --- | --- |
| `--color-*` | `color` | `ThemeKeys.Color` |
| `--font-*` | `font` | `ThemeKeys.Font` |
| `--text-*` | `text` | `ThemeKeys.Text` |
| `--font-weight-*` | `font-weight` | `ThemeKeys.FontWeight` |
| `--tracking-*` | `tracking` | `ThemeKeys.Tracking` |
| `--leading-*` | `leading` | `ThemeKeys.Leading` |
| `--breakpoint-*` | `breakpoint` | `ThemeKeys.Breakpoint` |
| `--container-*` | `container` | `ThemeKeys.Container` |
| `--spacing-*` | `spacing` | `ThemeKeys.Spacing` |
| `--radius-*` | `radius` | `ThemeKeys.Radius` |
| `--shadow-*` | `shadow` | `ThemeKeys.Shadow` |
| `--inset-shadow-*` | `inset-shadow` | `ThemeKeys.InsetShadow` |
| `--text-shadow-*` | `text-shadow` | `ThemeKeys.TextShadow` |
| `--drop-shadow-*` | `drop-shadow` | `ThemeKeys.DropShadow` |
| `--blur-*` | `blur` | `ThemeKeys.Blur` |
| `--perspective-*` | `perspective` | `ThemeKeys.Perspective` |
| `--aspect-*` | `aspect` | `ThemeKeys.Aspect` |
| `--ease-*` | `ease` | `ThemeKeys.Ease` |
| `--animate-*` | `animate` | `ThemeKeys.Animate` |

If you modified one of these namespaces in your Tailwind config, add the variable names to the theme as well, so that tailwind-merge-dotnet knows about them. E.g. after adding `--text-huge: 100px` to your Tailwind config:

```csharp
options.Extend( new ExtendedConfig
{
    Theme = new()
    {
        // ↓ `text` is the key of the `--text-*` namespace
        //                  ↓ `huge` is the variable name without the namespace prefix
        [ThemeKeys.Text] = ["huge"]
    }
} );

TwMerge.Merge( "text-lg text-huge" ); // → "text-huge"
TwMerge.Merge( "text-huge text-sm" ); // → "text-sm"
```

A theme scale is applied to all class groups which use it, e.g. extending `ThemeKeys.Spacing` adds the new values to padding, margin, gap, inset and all other spacing classes at once.

### Class groups

Classes which modify the same CSS property form a class group, and tailwind-merge-dotnet only keeps the last class of each group. A `ClassGroup` consists of a base class name and its definitions, which can be:

- strings for fixed class parts, where an empty string stands for the base class name itself
- validators from `Validators` (or any `Func<string, bool>`) for class parts that can't be listed, e.g. arbitrary values
- theme scales referenced with `ThemeReference`, e.g. `new ThemeReference( ThemeKeys.Spacing )`
- nested `ClassGroup` instances for class parts that share a prefix

E.g. a class group for custom `elevation-*` utilities:

```csharp
options.Extend( new ExtendedConfig
{
    ClassGroups = new()
    {
        // ↓ Resolves to elevation, elevation-low, elevation-high and elevation-[...]
        ["elevation"] = new ClassGroup( "elevation", ["", "low", "high", Validators.IsArbitraryValue] )
    }
} );

TwMerge.Merge( "elevation-low elevation-high" ); // → "elevation-high"
```

The key (`"elevation"`) is the ID of the class group. It's only used internally and has to be unique among all class groups. To add values to a default class group, extend it under its ID (e.g. `"shadow"`); the IDs are listed in the [default configuration](../src/TwMergeConfig.cs).

If several of your class groups share the same values, you can put them into a custom theme scale and reference it with `new ThemeReference( "elevation" )` in each class group.

---

[Back to overview](./README.md)
