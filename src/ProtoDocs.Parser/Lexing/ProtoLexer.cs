using System.Text;

namespace ProtoDocs.Parser.Lexing;

public interface IProtoLexer
{
    IEnumerable<Token> Tokenize();
}

public class ProtoLexer : IProtoLexer
{
    private readonly string _content;
    private int _position;
    private State _state = State.Seeking;
    
    public ProtoLexer(string content)
    {
        _content = content;
    }
    
    public IEnumerable<Token> Tokenize()
    {
        var buffer = new StringBuilder();
        
        while (_position < _content.Length)
        {
            var currentCharacter = _content[_position];
            
            switch (_state)
            {
                case State.Seeking:
                    if (char.IsWhiteSpace(currentCharacter))
                    {
                        _position++;
                    }
                    else if (currentCharacter == '/')
                    {
                        _state = State.StartingComment;
                    }
                    else if (currentCharacter == '"')
                    {
                        _state = State.ReadingQuotedValue;
                        _position++;
                    }
                    else if (IsSymbol(currentCharacter))
                    {
                        yield return new Token(TokenType.Symbol, currentCharacter.ToString());
                        _position++;
                    }
                    else
                    {
                        _state = State.ReadingWord;
                    }
                    break;
                case State.StartingComment:
                    if (currentCharacter == '/')
                    {
                        _position++;
                    }
                    else
                    {
                        _state = State.ReadingComment;
                    }
                    break;
                case State.ReadingComment:
                    if (currentCharacter == '\n')
                    {
                        // check if next line is continuation of comment
                        if (_content[_position + 1] == '/') // TODO: skip through all spaces (not whitespace) to get to next character in order to account for indentation
                        {
                            buffer.Append(' ');
                            _state = State.StartingComment;
                            _position++;
                        }
                        else
                        {
                            var commentToken = new Token(TokenType.Comment, buffer.ToString());
                            buffer.Clear();
                            _state = State.Seeking;
                            
                            yield return commentToken;
                        }
                    }
                    else
                    {
                        buffer.Append(currentCharacter);
                        _position++;
                    }
                    break;
                case State.ReadingQuotedValue:
                    if (currentCharacter == '"') // TODO: account for quote escapes? \"
                    {
                        yield return new Token(TokenType.QuotedString, buffer.ToString());
                        buffer.Clear();
                        _state = State.Seeking;
                        _position++;
                    }
                    else
                    {
                        buffer.Append(currentCharacter);
                        _position++;
                    }
                    break;
                case State.ReadingWord:
                    if (currentCharacter == '.')
                    {
                        buffer.Append(currentCharacter);
                        _state = State.ReadingQualifiedName;
                        _position++;
                    }
                    else if (char.IsWhiteSpace(currentCharacter) || IsSymbol(currentCharacter))
                    {
                        yield return DetermineWordKind(buffer.ToString());
                        buffer.Clear();
                        _state = State.Seeking;
                    }
                    else if (currentCharacter == '<')
                    {
                        buffer.Append(currentCharacter);
                        _state = State.ReadingGeneric;
                        _position++;
                    }
                    else
                    {
                        buffer.Append(currentCharacter);
                        _position++;
                    }
                    break;
                case State.ReadingGeneric:
                    if (currentCharacter == '>')
                    {
                        buffer.Append(currentCharacter);
                        yield return new Token(TokenType.GenericType, buffer.ToString());
                        buffer.Clear();
                        _state = State.Seeking;
                        _position++;
                    }
                    else
                    {
                        buffer.Append(currentCharacter);
                        _position++;
                    }
                    break;
                case State.ReadingQualifiedName:
                    if (char.IsWhiteSpace(currentCharacter) || IsSymbol(currentCharacter))
                    {
                        yield return DetermineWordKind(buffer.ToString());
                        buffer.Clear();
                        _state = State.Seeking;
                    }
                    else
                    {
                        buffer.Append(currentCharacter);
                        _position++;
                    }
                    break;
            }
        }
        
        // TODO: deal with this pain in the ass that is dumping the buffer 
        // if we hit the end of a file first
        if (buffer.Length > 0)
        {
            switch (_state)
            {
                case State.ReadingComment:
                    yield return new Token(TokenType.Comment, buffer.ToString());
                    buffer.Clear();
                    break;
                case State.ReadingWord:
                    yield return DetermineWordKind(buffer.ToString());
                    buffer.Clear();
                    break;
            }
        }
    }

    private static bool IsSymbol(char c)
        => Lexicon.Symbol.IsMatch(c.ToString());

    private static Token DetermineWordKind(string word)
    {
        TokenType type;
        switch (word)
        {
            case "syntax":
                type = TokenType.Syntax;
                break;
            case "package":
                type = TokenType.Package;
                break;
            case "import":
                type = TokenType.Import;
                break;
            case "option":
                type = TokenType.Option;
                break;
            case "message":
                type = TokenType.Message;
                break;
            case "service":
                type = TokenType.Service;
                break;
            case "rpc":
                type = TokenType.Rpc;
                break;
            case "repeated":
                type = TokenType.Repeated;
                break;
            case "returns":
                type = TokenType.Returns;
                break;
            case "stream":
                type = TokenType.Stream;
                break;
            case "enum":
                type = TokenType.Enum;
                break;
            case "oneof":
                type = TokenType.OneOf;
                break;
            case "reserved":
                type = TokenType.Reserved;
                break;
            case "true":
            case "false":
                type = TokenType.Boolean;
                break;
            case var _ when word.StartsWith("map<"):
                type = TokenType.Map;
                break;
            case var _ when Lexicon.Number.IsMatch(word):
                type = TokenType.Number;
                break;
            case var _ when Lexicon.Type.IsMatch(word):
                type = TokenType.Type;
                break;
            default:
                type = TokenType.Identifier;
                break;
        }

        return new Token(type, word);
    }
    
    private enum State
    {
        Seeking,
        StartingComment,
        ReadingComment,
        ReadingWord,
        ReadingGeneric,
        ReadingQuotedValue,
        ReadingQualifiedName
    }
}