using BatchCore.SDK.Clients;
using BatchCore.SDK.Configuration;
using BatchCore.SDK.Extensions;
using BatchCore.SDK.Interfaces;
using BatchCore.SDK.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Example 1: Basic usage without dependency injection
Console.WriteLine("=== Example 1: Basic Usage ===");
await BasicUsageExample();

Console.WriteLine("\n=== Example 2: Dependency Injection ===");
await DependencyInjectionExample();

Console.WriteLine("\n=== Example 3: Batch Operations ===");
await BatchOperationsExample();

Console.WriteLine("\nAll examples completed successfully!");

static async Task BasicUsageExample()
{
    var options = new BatchCoreOptions
    {
        ApiEndpoint = "https://api.example.com",
        ApiKey = "your-api-key",
        TimeoutSeconds = 60,
        BatchSize = 50
    };

    var client = new BatchCoreClient(options);

    var request = new BatchRequest
    {
        Id = "batch-001",
        Name = "Sample Batch",
        Description = "Processing sample data",
        Items = new List<string> { "item1", "item2", "item3" }
    };

    var response = await client.SubmitBatchAsync(request);
    Console.WriteLine($"Batch ID: {response.Id}");
    Console.WriteLine($"Status: {response.Status}");
    Console.WriteLine($"Success Count: {response.SuccessCount}");
    Console.WriteLine($"Total Count: {response.TotalCount}");
}

static async Task DependencyInjectionExample()
{
    var services = new ServiceCollection();

    // Configure logging
    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Information);
    });

    // Add BatchCore SDK
    services.AddBatchCore(options =>
    {
        options.ApiEndpoint = "https://api.example.com";
        options.ApiKey = "your-api-key";
        options.TimeoutSeconds = 60;
        options.EnableDetailedLogging = true;
    });

    var serviceProvider = services.BuildServiceProvider();
    var client = serviceProvider.GetRequiredService<IBatchCoreClient>();

    var request = new BatchRequest
    {
        Id = "batch-002",
        Name = "DI Example Batch",
        Items = new List<string> { "task1", "task2", "task3", "task4" }
    };

    var response = await client.SubmitBatchAsync(request);
    Console.WriteLine($"Batch processed: {response.Id} - Status: {response.Status}");
}

static async Task BatchOperationsExample()
{
    var options = new BatchCoreOptions
    {
        BatchSize = 10,
        MaxRetryAttempts = 5
    };

    var client = new BatchCoreClient(options);

    // Submit a batch
    var request = new BatchRequest
    {
        Id = "batch-003",
        Name = "Operations Example",
        Items = Enumerable.Range(1, 20).Select(i => $"item-{i}").ToList()
    };

    Console.WriteLine("Submitting batch...");
    var submitResponse = await client.SubmitBatchAsync(request);
    Console.WriteLine($"Submitted: {submitResponse.Id}");

    // Check status
    Console.WriteLine("Checking batch status...");
    var statusResponse = await client.GetBatchStatusAsync(submitResponse.Id!);
    Console.WriteLine($"Status: {statusResponse.Status}");

    // Cancel batch (for demonstration)
    Console.WriteLine("Cancelling batch...");
    var cancelled = await client.CancelBatchAsync(submitResponse.Id!);
    Console.WriteLine($"Cancelled: {cancelled}");
}
