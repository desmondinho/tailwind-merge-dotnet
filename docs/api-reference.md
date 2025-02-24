# API reference

Reference to all public members of the TailwindMerge.NET.

## `TwMerge`

```csharp
/// <summary>
/// A utility for merging conflicting Tailwind CSS classes.
/// </summary>
public partial class TwMerge
```

### Constructors

```csharp
/// <summary>
/// Initializes a new instance of <see cref="TwMerge" /> with optional configuration options.
/// </summary>
/// <param name="options">The configuration options.</param>
public TwMerge( IOptions<TwMergeConfig>? options = null )
```

### Methods 

```csharp
/// <summary>
/// Merges the given <paramref name="classNames"/>, resolving any conflicts if present.
/// </summary>
/// <param name="classNames">The collection of CSS classes to be merged.</param>
/// <returns>A <see langword="string"/> of merged CSS classes.</returns>
public string? Merge( params string?[] classNames )
```

## `TwMergeConfig`

```csharp
/// <summary>
/// Represents the configuration settings for the <see cref="TwMerge"/>.
/// </summary>
public class TwMergeConfig
```

### Constructors

```csharp
/// <summary>
/// Initializes a new instance of the <see cref="TwMergeConfig"/> class.
/// </summary>
public TwMergeConfig()
```

### Properties 

```csharp
/// <summary>
/// Gets or sets the maximum size of the LRU cache used for memoizing results.
/// </summary>
/// <remarks>
/// The default is 500
/// </remarks>
public int CacheSize { get; set; }

/// <summary>
/// Gets or sets the value that allows to add a custom prefix to 
/// all of Tailwind CSS generated utility classes.
/// </summary>
public string? Prefix { get; set; }

/// <summary>
/// Gets or sets the theme scales of the configuration.
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
```

### Methods 

```csharp
/// <summary>
/// Creates a new instance of the <see cref="TwMergeConfig"/> class with default settings.
/// </summary>
/// <returns>A <see cref="TwMergeConfig"/> instance.</returns>
public static TwMergeConfig Default()

/// <summary>
/// Extends the current configuration with the values from the provided <see cref="ExtendedConfig"/>.
/// </summary>
/// <param name="extendedConfig">The extended configuration.</param>
public void Extend( ExtendedConfig extendedConfig )

/// <summary>
/// Overrides the current configuration with the values from the provided <see cref="ExtendedConfig"/>.
/// </summary>
/// <param name="extendedConfig">The extended configuration.</param>
public void Override( ExtendedConfig extendedConfig )
```

## `ExtendedConfig`

```csharp
/// <summary>
/// Represents the extended configuration for the TailwindMerge utility.
/// </summary>
public class ExtendedConfig
```

### Properties

```csharp
/// <summary>
/// Gets or sets the theme for the configuration extension.
/// </summary>
public Dictionary<string, object[]>? Theme { get; set; }

/// <summary>
/// Gets or sets the class groups for the configuration extension.
/// </summary>
public Dictionary<string, ClassGroup>? ClassGroups { get; set; }

/// <summary>
/// Gets or sets the conflicting class groups for the configuration extension.
/// </summary>
public Dictionary<string, string[]>? ConflictingClassGroups { get; set; }

/// <summary>
/// Gets or sets the conflicting class group modifiers for the configuration extension.
/// </summary>
public Dictionary<string, string[]>? ConflictingClassGroupModifiers { get; set; }
```

## `ThemeGetter`

```csharp
/// <summary>
/// Represents a method that retrieves an array of objects from a theme based on a specified key.
/// </summary>
/// <param name="theme">The theme to retrieve values from.</param>
/// <returns>An array of objects associated with the specified key in the theme.</returns>
public delegate object[] ThemeGetter( Dictionary<string, object[]> theme );
```

## `ThemeUtility`

```csharp
/// <summary>
/// Provides utility method for working with theme configurations.
/// </summary>
public static class ThemeUtility
```

