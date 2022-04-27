using VDsoft.TextProcessor.BusinessLogic.Infrastructure;

namespace VDsoft.TextProcessor.Infrastructure;

/// <summary>
/// Access the file system.
/// </summary>
/// <seealso cref="IFileReader" />
public class FileReader : IFileReader
{
    /// <inheritdoc/>
    public async Task<string> ReadFileAsync(string filePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath), "The provided path must not be null or empty");
        }

        if (cancellationToken == CancellationToken.None)
        {
            throw new ArgumentException("Please provide a valid cancellation token", nameof(cancellationToken));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' does not exist");
        }

        return string.Join(" ", await File.ReadAllLinesAsync(filePath, cancellationToken));
    }
}
