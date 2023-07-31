using TextAnalyzer.Models;

namespace TextAnalyzer.Factories;

internal sealed class TextParserFactory
{
    public FileTextParser Create(string[] filesToAnalyze, string[] words)
        => new(filesToAnalyze, words);
}