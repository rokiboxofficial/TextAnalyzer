using TextAnalyzer.Abstractions;
using TextAnalyzer.Models;

namespace TextAnalyzer.Factories;

internal sealed class ConsolePresenterFactory : IPresenterFactory
{
    public IPresenter Create(TextParsingResult parsingResult)
        => new ConsolePresenter(parsingResult);
}