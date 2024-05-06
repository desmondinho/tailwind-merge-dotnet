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
}
