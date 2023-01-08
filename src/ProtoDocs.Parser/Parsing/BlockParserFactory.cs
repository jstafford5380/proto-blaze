using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Parsing;

internal static class BlockParserFactory
{
    private static readonly Dictionary<TokenType, IBlockParser> Parsers = new()
    {
        [TokenType.Syntax] = new SyntaxParser(),
        [TokenType.Package] = new PackageParser(),
        [TokenType.Import] = new ImportParser(),
        [TokenType.Option] = new OptionBlockParser(),
        [TokenType.Service] = new ServiceParser(),
        [TokenType.Rpc] = new RpcParser(),
        [TokenType.Message] = new MessageParser(),
        [TokenType.NormalField] = new NormalFieldParser(),
        [TokenType.OneOf] = new OneOfFieldParser(),
        [TokenType.Map] = new MapFieldParser(),
        [TokenType.Enum] = new EnumParser(),
        [TokenType.EnumField] = new EnumFieldParser(),
        [TokenType.Reserved] = new ReservedParser()
    };

    public static IBlockParser Get(TokenType type)
        => Parsers[type];

    public static bool TryGet(TokenType type, out IBlockParser? parser)
        => Parsers.TryGetValue(type, out parser);
}