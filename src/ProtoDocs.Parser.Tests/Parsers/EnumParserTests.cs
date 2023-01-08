using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public class EnumParserTests : ParserTests
{
    private readonly EnumParser _parser;

    public EnumParserTests()
    {
        _parser = new EnumParser();
    }

    [Fact]
    public void Parse_Plain()
    {
        // arrange
        const string content = """ 
            enum MyEnum { 
                FOO = 0;
                FOO_BAR = 1;
            }
        """;
        var tokens = Tokenize(content);
        
        // act
        var block = (EnumBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Equal("MyEnum", block.Name);
        Assert.Equal(2, block.Fields.Count());
    }

    [Fact]
    public void Parse_WithOption()
    {
        // arrange
        const string content = """ 
            enum MyEnum { 
                option allow_alias = true;
                FOO = 0;
                FOO_BAR = 1;
            }
        """;
        var tokens = Tokenize(content);
        
        // act
        var block = (EnumBlock) _parser.Parse(tokens);
        
        // assert
        var option = block.Options.FirstOrDefault();
        Assert.NotNull(option);
        Assert.Equal("allow_alias", option.Name);
        Assert.Equal("true", option.Value);
    }

    [Fact]
    public void Parse_WithFieldOptions()
    {
        // arrange
        const string content = """ 
            enum MyEnum {                
                FOO = 0 [(custom_option) = "hello world"];
                FOO_BAR = 1;
            }
        """;
        var tokens = Tokenize(content);
        
        // act
        var block = (EnumBlock) _parser.Parse(tokens);
        
        // assert
        var field = block.Fields.Single(f => f.Name == "FOO");
        Assert.Single(field.Options);
        var option = field.Options.Single();
        Assert.NotNull(option);
        Assert.Equal("custom_option", option.Name);
        Assert.Equal("hello world", option.Value);
    }
}

public class EnumFieldParserTests : ParserTests
{
    private readonly EnumFieldParser _parser;

    public EnumFieldParserTests()
    {
        _parser = new EnumFieldParser();
    }

    [Fact]
    public void Parse_NoOptions()
    {
        // arrange
        const string content = "EAA_UNSPECIFIED = 1;";
        var tokens = Tokenize(content);

        // act
        var block = (EnumFieldBlock)_parser.Parse(tokens);

        // assert
        Assert.Equal("EAA_UNSPECIFIED", block.Name);
        Assert.Equal(1, block.Value);
        Assert.Empty(block.Options);
    }

    [Fact]
    public void Parse_WithOptions()
    {
        // arrange
        const string content = "EAA_UNSPECIFIED = 1 [(custom_option) = \"hello world\"];";
        var tokens = Tokenize(content);
        
        // act
        var block = (EnumFieldBlock)_parser.Parse(tokens);

        // assert
        Assert.Equal("EAA_UNSPECIFIED", block.Name);
        Assert.Equal(1, block.Value);
        Assert.Single(block.Options);
    }
}