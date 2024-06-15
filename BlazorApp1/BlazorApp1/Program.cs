using BlazorApp1.Client.Pages;
using BlazorApp1.Components;

using TailwindMerge.Extensions;

using static TailwindMerge.TwMergeConfig;

internal class Program
{
    private static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddTailwindMerge( options =>
        {
            options.CacheSize = 100;
            options.Separator = "_";
            options.Prefix = "tw-";

            options.Theme["colors"].Add( "sosi" );
        } );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if( app.Environment.IsDevelopment() )
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler( "/Error", createScopeForErrors: true );
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies( typeof( BlazorApp1.Client._Imports ).Assembly );

        app.Run();
    }
}