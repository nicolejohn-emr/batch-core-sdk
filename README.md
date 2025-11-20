# BatchCore SDK

[![NuGet](https://img.shields.io/nuget/v/BatchCore.SDK.svg)](https://www.nuget.org/packages/BatchCore.SDK/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A comprehensive C# SDK for batch processing operations with built-in support for various batch processing patterns and best practices.

## Features

- 🚀 **Easy to Use**: Simple API for batch operations
- ⚙️ **Configurable**: Flexible configuration options
- 🔌 **Dependency Injection**: First-class support for DI
- 📝 **Well Documented**: Comprehensive XML documentation
- 🧪 **Well Tested**: High test coverage with unit tests
- 🔒 **Type Safe**: Strongly typed models and responses
- 📦 **NuGet Ready**: Easy deployment as NuGet package

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package BatchCore.SDK
```

Or via Package Manager Console:

```powershell
Install-Package BatchCore.SDK
```

## Quick Start

### Basic Usage

```csharp
using BatchCore.SDK.Clients;
using BatchCore.SDK.Configuration;
using BatchCore.SDK.Models;

// Configure the SDK
var options = new BatchCoreOptions
{
    ApiEndpoint = "https://api.example.com",
    ApiKey = "your-api-key",
    TimeoutSeconds = 60,
    BatchSize = 100
};

// Create the client
var client = new BatchCoreClient(options);

// Submit a batch request
var request = new BatchRequest
{
    Id = "batch-001",
    Name = "Sample Batch",
    Items = new List<string> { "item1", "item2", "item3" }
};

var response = await client.SubmitBatchAsync(request);
Console.WriteLine($"Batch Status: {response.Status}");
```

### Dependency Injection

```csharp
using BatchCore.SDK.Extensions;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Add BatchCore SDK with configuration
services.AddBatchCore(options =>
{
    options.ApiEndpoint = "https://api.example.com";
    options.ApiKey = "your-api-key";
    options.TimeoutSeconds = 60;
    options.EnableDetailedLogging = true;
});

var serviceProvider = services.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<IBatchCoreClient>();
```

## Configuration Options

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| `ApiEndpoint` | string | null | The API endpoint URL |
| `ApiKey` | string | null | API key for authentication |
| `TimeoutSeconds` | int | 30 | Timeout for API requests |
| `MaxRetryAttempts` | int | 3 | Maximum retry attempts |
| `EnableDetailedLogging` | bool | false | Enable detailed logging |
| `BatchSize` | int | 100 | Batch size for operations |

## API Reference

### IBatchCoreClient

The main interface for interacting with the SDK.

#### Methods

- `Task<BatchResponse> SubmitBatchAsync(BatchRequest request, CancellationToken cancellationToken = default)`
  - Submits a batch request for processing
  
- `Task<BatchResponse> GetBatchStatusAsync(string batchId, CancellationToken cancellationToken = default)`
  - Gets the status of a batch operation
  
- `Task<bool> CancelBatchAsync(string batchId, CancellationToken cancellationToken = default)`
  - Cancels a batch operation

### Models

#### BatchRequest

```csharp
public class BatchRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string> Items { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

#### BatchResponse

```csharp
public class BatchResponse
{
    public string? Id { get; set; }
    public BatchStatus Status { get; set; }
    public string? Message { get; set; }
    public int SuccessCount { get; set; }
    public int FailureCount { get; set; }
    public int TotalCount { get; set; }
    public long ProcessingDurationMs { get; set; }
    public DateTime CompletedAt { get; set; }
    public List<string> Errors { get; set; }
}
```

#### BatchStatus

```csharp
public enum BatchStatus
{
    Pending,
    Processing,
    Completed,
    PartiallyCompleted,
    Failed,
    Cancelled
}
```

## Examples

### Check Batch Status

```csharp
var status = await client.GetBatchStatusAsync("batch-001");
Console.WriteLine($"Status: {status.Status}");
Console.WriteLine($"Success Count: {status.SuccessCount}");
Console.WriteLine($"Total Count: {status.TotalCount}");
```

### Cancel a Batch

```csharp
var cancelled = await client.CancelBatchAsync("batch-001");
if (cancelled)
{
    Console.WriteLine("Batch cancelled successfully");
}
```

## Building from Source

```bash
# Clone the repository
git clone https://github.com/nicolejohn-emr/batch-core-sdk.git
cd batch-core-sdk

# Build the solution
dotnet build

# Run tests
dotnet test

# Create NuGet package
dotnet pack -c Release
```

## Project Structure

```
batch-core-sdk/
├── src/
│   └── BatchCore.SDK/          # Main SDK library
│       ├── Clients/            # Client implementations
│       ├── Configuration/      # Configuration classes
│       ├── Exceptions/         # Custom exceptions
│       ├── Extensions/         # Extension methods
│       ├── Interfaces/         # Interface definitions
│       └── Models/             # Data models
├── tests/
│   └── BatchCore.SDK.Tests/    # Unit tests
├── samples/
│   └── BatchCore.SDK.Sample/   # Sample application
└── docs/                       # Documentation
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a list of changes.

## Support

For questions and support, please open an issue on the [GitHub repository](https://github.com/nicolejohn-emr/batch-core-sdk/issues).
