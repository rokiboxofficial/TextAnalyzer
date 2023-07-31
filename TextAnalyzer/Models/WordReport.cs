using System.Collections;

namespace TextAnalyzer.Models;

internal sealed class WordReport : IEnumerable<FileEntry>
{
    private readonly Dictionary<string, int> _wordsCountByFilePath;

    public WordReport(Dictionary<string, int> wordsCountByFilePath, string word, int wordsTotalCount)
    {
        Word = word;
        WordsTotalCount = wordsTotalCount;
        _wordsCountByFilePath = wordsCountByFilePath;
    }

    public string Word { get; } = null!;
    public int WordsTotalCount { get; }

    public IEnumerator<FileEntry> GetEnumerator()
        => _wordsCountByFilePath.Select(kvpair => new FileEntry(kvpair.Key, kvpair.Value)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}