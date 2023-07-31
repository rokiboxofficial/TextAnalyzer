using TextAnalyzer.Models;

namespace TextAnalyzer.Abstractions;

internal interface IPresenter
{
    public void Show(ResultPresenterOptions resultPresenterOptions);
}