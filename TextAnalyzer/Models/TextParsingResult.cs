using System.Collections;

namespace TextAnalyzer.Models;

internal sealed class TextParsingResult : IEnumerable<WordReport>
{
    private readonly Dictionary<string, Dictionary<string, int>> _fileEntriesByWord;
    private readonly Dictionary<string, int> _wordsCountByWord;

    public TextParsingResult(
        Dictionary<string, Dictionary<string, int>> fileEntriesByWord,
        Dictionary<string, int> wordsCountByWord)
    {
        _fileEntriesByWord = fileEntriesByWord;
        _wordsCountByWord = wordsCountByWord;
    }

    public IEnumerator<WordReport> GetEnumerator()
    {
        return _fileEntriesByWord.Select(kvpair => new WordReport(kvpair.Value, kvpair.Key, _wordsCountByWord[kvpair.Key]))
                                 .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}