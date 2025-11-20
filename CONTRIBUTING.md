# Contributing to BatchCore SDK

Thank you for your interest in contributing to BatchCore SDK! This document provides guidelines and instructions for contributing.

## Code of Conduct

Please be respectful and constructive in all interactions. We aim to maintain a welcoming and inclusive community.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Git
- A code editor (Visual Studio, VS Code, or Rider)

### Setting Up Development Environment

1. Fork the repository on GitHub
2. Clone your fork locally:
   ```bash
   git clone https://github.com/your-username/batch-core-sdk.git
   cd batch-core-sdk
   ```

3. Add the upstream repository:
   ```bash
   git remote add upstream https://github.com/nicolejohn-emr/batch-core-sdk.git
   ```

4. Build the solution:
   ```bash
   dotnet build
   ```

5. Run tests:
   ```bash
   dotnet test
   ```

## Development Workflow

### 1. Create a Branch

Create a feature branch from `main`:

```bash
git checkout -b feature/your-feature-name
```

Use descriptive branch names:
- `feature/` for new features
- `fix/` for bug fixes
- `docs/` for documentation changes
- `refactor/` for code refactoring

### 2. Make Changes

- Write clean, readable code
- Follow C# coding conventions
- Add XML documentation comments for public APIs
- Include unit tests for new functionality
- Update documentation as needed

### 3. Test Your Changes

Before committing:

```bash
# Run all tests
dotnet test

# Build in release mode
dotnet build --configuration Release

# Run the sample application
cd samples/BatchCore.SDK.Sample
dotnet run
```

### 4. Commit Your Changes

Write clear, descriptive commit messages:

```bash
git add .
git commit -m "Add feature: description of changes"
```

Good commit messages:
- "Add batch retry mechanism with exponential backoff"
- "Fix null reference exception in GetBatchStatusAsync"
- "Update README with configuration examples"

### 5. Push and Create Pull Request

```bash
git push origin feature/your-feature-name
```

Then create a pull request on GitHub with:
- Clear description of changes
- Link to any related issues
- Screenshots (if UI changes)
- Test results

## Coding Standards

### C# Style Guide

- Use 4 spaces for indentation (not tabs)
- Follow .editorconfig settings
- Use meaningful variable and method names
- Keep methods small and focused
- Prefer explicit types over `var` for non-obvious types

### Naming Conventions

- PascalCase for classes, methods, properties, and public fields
- camelCase for local variables and private fields
- Prefix private fields with underscore: `_fieldName`
- Use descriptive names: `GetBatchStatus()` not `GetBS()`

### Documentation

- Add XML documentation comments for all public APIs
- Include `<summary>`, `<param>`, `<returns>`, and `<exception>` tags
- Provide usage examples in documentation
- Update README.md if adding new features

Example:

```csharp
/// <summary>
/// Submits a batch request for processing.
/// </summary>
/// <param name="request">The batch request to submit.</param>
/// <param name="cancellationToken">Cancellation token for the operation.</param>
/// <returns>The batch response containing processing results.</returns>
/// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
public async Task<BatchResponse> SubmitBatchAsync(
    BatchRequest request, 
    CancellationToken cancellationToken = default)
{
    // Implementation
}
```

## Testing Guidelines

### Writing Tests

- Use xUnit for unit tests
- Follow Arrange-Act-Assert pattern
- Test both success and failure scenarios
- Use descriptive test names

Example:

```csharp
[Fact]
public async Task SubmitBatchAsync_WithValidRequest_ReturnsSuccessResponse()
{
    // Arrange
    var options = new BatchCoreOptions();
    var client = new BatchCoreClient(options);
    var request = new BatchRequest { Items = new List<string> { "item1" } };

    // Act
    var response = await client.SubmitBatchAsync(request);

    // Assert
    Assert.Equal(BatchStatus.Completed, response.Status);
}
```

### Test Coverage

- Aim for high test coverage (80%+)
- Test edge cases and error conditions
- Include integration tests for critical paths

## Pull Request Process

1. **Update Documentation**: Ensure README and other docs reflect your changes
2. **Add Tests**: All new features must include tests
3. **Update Changelog**: Add entry to CHANGELOG.md
4. **Pass CI Checks**: All tests and builds must pass
5. **Code Review**: Address reviewer feedback promptly
6. **Squash Commits**: Keep history clean (if requested)

### PR Checklist

- [ ] Code builds without errors
- [ ] All tests pass
- [ ] New features have tests
- [ ] Documentation updated
- [ ] CHANGELOG.md updated
- [ ] No merge conflicts
- [ ] Follows coding standards

## Types of Contributions

### Bug Reports

When reporting bugs, include:
- Clear description of the issue
- Steps to reproduce
- Expected vs actual behavior
- Environment details (.NET version, OS)
- Stack traces or error messages

### Feature Requests

For new features:
- Explain the use case
- Describe proposed solution
- Consider alternatives
- Discuss potential impacts

### Documentation

Documentation improvements are always welcome:
- Fix typos and grammar
- Add examples
- Clarify confusing sections
- Improve structure

## Building NuGet Package

To build a NuGet package locally:

```bash
dotnet pack src/BatchCore.SDK/BatchCore.SDK.csproj --configuration Release --output ./artifacts
```

## Release Process

Releases are managed by maintainers:

1. Update version in `.csproj` file
2. Update CHANGELOG.md
3. Create GitHub release with tag (e.g., `v1.0.0`)
4. Package is automatically published to NuGet

## Questions?

If you have questions:
- Open a discussion on GitHub
- Check existing issues and pull requests
- Review documentation

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

## Recognition

Contributors will be recognized in release notes and the README.

Thank you for contributing to BatchCore SDK! 🎉
