using Microsoft.AspNetCore.Components;

using TailwindMerge;

namespace BlazorApp1.Client.Pages;
public partial class Counter
{
    [Inject] private TwMerge TwMerge { get; set; } = default!;

    private int currentCount = 0;

    private void IncrementCount()
    {
        var asd = TwMerge.Merge( "bg-red-500", "bg-blue-300" );
        currentCount++;
    }
}