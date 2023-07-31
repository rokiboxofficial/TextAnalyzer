using System.Text.RegularExpressions;

namespace TextAnalyzer.Models;

internal sealed class FileTextParser
{
    private readonly string[] _filesForAnalysis;
    private readonly HashSet<string> _wordsForAnalysis;

    public FileTextParser(string[] filesToAnalyze, string[] words)
    {
        _filesForAnalysis = filesToAnalyze;
        _wordsForAnalysis = new HashSet<string>(words);
    }

    public async Task<TextParsingResult> ParseAsync(TextParsingOptions options)
    {
        var fileEntriesByWord = new Dictionary<string, Dictionary<string, int>>();
        var wordsCountByWord = new Dictionary<string, int>();

        foreach (var file in _filesForAnalysis)
            await ParseFile(options, fileEntriesByWord, wordsCountByWord, file);

        var parsingResult = new TextParsingResult(fileEntriesByWord, wordsCountByWord);

        return parsingResult;
    }

    private async Task ParseFile(
        TextParsingOptions options,
        Dictionary<string, Dictionary<string, int>> fileEntriesByFilePath,
        Dictionary<string, int> wordsCountByWord,
        string file)
    {
        var lines = File.ReadLinesAsync(file);

        await foreach (var line in lines)
            ParseLine(options, fileEntriesByFilePath, wordsCountByWord, file, line);
    }

    private void ParseLine(
        TextParsingOptions options,
        Dictionary<string, Dictionary<string, int>> fileEntriesByWord,
        Dictionary<string, int> wordsCountByWord,
        string file,
        string line)
    {
        var words = GetWordsWithParsingOptions(line, options);

        foreach (var word in words)
        {
            if (!_wordsForAnalysis.Contains(word))
                continue;

            if (!fileEntriesByWord.ContainsKey(word))
                fileEntriesByWord[word] = new();
            var fileEntry = fileEntriesByWord[word];

            if (!fileEntry.ContainsKey(file))
                fileEntry[file] = 0;
            fileEntry[file]++;

            if (!wordsCountByWord.ContainsKey(word))
                wordsCountByWord[word] = 0;
            wordsCountByWord[word]++;
        }
    }

    private IEnumerable<string> GetWordsWithParsingOptions(string s, TextParsingOptions options)
    {
        var regex = new Regex(@"[A-Za-zА-Яа-я0-9]+");
        var matchCollection = regex.Matches(s);

        return matchCollection.Select(match => ConvertWordWithOptions(match.Value));

        string ConvertWordWithOptions(string word)
        {
            return options == TextParsingOptions.IgnoreCase
                ? word.ToLower()
                : word;
        }
    }
}