namespace ProtoDocs.Parser;

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

public class Token
{
    public TokenType Type { get; set; }

    public string Value { get; set; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString() => $"{Type}:{Value}";
}