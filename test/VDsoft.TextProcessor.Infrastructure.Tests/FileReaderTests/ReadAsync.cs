namespace VDsoft.TextProcessor.Infrastructure.Tests.FileReaderTests;

public class ReadAsync
{
    private const string testFilePath = @"TestData\TextFile.txt";
    private readonly CancellationTokenSource tokenSource;
    private readonly FileReader sut;

    public ReadAsync()
    {
        tokenSource = new();
        sut = new();
    }

    [Fact]
    public async Task ReadAsync_FileIsPresentAndHasContent_ContentOfTheFileIsRead()
    {
        // Act
        var actual = await sut.ReadFileAsync(testFilePath, tokenSource.Token);

        // Assert
        actual.Should().NotBeNullOrWhiteSpace().And.Be("Content of the test file should be read");
    }

    [Fact]
    public async Task ReadAsync_ProvidedFileNotFound_FileNotFoundException()
    {
        // Arrange
        var filePath = @"X:\Shall\Not\Exist.txt";
        var expected = new FileNotFoundException($"The file '{filePath}' does not exist");

        // Act
        Func<Task> invokation = async () => await sut.ReadFileAsync(filePath, tokenSource.Token);

        // Assert
        await invokation.Should().ThrowAsync<FileNotFoundException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ReadAsync_PathIsNull_ArgumentNullException(string path)
    {
        // Arrange
        var expected = new ArgumentNullException("filePath", "The provided path must not be null or empty");

        // Act
        Func<Task> invokation = async () => await sut.ReadFileAsync(path, tokenSource.Token);

        // Assert
        await invokation.Should().ThrowAsync<ArgumentNullException>().WithMessage(expected.Message);
    }

    [Fact]
    public async Task ReadAsync_CancellationTokenNoneProvided_ArgumentException()
    {
        // Arrange
        var token = CancellationToken.None;
        var expected = new ArgumentException("Please provide a valid cancellation token", "cancellationToken");

        // Act
        Func<Task> invokation = async () => await sut.ReadFileAsync(testFilePath, token);

        // Assert
        await invokation.Should().ThrowAsync<ArgumentException>().WithMessage(expected.Message);
    }

    [Fact]
    public async Task ReadAsync_OperationIsCanceled_OperationCanceledException()
    {
        // Arrange
        tokenSource.Cancel();

        // Act
        Func<Task> invokation = async () => await sut.ReadFileAsync(testFilePath, tokenSource.Token);

        // Assert
        await invokation.Should().ThrowAsync<OperationCanceledException>();
    }
}
