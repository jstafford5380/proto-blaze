using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public class ReservedFieldParser : ParserTests
{
    private readonly ReservedParser _parser;

    public ReservedFieldParser()
    {
        _parser = new ReservedParser();
    }

    [Theory]
    [InlineData("reserved 2, 15, 9 to 11")]
    public void Parse_Ranges(string content)
    {
        // arrange
        var tokens = Tokenize(content);

        // act
        var block = (ReservedBlock)_parser.Parse(tokens);

        // assert
        Assert.All(block.);
    }
}