namespace ProtoDocs.Parser.Lexing;

public enum TokenType
{
    Message,
    Enum,
    EnumField,
    Repeated,
    Identifier,
    FullIdentifier,
    Option,
    Number,
    Boolean,
    String,
    Symbol,
    Package,
    Import,
    Service,
    Rpc,
    Returns,
    Type,
    GenericType,
    Syntax,
    QuotedString,
    QuotedPath,
    Comment,
    Stream,
    NormalField,
    OneOf,
    Map,
    Reserved
}