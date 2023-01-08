using System.Resources;
using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public class ReservedFieldParserTests : ParserTests
{
    private readonly ReservedParser _parser;

    public ReservedFieldParserTests()
    {
        _parser = new ReservedParser();
    }

    [Fact]
    public void Parse_Ranges()
    {
        // arrange
        const string content = "reserved 2, 15, 9 to 11;";
        var expected = new[] { 2, 15, 9, 10, 11 };
        var tokens = Tokenize(content);

        // act
        var block = (ReservedBlock)_parser.Parse(tokens);

        // assert
        Assert.All(expected, i => Assert.Contains(i, block.Positions));
    }

    [Fact]
    public void Parse_Fields()
    {
        // arrange
        const string content = "reserved \"foo\", \"bar\";";
        var expected = new[] { "foo", "bar" };
        var tokens = Tokenize(content);
        
        // act
        var block = (ReservedBlock) _parser.Parse(tokens);
        
        // assert
        Assert.All(expected, i => Assert.Contains(i, block.FieldNames));
    }
}