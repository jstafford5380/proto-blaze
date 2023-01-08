using System.Text.RegularExpressions;

namespace ProtoDocs.Parser.LexicalAnalysis;

public partial class Lexicon
{
    public static Regex Message => MessageRegex();
    public static Regex Enum => EnumRegex();
    public static Regex Repeated => RepeatedRegex();
    public static Regex Identifier => IdentifierRegex();
    public static Regex FullIdentifier => FullIdentifierRegex();
    public static Regex Option => OptionRegex();
    public static Regex Number => NumberRegex();
    public static Regex String => StringRegex();
    public static Regex Symbol => SymbolRegex();
    public static Regex Syntax => SyntaxRegex();
    public static Regex Package => PackageRegex();
    public static Regex Import => ImportRegex();
    public static Regex Service => ServiceRegex();
    public static Regex Rpc => RpcRegex();
    public static Regex Returns => ReturnsRegex();
    public static Regex Type => TypeRegex();
    public static Regex QuotedString => QuotedStringRegex();
    public static Regex QuotedPath => QuotedPathRegex();

    public static Regex Comment => CommentRegex();

    public static bool IsTerminator(Token token) 
        => token.Type is TokenType.Symbol && token.Value is ";";
    

    [GeneratedRegex("^message$")]
    private static partial Regex MessageRegex();

    [GeneratedRegex("^enum$")]
    private static partial Regex EnumRegex();

    [GeneratedRegex("^repeated$")]
    private static partial Regex RepeatedRegex();

    [GeneratedRegex("^[A-Za-z_][A-Za-z0-9_]*$")]
    private static partial Regex IdentifierRegex();

    [GeneratedRegex(@"^[A-Za-z_][A-Za-z0-9_]*(\.[A-Za-z_][A-Za-z0-9_]*)*$")]
    private static partial Regex FullIdentifierRegex();

    [GeneratedRegex("^option$")]
    private static partial Regex OptionRegex();

    [GeneratedRegex("^[0-9]+$")]
    private static partial Regex NumberRegex();

    [GeneratedRegex(@"^\""[^\""]*\""$")]
    private static partial Regex StringRegex();

    [GeneratedRegex(@"^[{}\[\]\(\);=,]$")]
    private static partial Regex SymbolRegex();

    [GeneratedRegex("^syntax$")]
    private static partial Regex SyntaxRegex();

    [GeneratedRegex("^package$")]
    private static partial Regex PackageRegex();

    [GeneratedRegex("^import$")]
    private static partial Regex ImportRegex();

    [GeneratedRegex("^service$")]
    private static partial Regex ServiceRegex();

    [GeneratedRegex("^rpc$")]
    private static partial Regex RpcRegex();

    [GeneratedRegex("^returns$")]
    private static partial Regex ReturnsRegex();
    
    [GeneratedRegex("^reserved$")]
    private static partial Regex ReservedRegex();

    [GeneratedRegex(
        "^(double|float|int32|int64|uint32|uint64|sint32|sint64|fixed32|fixed64|sfixed32|sfixed64|bool|string|bytes)$")]
    private static partial Regex TypeRegex();

    [GeneratedRegex("^\"(.*)\"$")]
    private static partial Regex QuotedStringRegex();

    [GeneratedRegex("^\"(.+)(\\/([^\\/]+))*\"$")]
    private static partial Regex QuotedPathRegex();

    [GeneratedRegex("^\\/\\/(.*)$")]
    private static partial Regex CommentRegex();
}