### Methods

```csharp
/// <summary>
/// Creates a <see cref="ThemeGetter"/> delegate that retrieves values from a theme based on the specified key.
/// </summary>
/// <param name="key">The key to use for retrieving values from the theme.</param>
/// <returns>A <see cref="ThemeGetter"/>.</returns>
public static ThemeGetter FromTheme( string key )
```

## Validators

```csharp
/// <summary>
/// Provides a set of validation functions for different class name parts.
/// </summary>
public static partial class Validators
```

### Properties

```csharp
/// <summary>
/// Always returns true, indicating that any value is valid.
/// </summary>
public static Func<string, bool> IsAny { get; }

/// <summary>
/// Validates whether a string represents a fraction.
/// </summary>
public static Func<string, bool> IsFraction { get; }

/// <summary>
/// Validates whether a string represents a number.
/// </summary>
public static Func<string, bool> IsNumber { get; }

/// <summary>
/// Validates whether a string represents an integer.
/// </summary>
public static Func<string, bool> IsInteger { get; }

/// <summary>
/// Validates whether a string represents a percentage.
/// </summary>
public static Func<string, bool> IsPercent { get; }

/// <summary>
/// Validates whether a string represents a t-shirt size value.
/// </summary>
public static Func<string, bool> IsTshirtSize { get; }

/// <summary>
/// Validates whether a string represents an arbitrary value.
/// </summary>
public static Func<string, bool> IsArbitraryValue { get; }

/// <summary>
/// Validates whether a string represents an arbitrary variable.
/// </summary>
public static Func<string, bool> IsArbitraryVariable { get; }

/// <summary>
/// Validates whether a string represents an arbitrary variable length value.
/// </summary>
public static Func<string, bool> IsArbitraryVariableLength { get; }

/// <summary>
/// Validates whether a string represents an arbitrary variable family name value.
/// </summary>
public static Func<string, bool> IsArbitraryVariableFamilyName { get; }

/// <summary>
/// Validates whether a string represents an arbitrary variable position value.
/// </summary>
public static Func<string, bool> IsArbitraryVariablePosition { get; }

/// <summary>
/// Validates whether a string represents an arbitrary variable size value.
/// </summary>
public static Func<string, bool> IsArbitraryVariableSize { get; }

/// <summary>
/// Validates whether a string represents an arbitrary variable image value.
/// </summary>
public static Func<string, bool> IsArbitraryVariableImage { get; }

/// <summary>
/// Validates whether a string represents an arbitrary variable shadow value.
/// </summary>
public static Func<string, bool> IsArbitraryVariableShadow { get; }

/// <summary>
/// Validates whether a string represents an arbitrary size.
/// </summary>
public static Func<string, bool> IsArbitrarySize { get; }

/// <summary>
/// Validates whether a string represents an arbitrary length value.
/// </summary>
public static Func<string, bool> IsArbitraryLength { get; }

/// <summary>
/// Validates whether a string represents an arbitrary number.
/// </summary>
public static Func<string, bool> IsArbitraryNumber { get; }

/// <summary>
/// Validates whether a string represents an arbitrary position.
/// </summary>
public static Func<string, bool> IsArbitraryPosition { get; }

/// <summary>
/// Validates whether a string represents an arbitrary image value.
/// </summary>
public static Func<string, bool> IsArbitraryImage { get; }

/// <summary>
/// Validates whether a string represents an arbitrary shadow value.
/// </summary>
public static Func<string, bool> IsArbitraryShadow { get; }

/// <summary>
/// Validates whether a string is not an arbitrary value or variable.
/// </summary>
public static Func<string, bool> IsAnyNonArbitrary { get; }
```

A brief summary for each validator:

