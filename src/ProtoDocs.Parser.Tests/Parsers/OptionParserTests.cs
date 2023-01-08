using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public class OptionParserTests : ParserTests
{
    private readonly OptionParser _parser;

    public OptionParserTests()
    {
        _parser = new OptionParser();
    }

    [Theory]
    [InlineData("packed=true", "packed", "true")]
    [InlineData("some_other_thing=\"quoted value\"", "some_other_thing", "quoted value")]
    public void Parse_SimpleOption(string content, string name, string value)
    {
        // arrange
        var tokens = Tokenize(content);
        
        // act
        var block = (OptionBlock)_parser.Parse(tokens);
        
        // assert
        Assert.Equal(name, block.Name);
        Assert.Equal(value, block.Value);
    }

    [Theory]
    [InlineData("(foo.bar.baz)=true", "foo.bar.baz", "true")]
    [InlineData("(foo.bar.baz.bat) = \"quoted value\"", "foo.bar.baz.bat", "quoted value")]
    public void Parse_FullIdent(string content, string name, string value)
    {
        // arrange
        var tokens = Tokenize(content);
        
        // act
        var block = (OptionBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Equal(name, block.Name);
        Assert.Equal(value, block.Value);
    }
}