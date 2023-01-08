using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Parsing;

internal static class BlockParserFactory
{
    private static readonly Dictionary<TokenType, IBlockParser> Parsers = new()
    {
        [TokenType.Syntax] = new SyntaxParser(),
        [TokenType.Package] = new PackageParser(),
        [TokenType.Import] = new ImportParser(),
        [TokenType.Option] = new OptionParser()
    };

    public static IBlockParser Get(TokenType type)
        => Parsers[type];
}