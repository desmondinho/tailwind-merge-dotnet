# What is it for

## Video introduction

[Watch this introduction video from Simon Vrachliotis (@simonswiss) ↓ 
![The "why" behind tailwind-merge](https://img.youtube.com/vi/tfgLd5ZSNPc/maxresdefault.jpg)](https://www.youtube.com/watch?v=tfgLd5ZSNPc (Watch YouTube video "Tailwind-Merge Is Incredibly Useful — And Here's Why!"))

## Thanks, but I prefer to read

If you use Tailwind CSS with a component-based UI renderer like [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor), 
you're probably familiar with the situation that you want to change some styles of a component, but only in a one-off case.

```razor
@* MyGenericInput.razor *@

<input class="@_cssClass" />

@code {
    [Parameter] public string? CssClass { get; set; }

    private string? _cssClass;

    protected override void OnParametersSet()
    {
        _cssClass = $"border rounded px-2 py-1 {CssClass}";
    }
}
```

```razor
@* MyOneOffInput.razor *@

<MyGenericInput CssClass="p-3" /> @* ← Only want to change some padding *@
```

When `MyOneOffInput` is rendered, an input with the class `border rounded px-2 py-1 p-3` gets created. 
But because of the way the [CSS cascade](https://developer.mozilla.org/en-US/docs/Web/CSS/Cascade) works, the styles of the `p-3` class are ignored. 
The order of the classes in the `_cssClass` string doesn't matter at all and the only way to apply the `p-3` styles is to remove both `px-2` and `py-1`.

This is where `TwMerge` comes in.

```razor
@* MyGenericInput.razor *@

<input class="@_cssClass" />

@code {
    [Parameter] public string? CssClass { get; set; }

    [Inject] private TwMerge TwMerge { get; set; } = default!;

    private string? _cssClass;

    protected override void OnParametersSet()
    {
        // ↓ Now `CssClass` can override conflicting classes
        _cssClass = TwMerge.Merge( "border rounded px-2 py-1", CssClass );
    }
}
```

`TwMerge` overrides conflicting classes and keeps everything else untouched. 
In the case of the `MyOneOffInput`, the input is now rendered with the classes `border rounded p-3`.

---

[Back to overview](./README.md)
