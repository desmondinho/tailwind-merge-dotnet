using Microsoft.Extensions.DependencyInjection;

namespace TailwindMerge.Extensions;

/// <summary>
/// Provides extension methods to configure TailwindMerge services on a <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
	/// Registers a singleton instance of the <see cref="TwMerge"/> service.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/>.</param>
	public static void AddTailwindMerge( this IServiceCollection services )
    {
        services.AddSingleton<TwMerge>();
    }

    /// <summary>
    /// Registers a singleton instance of the <see cref="TwMerge"/> service with the given configuration options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="options">The delegate to configure the <see cref="TwMergeConfig"/> options.</param>
    public static void AddTailwindMerge( this IServiceCollection services, Action<TwMergeConfig> options )
    {
        services.AddSingleton<TwMerge>();
        services.Configure( options );
    }
}
