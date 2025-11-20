using BatchCore.SDK.Clients;
using BatchCore.SDK.Configuration;
using BatchCore.SDK.Exceptions;
using BatchCore.SDK.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace BatchCore.SDK.Tests;

public class BatchCoreClientTests
{
    [Fact]
    public void Constructor_WithValidOptions_CreatesInstance()
    {
        // Arrange
        var options = new BatchCoreOptions
        {
            ApiEndpoint = "https://api.example.com",
            ApiKey = "test-key"
        };

        // Act
        var client = new BatchCoreClient(options);

        // Assert
        Assert.NotNull(client);
    }

    [Fact]
    public void Constructor_WithNullOptions_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BatchCoreClient(null!));
    }

    [Fact]
    public void Constructor_WithInvalidTimeout_ThrowsConfigurationException()
    {
        // Arrange
        var options = new BatchCoreOptions
        {
            TimeoutSeconds = 0
        };

        // Act & Assert
        Assert.Throws<BatchCoreConfigurationException>(() => new BatchCoreClient(options));
    }

    [Fact]
    public void Constructor_WithNegativeRetryAttempts_ThrowsConfigurationException()
    {
        // Arrange
        var options = new BatchCoreOptions
        {
            MaxRetryAttempts = -1
        };

        // Act & Assert
        Assert.Throws<BatchCoreConfigurationException>(() => new BatchCoreClient(options));
    }

    [Fact]
    public void Constructor_WithInvalidBatchSize_ThrowsConfigurationException()
    {
        // Arrange
        var options = new BatchCoreOptions
        {
            BatchSize = 0
        };

        // Act & Assert
        Assert.Throws<BatchCoreConfigurationException>(() => new BatchCoreClient(options));
    }

    [Fact]
    public async Task SubmitBatchAsync_WithValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options, NullLogger<BatchCoreClient>.Instance);
        var request = new BatchRequest
        {
            Id = "test-batch-1",
            Name = "Test Batch",
            Items = new List<string> { "item1", "item2", "item3" }
        };

        // Act
        var response = await client.SubmitBatchAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.Id, response.Id);
        Assert.Equal(BatchStatus.Completed, response.Status);
        Assert.Equal(3, response.SuccessCount);
        Assert.Equal(0, response.FailureCount);
    }

    [Fact]
    public async Task SubmitBatchAsync_WithNullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.SubmitBatchAsync(null!));
    }

    [Fact]
    public async Task GetBatchStatusAsync_WithValidBatchId_ReturnsStatus()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options);
        var batchId = "test-batch-1";

        // Act
        var response = await client.GetBatchStatusAsync(batchId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(batchId, response.Id);
        Assert.Equal(BatchStatus.Completed, response.Status);
    }

    [Fact]
    public async Task GetBatchStatusAsync_WithNullBatchId_ThrowsArgumentException()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => client.GetBatchStatusAsync(null!));
    }

    [Fact]
    public async Task GetBatchStatusAsync_WithEmptyBatchId_ThrowsArgumentException()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => client.GetBatchStatusAsync(string.Empty));
    }

    [Fact]
    public async Task CancelBatchAsync_WithValidBatchId_ReturnsTrue()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options);
        var batchId = "test-batch-1";

        // Act
        var result = await client.CancelBatchAsync(batchId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CancelBatchAsync_WithNullBatchId_ThrowsArgumentException()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => client.CancelBatchAsync(null!));
    }

    [Fact]
    public async Task CancelBatchAsync_WithEmptyBatchId_ThrowsArgumentException()
    {
        // Arrange
        var options = new BatchCoreOptions();
        var client = new BatchCoreClient(options);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => client.CancelBatchAsync(string.Empty));
    }
}
