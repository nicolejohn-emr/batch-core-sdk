using BatchCore.SDK.Configuration;
using BatchCore.SDK.Exceptions;
using BatchCore.SDK.Interfaces;
using BatchCore.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BatchCore.SDK.Clients;

/// <summary>
/// Main client for interacting with the BatchCore SDK.
/// </summary>
public class BatchCoreClient : IBatchCoreClient
{
    private readonly BatchCoreOptions _options;
    private readonly ILogger<BatchCoreClient> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchCoreClient"/> class.
    /// </summary>
    /// <param name="options">Configuration options for the client.</param>
    /// <param name="logger">Logger instance for logging operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when options is null.</exception>
    /// <exception cref="BatchCoreConfigurationException">Thrown when required configuration is missing.</exception>
    public BatchCoreClient(BatchCoreOptions options, ILogger<BatchCoreClient>? logger = null)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLogger<BatchCoreClient>.Instance;

        ValidateOptions();
        
        _logger.LogInformation("BatchCoreClient initialized successfully");
    }

    /// <summary>
    /// Submits a batch request for processing.
    /// </summary>
    /// <param name="request">The batch request to submit.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The batch response containing processing results.</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    public async Task<BatchResponse> SubmitBatchAsync(BatchRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        _logger.LogInformation("Submitting batch request: {BatchId}", request.Id);

        try
        {
            // Simulate batch processing
            await Task.Delay(100, cancellationToken);

            var response = new BatchResponse
            {
                Id = request.Id ?? Guid.NewGuid().ToString(),
                Status = BatchStatus.Completed,
                Message = "Batch processed successfully",
                SuccessCount = request.Items.Count,
                FailureCount = 0,
                TotalCount = request.Items.Count,
                ProcessingDurationMs = 100,
                CompletedAt = DateTime.UtcNow
            };

            _logger.LogInformation("Batch submitted successfully: {BatchId}", response.Id);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting batch: {BatchId}", request.Id);
            throw new BatchCoreException("Failed to submit batch request", ex);
        }
    }

    /// <summary>
    /// Gets the status of a batch operation.
    /// </summary>
    /// <param name="batchId">The unique identifier of the batch.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The batch response containing current status.</returns>
    /// <exception cref="ArgumentException">Thrown when batchId is null or empty.</exception>
    public async Task<BatchResponse> GetBatchStatusAsync(string batchId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(batchId))
        {
            throw new ArgumentException("Batch ID cannot be null or empty", nameof(batchId));
        }

        _logger.LogInformation("Getting status for batch: {BatchId}", batchId);

        try
        {
            // Simulate status retrieval
            await Task.Delay(50, cancellationToken);

            var response = new BatchResponse
            {
                Id = batchId,
                Status = BatchStatus.Completed,
                Message = "Batch is completed",
                CompletedAt = DateTime.UtcNow
            };

            _logger.LogInformation("Status retrieved for batch: {BatchId}", batchId);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting batch status: {BatchId}", batchId);
            throw new BatchCoreException("Failed to get batch status", ex);
        }
    }

    /// <summary>
    /// Cancels a batch operation.
    /// </summary>
    /// <param name="batchId">The unique identifier of the batch to cancel.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>True if the batch was cancelled successfully; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown when batchId is null or empty.</exception>
    public async Task<bool> CancelBatchAsync(string batchId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(batchId))
        {
            throw new ArgumentException("Batch ID cannot be null or empty", nameof(batchId));
        }

        _logger.LogInformation("Cancelling batch: {BatchId}", batchId);

        try
        {
            // Simulate cancellation
            await Task.Delay(50, cancellationToken);

            _logger.LogInformation("Batch cancelled successfully: {BatchId}", batchId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling batch: {BatchId}", batchId);
            throw new BatchCoreException("Failed to cancel batch", ex);
        }
    }

    private void ValidateOptions()
    {
        if (_options.TimeoutSeconds <= 0)
        {
            throw new BatchCoreConfigurationException("TimeoutSeconds must be greater than 0");
        }

        if (_options.MaxRetryAttempts < 0)
        {
            throw new BatchCoreConfigurationException("MaxRetryAttempts cannot be negative");
        }

        if (_options.BatchSize <= 0)
        {
            throw new BatchCoreConfigurationException("BatchSize must be greater than 0");
        }
    }
}
