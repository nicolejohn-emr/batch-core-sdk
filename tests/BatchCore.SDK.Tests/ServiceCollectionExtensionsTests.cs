using BatchCore.SDK.Configuration;
using BatchCore.SDK.Extensions;
using BatchCore.SDK.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BatchCore.SDK.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddBatchCore_WithConfiguration_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddBatchCore(options =>
        {
            options.ApiEndpoint = "https://api.example.com";
            options.ApiKey = "test-key";
            options.TimeoutSeconds = 60;
        });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetService<IBatchCoreClient>();
        var options = serviceProvider.GetService<BatchCoreOptions>();

        Assert.NotNull(client);
        Assert.NotNull(options);
        Assert.Equal("https://api.example.com", options.ApiEndpoint);
        Assert.Equal("test-key", options.ApiKey);
        Assert.Equal(60, options.TimeoutSeconds);
    }

    [Fact]
    public void AddBatchCore_WithoutConfiguration_RegistersServicesWithDefaults()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddBatchCore();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetService<IBatchCoreClient>();
        var options = serviceProvider.GetService<BatchCoreOptions>();

        Assert.NotNull(client);
        Assert.NotNull(options);
        Assert.Equal(30, options.TimeoutSeconds);
        Assert.Equal(3, options.MaxRetryAttempts);
        Assert.Equal(100, options.BatchSize);
    }

    [Fact]
    public void AddBatchCore_WithNullServices_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            ((IServiceCollection)null!).AddBatchCore(options => { }));
    }

    [Fact]
    public void AddBatchCore_WithNullConfiguration_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddBatchCore(null!));
    }
}
