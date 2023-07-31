using TextAnalyzer.Abstractions;
using TextAnalyzer.Models;

namespace TextAnalyzer;

internal sealed class ConsolePresenter : IPresenter
{
    private readonly TextParsingResult _parsingResult;

    public ConsolePresenter(TextParsingResult parsingResult)
    {
        _parsingResult = parsingResult;
    }

    public void Show(ResultPresenterOptions resultPresenterOptions)
    {
        switch (resultPresenterOptions)
        {
            case ResultPresenterOptions.GroupByWord:
                ShowResultByWords();
                break;
            case ResultPresenterOptions.GroupByDirectory:
                ShowResultByDirectories();
                break;
        }
    }

    private void ShowResultByWords()
    {
        foreach(var wordReport in _parsingResult)
            Console.WriteLine($"{wordReport.Word}: {wordReport.WordsTotalCount}");
    }

    private void ShowResultByDirectories()
    {
        foreach(var wordReport in _parsingResult)
        {
            Console.WriteLine($"{wordReport.Word}:");
            foreach (var fileEntry in wordReport)
                Console.WriteLine($"\t{Path.GetFileName(fileEntry.FilePath)}: {fileEntry.WordsCount}");
        }
    }
}