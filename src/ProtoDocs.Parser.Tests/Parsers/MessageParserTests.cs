using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;
using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public abstract class ParserTests
{
    protected static Queue<Token> Tokenize(string content)
    {
        var lexer = new ProtoLexer(content);
        var tokens = lexer.Tokenize();
        return new Queue<Token>(tokens);
    }
}

public class MessageParserTests : ParserTests
{
    private readonly MessageParser _parser;

    public MessageParserTests()
    {
        _parser = new MessageParser();
    }

    [Fact]
    public void Parse_TypedOnly_Parses()
    {
        // arrange
        const string content = """ 
            message EntityNotification {
              string foo = 1;
              int32 bar = 2;
            }    
        """;
        var tokens = Tokenize(content);

        // act
        var block = (MessageBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Equal(2, block.Children.Count);
        Assert.Equal("EntityNotification", block.Name);
    }

    
}