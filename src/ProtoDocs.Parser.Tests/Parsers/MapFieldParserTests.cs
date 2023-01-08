using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public class MapFieldParserTests : ParserTests
{
    private readonly MapFieldParser _parser;

    public MapFieldParserTests()
    {
        _parser = new MapFieldParser();
    }

    [Fact]
    public void Parse_NoOptions()
    {
        // arrange
        var content = "map<string, FooBar> some_field = 2;";
        var tokens = Tokenize(content);

        // act
        var block = (MapFieldBlock)_parser.Parse(tokens);

        // assert
        Assert.True(block.KeyType.Equals(ProtobufType.String));
        Assert.Equal("FooBar", block.ValueType);
        Assert.Equal(2, block.Position);
    }
}