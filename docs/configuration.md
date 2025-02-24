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

TODO

---

[Back to overview](./README.md)
