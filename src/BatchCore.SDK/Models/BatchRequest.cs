namespace BatchCore.SDK.Models;

/// <summary>
/// Represents a batch processing request.
/// </summary>
public class BatchRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the batch request.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the batch request.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the batch request.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the items to be processed in the batch.
    /// </summary>
    public List<string> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the metadata associated with the batch request.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new();

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
