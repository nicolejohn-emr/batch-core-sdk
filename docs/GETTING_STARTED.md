# Getting Started with BatchCore SDK

This guide will help you get started with the BatchCore SDK for .NET applications.

## Prerequisites

- .NET 8.0 SDK or later
- A C# IDE (Visual Studio, Visual Studio Code, or Rider)

## Installation

### Via .NET CLI

```bash
dotnet add package BatchCore.SDK
```

### Via NuGet Package Manager

```powershell
Install-Package BatchCore.SDK
```

### Via Visual Studio

1. Right-click on your project in Solution Explorer
2. Select "Manage NuGet Packages"
3. Search for "BatchCore.SDK"
4. Click "Install"

## Basic Setup

### 1. Without Dependency Injection

```csharp
using BatchCore.SDK.Clients;
using BatchCore.SDK.Configuration;
using BatchCore.SDK.Models;

// Create configuration
var options = new BatchCoreOptions
{
    ApiEndpoint = "https://api.example.com",
    ApiKey = "your-api-key",
    TimeoutSeconds = 60
};

// Create client
var client = new BatchCoreClient(options);
```

### 2. With Dependency Injection (Recommended)

#### In Program.cs (ASP.NET Core or Console)

```csharp
using BatchCore.SDK.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddBatchCore(options =>
    {
        options.ApiEndpoint = "https://api.example.com";
        options.ApiKey = "your-api-key";
        options.TimeoutSeconds = 60;
        options.EnableDetailedLogging = true;
    });
});

var host = builder.Build();

// Get the client from DI
var client = host.Services.GetRequiredService<IBatchCoreClient>();
```

#### In Startup.cs (ASP.NET Core Web API)

```csharp
using BatchCore.SDK.Extensions;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddBatchCore(options =>
        {
            options.ApiEndpoint = Configuration["BatchCore:ApiEndpoint"];
            options.ApiKey = Configuration["BatchCore:ApiKey"];
        });
        
        services.AddControllers();
    }
}
```

## Configuration

### From appsettings.json

```json
{
  "BatchCore": {
    "ApiEndpoint": "https://api.example.com",
    "ApiKey": "your-api-key",
    "TimeoutSeconds": 60,
    "MaxRetryAttempts": 3,
    "BatchSize": 100,
    "EnableDetailedLogging": true
  }
}
```

Then in code:

```csharp
services.AddBatchCore(options =>
{
    Configuration.GetSection("BatchCore").Bind(options);
});
```

## Usage Examples

### Submit a Batch

```csharp
var request = new BatchRequest
{
    Id = "batch-001",
    Name = "My First Batch",
    Description = "Processing sample data",
    Items = new List<string> { "item1", "item2", "item3" }
};

var response = await client.SubmitBatchAsync(request);

Console.WriteLine($"Status: {response.Status}");
Console.WriteLine($"Success: {response.SuccessCount}/{response.TotalCount}");
```

### Check Batch Status

```csharp
var status = await client.GetBatchStatusAsync("batch-001");

switch (status.Status)
{
    case BatchStatus.Pending:
        Console.WriteLine("Batch is pending...");
        break;
    case BatchStatus.Processing:
        Console.WriteLine("Batch is being processed...");
        break;
    case BatchStatus.Completed:
        Console.WriteLine("Batch completed successfully!");
        break;
    case BatchStatus.Failed:
        Console.WriteLine($"Batch failed: {status.Message}");
        break;
}
```

### Cancel a Batch

```csharp
var cancelled = await client.CancelBatchAsync("batch-001");
if (cancelled)
{
    Console.WriteLine("Batch cancelled successfully");
}
```

### Error Handling

```csharp
using BatchCore.SDK.Exceptions;

try
{
    var response = await client.SubmitBatchAsync(request);
}
catch (BatchCoreConfigurationException ex)
{
    // Configuration error
    Console.WriteLine($"Configuration error: {ex.Message}");
}
catch (BatchCoreException ex)
{
    // General SDK error
    Console.WriteLine($"SDK error: {ex.Message}");
}
catch (Exception ex)
{
    // Unexpected error
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
```

## Advanced Usage

### With Cancellation Token

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

try
{
    var response = await client.SubmitBatchAsync(request, cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation was cancelled");
}
```

### Batch Processing with Metadata

```csharp
var request = new BatchRequest
{
    Id = "batch-002",
    Name = "Advanced Batch",
    Items = new List<string> { "item1", "item2" },
    Metadata = new Dictionary<string, string>
    {
        { "source", "api" },
        { "priority", "high" },
        { "user", "admin" }
    }
};
```

### Logging

The SDK uses `Microsoft.Extensions.Logging` for logging. Configure logging in your application:

```csharp
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information);
});

services.AddBatchCore(options =>
{
    options.EnableDetailedLogging = true;
});
```

## Next Steps

- Check out the [API Reference](API_REFERENCE.md)
- Explore [Advanced Topics](ADVANCED.md)
- View [Examples](../samples/)
- Read the [FAQ](FAQ.md)

## Troubleshooting

### Issue: Client throws configuration exception

**Solution**: Ensure all required configuration options are set properly:
- `TimeoutSeconds` must be greater than 0
- `MaxRetryAttempts` must be non-negative
- `BatchSize` must be greater than 0

### Issue: Package not found

**Solution**: Make sure you have the correct package source configured:

```bash
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
```

## Support

For additional help:
- Open an issue on [GitHub](https://github.com/nicolejohn-emr/batch-core-sdk/issues)
- Check the [documentation](../README.md)
- Review the [sample projects](../samples/)
