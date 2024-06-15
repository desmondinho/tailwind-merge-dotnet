using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using TailwindMerge.Extensions;

using static TailwindMerge.TwMergeConfig;

internal class Program
{
    private static async global::System.Threading.Tasks.Task Main( string[] args )
    {
        var builder = WebAssemblyHostBuilder.CreateDefault( args );

        builder.Services.AddTailwindMerge( options =>
        {
            options.CacheSize = 100;
            options.Separator = "_";
            options.Prefix = "tw-";

            options.Theme["colors"].Add( "sosi" );

        } );

        await builder.Build().RunAsync();
    }
}