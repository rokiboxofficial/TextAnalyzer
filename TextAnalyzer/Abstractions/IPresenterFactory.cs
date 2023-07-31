using TextAnalyzer.Abstractions;
using TextAnalyzer.Models;

namespace TextAnalyzer.Factories;

internal interface IPresenterFactory
{
    public IPresenter Create(TextParsingResult parsingResult);
}