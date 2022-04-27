using System.Collections.Concurrent;

namespace VDsoft.TextProcessor.BusinessLogic.Processor
{
    /// <summary>
    /// Provides functionality to count words in a text.
    /// </summary>
    public static class WordCounter
    {
        /// <summary>
        /// Counts all the words in the provided text.
        /// </summary>
        /// <param name="text">The text to count the words of.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the parallel operation.</param>
        /// <param name="separator">The separator to dived the text by. The default value is a whitespace.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> containing the words as keys and the count as a value.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static Dictionary<string, int> Count(string text, CancellationToken cancellationToken, char separator = ' ')
        {
            ArgumentNullException.ThrowIfNull(text);

            if (cancellationToken == CancellationToken.None)
            {
                throw new ArgumentException("Please provide a valid cancellation token", nameof(cancellationToken));
            }

            if (text == string.Empty)
            {
                return new Dictionary<string, int>();
            }

            var splittedText = SplitTextBySeparator(text, separator);

            var countedWords = CountWordsParallel(splittedText, cancellationToken);

            return countedWords.ToDictionary(k => k.Key, v => v.Value);
        }

        private static string[] SplitTextBySeparator(string text, char separator) => text.Split(separator).Where(x => x != string.Empty).ToArray();

        private static ConcurrentDictionary<string, int> CountWordsParallel(string[] splittedText, CancellationToken cancellationToken)
        {
            var countedWords = new ConcurrentDictionary<string, int>();
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount - 2, // host should have some reserve of cores to work
                CancellationToken = cancellationToken
            };

            try
            {
                Parallel.ForEach(
                    splittedText,
                    parallelOptions,
                    (currentWord) => countedWords.AddOrUpdate(currentWord.Trim(), 1, (key, oldValue) => ++oldValue));
            }
            catch (OperationCanceledException)
            {
                // Reset the dictionary according to PO decision.
                countedWords.Clear();
            }

            return countedWords;
        }
    }
}
