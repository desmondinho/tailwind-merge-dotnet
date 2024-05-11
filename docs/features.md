# Features

## Merging behavior

tailwind-merge-dotnet is built to be intuitive. It follows a set of rules to determine which class wins when there are conflicts. Here is a brief overview of its conflict resolution.

### Last conflicting class wins

```csharp
TwMerge.Merge("p-5 p-2 p-4"); // → "p-4"
```

### Allows refinements

```csharp
TwMerge.Merge("p-3 px-5"); // → "p-3 px-5"
TwMerge.Merge("inset-x-4 right-4"); // → "inset-x-4 right-4"
```

### Resolves non-trivial conflicts

```csharp
TwMerge.Merge("inset-x-px -inset-1"); // → "-inset-1"
TwMerge.Merge("bottom-auto inset-y-6"); // → "inset-y-6"
TwMerge.Merge("inline block"); // → "block"
```

### Supports modifiers and stacked modifiers

```csharp
TwMerge.Merge( "p-2 hover:p-4" ); // → "p-2 hover:p-4"
TwMerge.Merge( "hover:p-2 hover:p-4" ); // → "hover:p-4"
TwMerge.Merge( "hover:focus:p-2 focus:hover:p-4" ); // → "focus:hover:p-4"
```

The order of standard modifiers does not matter for tailwind-merge-dotnet.

### Supports arbitrary values

```csharp
TwMerge.Merge( "bg-black bg-[color:var(--mystery-var)]" ); // → "bg-[color:var(--mystery-var)]"
TwMerge.Merge( "grid-cols-[1fr,auto] grid-cols-2" ); // → "grid-cols-2"
```

### Supports arbitrary properties

```csharp
TwMerge.Merge( "[mask-type:luminance] [mask-type:alpha]" ); // → "[mask-type:alpha]"
TwMerge.Merge( "[--scroll-offset:56px] lg:[--scroll-offset:44px]" );
// → "[--scroll-offset:56px] lg:[--scroll-offset:44px]"

// Don"t do this!
TwMerge.Merge( "[padding:1rem] p-8" ); // → "[padding:1rem] p-8"
```

> **Warning**
> tailwind-merge-dotnet does not resolve conflicts between arbitrary properties and their matching Tailwind classes.

### Supports arbitrary variants

```csharp
TwMerge.Merge( "[&:nth-child(3)]:py-0 [&:nth-child(3)]:py-4" ); // → "[&:nth-child(3)]:py-4"
TwMerge.Merge( "dark:hover:[&:nth-child(3)]:py-0 hover:dark:[&:nth-child(3)]:py-4" );
// → "hover:dark:[&:nth-child(3)]:py-4"

// Don"t do this!
TwMerge.Merge( "[&:focus]:ring focus:ring-4" ); // → "[&:focus]:ring focus:ring-4"
```

> **Warning**
> tailwind-merge-dotnet does not resolve conflicts between arbitrary variants and their matching predefined modifiers.

The order of standard modifiers before and after an arbitrary variant in isolation (all modifiers before are one group, all modifiers after are another group) does not matter for tailwind-merge-dotnet. 
However, it does matter whether a standard modifier is before or after an arbitrary variant both for Tailwind CSS and tailwind-merge-dotnet because the resulting CSS selectors are different.

### Supports important modifier

```csharp
TwMerge.Merge( "!p-3 !p-4 p-5" ); // → "!p-4 p-5"
TwMerge.Merge( "!right-2 !-inset-x-1" ); // → "!-inset-x-1"
```

### Supports postfix modifiers

```csharp
TwMerge.Merge( "text-sm leading-6 text-lg/7" ); // → "text-lg/7"
```

### Preserves non-Tailwind classes

```csharp
TwMerge.Merge( "p-5 p-2 my-non-tailwind-class p-4" ); // → "my-non-tailwind-class p-4"
```

### Supports custom colors out of the box

```csharp
TwMerge.Merge( "text-red text-secret-sauce" ); // → "text-secret-sauce"
```

## Composition

tailwind-merge-dotnet has some features that simplify composing class strings together.

### Supports multiple arguments

```csharp
TwMerge.Merge( "some-class", "another-class yet-another-class", "so-many-classes" );
// → "some-class another-class yet-another-class so-many-classes"
```

## Performance

### Results are cached

Results get cached by default, so you don't need to worry about wasteful re-renders. 
The library uses a computationally lightweight [LRU cache](https://en.wikipedia.org/wiki/Cache_replacement_policies#Least_recently_used_(LRU)) which stores up to 500 different results by default. 
The cache is applied after all arguments are joined together to a single string. 
This means that if you call `Merge` repeatedly with different arguments that result in the same string when joined, the cache will be hit.
