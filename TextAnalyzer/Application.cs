using TextAnalyzer.Configuration;
using TextAnalyzer.Factories;

namespace TextAnalyzer;

internal sealed class Application
{
    private readonly ParserConfiguration _configuration;
    private readonly TextParserFactory _parserFactory;
    private readonly IPresenterFactory _presenterFactory;

    public Application(
        ParserConfiguration configuration,
        TextParserFactory parserFactory,
        IPresenterFactory presenterFactory)
    {
        _configuration = configuration;
        _parserFactory = parserFactory;
        _presenterFactory = presenterFactory;
    }

    public async Task StartAsync()
    {
        var directoryPath = _configuration.DirectoryPath;
        var wordsFileName = _configuration.WordsFileName;
        var parsingOptions = _configuration.ParsingOptions;
        var presenterOptions = _configuration.PresenterOptions;

        var filesForAnalysis = Directory.GetFiles(directoryPath, "*.txt")
                 .Where(path => Path.GetFileName(path) != wordsFileName)
                 .ToArray();

        var lines = await File.ReadAllLinesAsync(Path.Combine(directoryPath, wordsFileName));
        var wordsForAnalysis = lines.ToArray();

        if (wordsForAnalysis.Length == 0)
            throw new Exception("wordsForAnalysis can not be empty");

        if (filesForAnalysis.Length == 0)
            throw new Exception("filesForAnalysis can not be empty");

        var parser = _parserFactory.Create(filesForAnalysis, wordsForAnalysis);
        var parsingResult = await parser.ParseAsync(parsingOptions);

        var presenter = _presenterFactory.Create(parsingResult);
        presenter.Show(presenterOptions);
    }
}