namespace VDsoft.TextProcessor.Ui.Model;

/// <summary>
/// Represents a word with the count how often it occurs in a text.
/// </summary>
/// <param name="Word">The found word.</param>
/// <param name="Count">The count how often the word occurs.</param>
public record WordCount(string Word, int Count);