using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Configuration;
using TextAnalyzer.Factories;

namespace TextAnalyzer;

file static class Program
{
    private static async Task Main(string[] args)
    {
        if (!TryParseArgs(args, out var configuration))
            return;

        var serviceProvider = new ServiceCollection()
            .AddSingleton(configuration)
            .AddTransient<Application>()
            .AddTransient<IPresenterFactory, ConsolePresenterFactory>()
            .AddTransient<TextParserFactory>()
            .BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<Application>();
        await app.StartAsync();
    }

    private static bool TryParseArgs(string[] args, out ParserConfiguration configuration)
    {
        var parser = new Parser(with => {
            with.CaseInsensitiveEnumValues = true;
            with.HelpWriter = Console.Error;
        });

        var parserResult = parser.ParseArguments<ParserConfiguration>(args);

        configuration = parserResult.Value;

        return parserResult.Tag == ParserResultType.Parsed;
    }
}