using CommandLine;
using TextAnalyzer.Models;

namespace TextAnalyzer.Configuration;

internal sealed class ParserConfiguration
{
    [Option(shortName: 'd', Required = true, HelpText = "Directory path")]
    public string DirectoryPath { get; set; } = null!;
    [Option(shortName: 'w', Required = true, HelpText = "Name of file with words")]
    public string WordsFileName { get; set; } = null!;
    [Option(shortName: 'o', Required = true, HelpText = "Presentation kind (GroupByWord/GroupByDirectory)")]
    public ResultPresenterOptions PresenterOptions { get; set; }
    [Option(shortName: 'c', Required = true, HelpText = "Parsing kind (CaseSensitive/IgnoreCase)")]
    public TextParsingOptions ParsingOptions { get; set; }
}