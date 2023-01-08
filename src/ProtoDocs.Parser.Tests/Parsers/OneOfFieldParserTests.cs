using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public class OneOfFieldParserTests : ParserTests
{
    private readonly OneOfFieldParser _parser;

    public OneOfFieldParserTests()
    {
        _parser = new OneOfFieldParser();
    }

    [Fact]
    public void Parse_FieldsOnly()
    {
        // arrange
        const string content = """ 
            oneof foo {
                string name = 4;
                SubMessage sub_message = 9;
            }
        """;
        var tokens = Tokenize(content);

        // act
        var block = (OneOfFieldBlock)_parser.Parse(tokens);

        // assert
        Assert.Equal(2, block.Fields.Count());
        Assert.Equal("foo", block.Name);
        Assert.Contains(block.Fields, f => f is { Name: "name", FieldType: "string", Position: 4 });
        Assert.Contains(block.Fields, f => f is { Name: "sub_message", FieldType: "SubMessage", Position: 9 });
    }

    [Fact]
    public void Parse_WithOptions()
    {
        // arrange
        const string content = """ 
            oneof foo {
                option (some.option.value)=true;
                string name = 1;
            }
        """;
        var tokens = Tokenize(content);
        
        // act
        var block = (OneOfFieldBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Single(block.Fields);
        Assert.Single(block.Options);
        Assert.Equal("some.option.value", block.Options.Single().Name);
    }
}