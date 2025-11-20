using BatchCore.SDK.Models;

namespace BatchCore.SDK.Interfaces;

/// <summary>
/// Interface for the BatchCore SDK client.
/// </summary>
public interface IBatchCoreClient
{
    /// <summary>
    /// Submits a batch request for processing.
    /// </summary>
    /// <param name="request">The batch request to submit.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The batch response containing processing results.</returns>
    Task<BatchResponse> SubmitBatchAsync(BatchRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the status of a batch operation.
    /// </summary>
    /// <param name="batchId">The unique identifier of the batch.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The batch response containing current status.</returns>
    Task<BatchResponse> GetBatchStatusAsync(string batchId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels a batch operation.
    /// </summary>
    /// <param name="batchId">The unique identifier of the batch to cancel.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>True if the batch was cancelled successfully; otherwise, false.</returns>
    Task<bool> CancelBatchAsync(string batchId, CancellationToken cancellationToken = default);
}
