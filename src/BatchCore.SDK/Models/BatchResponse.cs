namespace BatchCore.SDK.Models;

/// <summary>
/// Represents a batch processing response.
/// </summary>
public class BatchResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the batch response.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the status of the batch processing.
    /// </summary>
    public BatchStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the message associated with the batch response.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the number of items processed successfully.
    /// </summary>
    public int SuccessCount { get; set; }

    /// <summary>
    /// Gets or sets the number of items that failed processing.
    /// </summary>
    public int FailureCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of items in the batch.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the processing duration in milliseconds.
    /// </summary>
    public long ProcessingDurationMs { get; set; }

    /// <summary>
    /// Gets or sets the completed timestamp.
    /// </summary>
    public DateTime CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets any errors that occurred during processing.
    /// </summary>
    public List<string> Errors { get; set; } = new();
}

/// <summary>
/// Represents the status of a batch operation.
/// </summary>
public enum BatchStatus
{
    /// <summary>
    /// The batch is pending processing.
    /// </summary>
    Pending,

    /// <summary>
    /// The batch is currently being processed.
    /// </summary>
    Processing,

    /// <summary>
    /// The batch has completed successfully.
    /// </summary>
    Completed,

    /// <summary>
    /// The batch has completed with some failures.
    /// </summary>
    PartiallyCompleted,

    /// <summary>
    /// The batch processing has failed.
    /// </summary>
    Failed,

    /// <summary>
    /// The batch processing was cancelled.
    /// </summary>
    Cancelled
}
