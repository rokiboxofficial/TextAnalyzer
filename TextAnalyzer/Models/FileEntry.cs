namespace TextAnalyzer.Models;

internal sealed class FileEntry
{
    public FileEntry(string filePath, int wordsCount)
    {
        FilePath = filePath;
        WordsCount = wordsCount;
    }

    public string FilePath { get; } = null!;
    public int WordsCount { get; }
}