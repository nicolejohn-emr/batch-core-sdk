using BatchCore.SDK.Clients;
using BatchCore.SDK.Configuration;
using BatchCore.SDK.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BatchCore.SDK.Extensions;

/// <summary>
/// Extension methods for configuring BatchCore SDK services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds BatchCore SDK services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Configuration action for BatchCore options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddBatchCore(
        this IServiceCollection services,
        Action<BatchCoreOptions> configure)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var options = new BatchCoreOptions();
        configure(options);

        services.AddSingleton(options);
        services.AddSingleton<IBatchCoreClient, BatchCoreClient>();

        return services;
    }

    /// <summary>
    /// Adds BatchCore SDK services to the dependency injection container with default options.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddBatchCore(this IServiceCollection services)
    {
        return AddBatchCore(services, _ => { });
    }
}
