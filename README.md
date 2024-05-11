<div align="center">
    <br />
    <a href="https://github.com/desmondinho/tailwind-merge-dotnet">
        <img src="https://raw.githubusercontent.com/desmondinho/tailwind-merge-dotnet/HEAD/assets/logo.svg" alt="tailwind-merge-dotnet" height="150px" />
    </a>
</div>

# tailwind-merge-dotnet

A utility to efficiently merge [Tailwind CSS](https://tailwindcss.com) classes in C# without style conflicts.

-   Supports Tailwind v3.4
-   Thread-safe LRU caching

## Prerequisites

|                          .NET SDK                          |      Support       |
| :--------------------------------------------------------: | :----------------: |
| [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) | :heavy_check_mark: |

## Usage

Register the service in the DI container:

```csharp
// Program.cs

using TailwindMerge.Extensions;

var builder = WebApplication.CreateBuilder( args );

// Add TailwindMerge to the container
builder.Services.AddTailwindMerge();
```

---

Inject the service into component using one of the following approaches:

```razor
@* Page.razor *@

@inject TwMerge TwMerge

@TwMerge.Merge("px-2 py-1 bg-red hover:bg-dark-red", "p-3 bg-[#B91C1C]")
@* → "hover:bg-dark-red p-3 bg-[#B91C1C]" *@
```

```csharp
// Page.razor.cs

[Inject] private TwMerge TwMerge {get; set; } = default!;

TwMerge.Merge("px-2 py-1 bg-red hover:bg-dark-red", "p-3 bg-[#B91C1C]")
// → "hover:bg-dark-red p-3 bg-[#B91C1C]"
```

## Contributing

Contributions are always welcome!

See [CONTRIBUTING.md](CONTRIBUTING.md) for ways to get started. Please adhere to this project's [CODE_OF_CONDUCT](CODE_OF_CONDUCT.md).

## Acknowledgements 🙏

This project is a C# adaptation of [tailwind-merge](https://github.com/dcastil/tailwind-merge) originally developed by [dcastil](https://github.com/dcastil).
