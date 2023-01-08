using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;
using ProtoDocs.Parser.Parsing;

namespace ProtoDocs.Parser.Tests;

public class LexerTests
{
    [Fact]
    public void Test1()
    {
        var content = File.ReadAllText("test.proto");
        var lexer = new ProtoLexer(content);

        var tokens = lexer.Tokenize().ToList();
        var parser = new ProtoParser();
        var blocks = parser.Parse(tokens.ToList());
    }

    [Theory]
    [InlineData("// this is a comment")]
    [InlineData("// this is a\n//multiline comment")]
    [InlineData("// this is a    \n//multiline comment")]
    public void Tokenize_Comment(string content)
    {
        // arrange
        var lexer = new ProtoLexer(content);
        
        // act
        var tokens = lexer.Tokenize().ToList();
        
        // assert
        Assert.All(tokens, t => Assert.Equal(TokenType.Comment, t.Type));
    }

    [Theory]
    [InlineData("syntax = \"proto3\";")]
    [InlineData("syntax  = \"proto3\"   ;")]
    public void Tokenize_Syntax(string content)
    {
        // arrange
        var lexer = new ProtoLexer(content);
        
        // act
        var tokens = lexer.Tokenize().ToList();
        
        // assert
        Assert.Equal(TokenType.Syntax, tokens[0].Type);
        Assert.Equal(TokenType.Symbol, tokens[1].Type);
        Assert.Equal(TokenType.QuotedString, tokens[2].Type);
        Assert.Equal(TokenType.Symbol, tokens[3].Type);
    }

    [Theory]
    [InlineData("package curator;")]
    public void Tokenize_Package(string content)
    {
        // arrange
        
        // act
        var tokens = ArrangeAct(content);

        // assert
        Assert.Equal(TokenType.Package, tokens[0].Type);
        Assert.Equal(TokenType.Identifier, tokens[1].Type);
        Assert.Equal(TokenType.Symbol, tokens[2].Type);
    }

    [Theory]
    [InlineData("import \"google/protobuf/timestamp.proto\";")]
    public void Tokenize_Import(string content)
    {
        // arrange
        
        // act
        var tokens = ArrangeAct(content);

        // assert
        Assert.Equal(TokenType.Import, tokens[0].Type);
        Assert.Equal(TokenType.QuotedString, tokens[1].Type);
        Assert.Equal(TokenType.Symbol, tokens[2].Type);
    }

    [Theory]
    [InlineData("option csharp_namespace = \"Some.Value\"")]
    [InlineData("option oodly_doodlyDog = \"Some spaced value\"")]
    public void Tokenize_Option(string content)
    {
        // arrange
        
        // act
        var tokens = ArrangeAct(content);

        // assert
        Assert.Equal(TokenType.Option, tokens[0].Type);
        Assert.Equal(TokenType.Identifier, tokens[1].Type);
        Assert.Equal(TokenType.Symbol, tokens[2].Type);
        Assert.Equal(TokenType.QuotedString, tokens[3].Type);
    }

    [Theory]
    [InlineData("service MyService {")]
    public void Tokenize_Service(string content)
    {
        // arrange
        
        // act
        var tokens = ArrangeAct(content);
        
        // assert
        Assert.Equal(TokenType.Service, tokens[0].Type);
        Assert.Equal(TokenType.Identifier, tokens[1].Type);
        Assert.Equal(TokenType.Symbol, tokens[2].Type);
    }

    private static IList<Token> ArrangeAct(string content)
    {
        var lexer = new ProtoLexer(content);
        return lexer.Tokenize().ToList();
    }
}