using VDsoft.TextProcessor.BusinessLogic.Processor;

namespace VDsoft.TextProcessor.BusinessLogic.Tests.Processor.WordCounterTests
{
    public class Count
    {
        private const string Text = "a a b b c c";
        private readonly CancellationTokenSource tokenSource;

        public Count()
        {
            tokenSource = new();
        }

        [Fact]
        public void Count_ValidDataProvided_DictionaryWithAllWordsAndCount()
        {
            // Arrange
            var expected = new Dictionary<string, int>
            {
                ["a"] = 2,
                ["b"] = 2,
                ["c"] = 2
            };

            // Act
            var actual = WordCounter.Count(Text, tokenSource.Token);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Count_NoneDefaultSeparatorWithCorrectTextProvided_DictionaryWIthAllWordsAndCount()
        {
            // Arrange
            var separator = '/';
            var text = "a/a/b/b/c/c";

            var expected = new Dictionary<string, int>
            {
                ["a"] = 2,
                ["b"] = 2,
                ["c"] = 2
            };

            // Act
            var actual = WordCounter.Count(text, tokenSource.Token, separator);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Count_TextIsNull_ArgumentNullException()
        {
            // Arrange
            var expectedParameterName = "text";

            // Act
            var invokation = () => WordCounter.Count(null, tokenSource.Token);

            // Assert
            invokation.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(expectedParameterName);
        }

        [Fact]
        public void Count_ProvidedTextIsEmpty_EmptyDictionary()
        {
            // Arrange
            var text = string.Empty;

            // Act
            var actual = WordCounter.Count(text, tokenSource.Token);

            // Assert
            actual.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void Count_CancellationTokenNoneProvided_ArgumentException()
        {
            // Arrange
            var token = CancellationToken.None;
            var expected = new ArgumentException("Please provide a valid cancellation token", "cancellationToken");

            // Act
            var invokation = () => WordCounter.Count(Text, token);

            // Assert
            invokation.Should().Throw<ArgumentException>().WithMessage(expected.Message).And.ParamName.Should().Be(expected.ParamName);
        }

        [Fact]
        public void Count_OperationIsCanceled_EmptyDictionary()
        {
            // Arrange
            tokenSource.Cancel();

            // Act
            var actual = WordCounter.Count(Text, tokenSource.Token);

            // Assert
            actual.Should().NotBeNull().And.BeEmpty();
        }
    }
}