- `IsAny` always returns true. Be careful with this validator as it might match unwanted classes. I use it primarily to match colors or when I'm certain there are no other class groups in a namespace.
- `IsAnyNonArbitrary` checks if the class part is not an arbitrary value or arbitrary variable.
- `IsArbitraryImage` checks whether class part is an arbitrary value which is an iamge, e.g. by starting with `image:`, `url:`, `linear-gradient(` or `url(` (`[url('/path-to-image.png')]`, `image:var(--maybe-an-image-at-runtime)]`) which is necessary for background-image classNames.
- `IsArbitraryLength` checks for arbitrary length values (`[3%]`, `[4px]`, `[length:var(--my-var)]`).
- `IsArbitraryNumber` checks whether class part is an arbitrary value which starts with `number:` or is a number (`[number:var(--value)]`, `[450]`) which is necessary for font-weight and stroke-width classNames.
- `IsArbitraryPosition` checks whether class part is an arbitrary value which starts with `position:` (`[position:200px_100px]`) which is necessary for background-position classNames.
- `IsArbitraryShadow` checks whether class part is an arbitrary value which starts with the same pattern as a shadow value (`[0_35px_60px_-15px_rgba(0,0,0,0.3)]`), namely with two lengths separated by a underscore, optionally prepended by `inset`.
- `IsArbitrarySize` checks whether class part is an arbitrary value which starts with `size:` (`[size:200px_100px]`) which is necessary for background-size classNames.
- `IsArbitraryValue` checks whether the class part is enclosed in brackets (`[something]`)
- `IsArbitraryVariable` checks whether the class part is an arbitrary variable (`(--my-var)`)
- `IsArbitraryVariableFamilyName` checks whether class part is an arbitrary variable with the `family-name` label (`(family-name:--my-font)`)
- `IsArbitraryVariableImage` checks whether class part is an arbitrary variable with the `image` or `url` label (`(image:--my-image)`)
- `IsArbitraryVariableLength` checks whether class part is an arbitrary variable with the `length` label (`(length:--my-length)`)
- `IsArbitraryVariablePosition` checks whether class part is an arbitrary variable with the `position` label (`(position:--my-position)`)
- `IsArbitraryVariableShadow` checks whether class part is an arbitrary variable with the `shadow` label or not label at all (`(shadow:--my-shadow)`, `(--my-shadow)`)
- `IsArbitraryVariableSize` checks whether class part is an arbitrary variable with the `size`, `length` or `percentage` label (`(size:--my-size)`)
- `IsFraction` checks whether class part is a fraction of two numbers (`1/2`, `127/256`)
- `IsInteger` checks for integer values (`3`).
- `IsNumber` checks for numbers (`3`, `1.5`)
- `IsPercent` checks for percent values (`12.5%`) which is used for color stop positions.
- `IsTshirtSize`checks whether class part is a T-shirt size (`sm`, `xl`), optionally with a preceding number (`2xl`).

## `ClassGroup`

```csharp
/// <summary>
/// Represents a group of CSS classes with associated metadata for the TailwindMerge utility.
/// </summary>
public readonly struct ClassGroup
```

### Constructors

```csharp
/// <summary>
/// Initializes a new instance of the <see cref="ClassGroup"/> struct
/// with the specified <paramref name="baseClassName"/> and <paramref name="definitions"/>.
/// </summary>
/// <param name="baseClassName">The base class name for the group.</param>
/// <param name="definitions">An array of definitions associated with the class group.</param>
public readonly struct ClassGroup( string? baseClassName, object[] definitions )

/// <summary>
/// Initializes a new instance of the <see cref="ClassGroup"/> struct with the specified <paramref name="definitions"/>.
/// </summary>
/// <param name="definitions">An array of definitions associated with the class group.</param>
public ClassGroup( object[] definitions ) : this( null, definitions )
```

### Properties

```csharp
/// <summary>
/// Gets the base class name for the group.
/// </summary>
public string? BaseClassName { get; }

/// <summary>
/// Gets an array of definitions associated with the class group.
/// </summary>
public object[] Definitions { get; }
```

---

[Back to overview](./README.md)
