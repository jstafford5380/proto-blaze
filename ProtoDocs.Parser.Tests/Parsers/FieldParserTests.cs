using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests;

public class FieldParserTests : ParserTests
{
    private readonly FieldParser _parser;

    public FieldParserTests()
    {
        _parser = new FieldParser();
    }

    [Theory]
    [InlineData("string my_property = 2;", "string", "my_property", 2)]
    [InlineData("int32 foobar = 1;", "int32", "foobar", 1)]
    [InlineData("SomeMessageType MyMessage = 1;", "SomeMessageType", "MyMessage", 1)]
    [InlineData("double some_number  =6  ;", "double", "some_number", 6)]
    public void Parse_SimpleField(string content, string type, string name, int position)
    {
        // arrange
        var tokens = Tokenize(content);
        
        // act
        var block = (FieldBlock)_parser.Parse(tokens);
        
        // assert
        Assert.Equal(type, block.FieldType);
        Assert.Equal(name, block.Name);
        Assert.Equal(position, block.Position);
    }

    [Fact]
    public void Parse_WithSingleOption()
    {
        // arrange
        const string content = "int32 foobar = 1 [packed=true];";
        var tokens = Tokenize(content);

        // act
        var block = (FieldBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Single(block.Options);
        var option = block.Options.First();
        Assert.Equal("packed", option.Name);
        Assert.Equal("true", option.Value);
    }

    [Fact]
    public void Parse_WithMultipleOptions()
    {
        // arrange
        const string content = "int32 foobar = 1 [packed=true,other=\"foo\"];";
        var tokens = Tokenize(content);
        
        // act
        var block = (FieldBlock)_parser.Parse(tokens);
        
        // assert
        Assert.Equal(2, block.Options.Count());
    }
}