namespace BatchCore.SDK.Configuration;

/// <summary>
/// Configuration options for the BatchCore SDK.
/// </summary>
public class BatchCoreOptions
{
    /// <summary>
    /// Gets or sets the API endpoint URL.
    /// </summary>
    public string? ApiEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the API key for authentication.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the timeout in seconds for API requests.
    /// Default is 30 seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// Default is 3.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Gets or sets whether to enable detailed logging.
    /// Default is false.
    /// </summary>
    public bool EnableDetailedLogging { get; set; } = false;

    /// <summary>
    /// Gets or sets the batch size for processing operations.
    /// Default is 100.
    /// </summary>
    public int BatchSize { get; set; } = 100;
}
