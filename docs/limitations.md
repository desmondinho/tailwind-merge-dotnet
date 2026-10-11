# Limitations

## Don't use classes that look like Tailwind classes but apply different styles

tailwind-merge applies some heuristics to detect the type of a class even if that particular class does not exist in the default Tailwind config. E.g. the class `text-1000xl` does not exist in Tailwind CSS by default but is treated like a `font-size` class in tailwind-merge because it starts with `text-` followed by an optional number and a T-shirt size, like all the other `font-size` classes.

This behavior has the advantage that you're less likely to need to configure tailwind-merge if you're only changing or extending some scales in your Tailwind config. But it also means that tailwind-merge treats classes that look like Tailwind classes as Tailwind classes although they might not be defined in your Tailwind config.

## You need to use label in arbitrary `font-family` classes

Both `font-weight` and `font-family` use the `font-*` prefix in Tailwind CSS. When using arbitrary values or variables without labels, tailwind-merge-dotnet can't know whether the class sets the font weight or font family, so it treats them as `font-weight` classes. This matches Tailwind CSS, which also defaults to font weight for ambiguous `font-*` classes.

Therefore it is necessary to always prepend arbitrary values of `font-family` classes with the label `family-name:` as in `font-[family-name:var(--my-font)]` or `font-(family-name:--my-font)`. `font-weight` classes don't need a label, but you can use `weight:` or `number:` for clarity.

```csharp
// Unlabeled arbitrary values and variables are treated as font-weight
TwMerge.Merge( "font-(--my-family) font-(--my-weight)" ); // → "font-(--my-weight)"

// Use the `family-name:` label for font-family
TwMerge.Merge( "font-(family-name:--my-family) font-(--my-weight)" );
// → "font-(family-name:--my-family) font-(--my-weight)"
```

## You need to use label in arbitrary `background-position` and `background-size` classes

tailwind-merge detects the type of class by parsing the class name. When using a class like `bg-[30%_30%]`, tailwind-merge can't know whether the class is a `background-position` or `background-size` class.

Therefore it is necessary to always prepend arbitrary values of `background-position` classes with the label `position:` as in `bg-[position:30%_30%]`. The same applies to `background-size` classes which need to be prepended with `length:`, `size:` or `percentage:`.

---

[Back to overview](./README.md)
