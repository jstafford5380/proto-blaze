using ProtoDocs.Parser.Parsing;
using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Tests.Parsers;

public class RpcParserTests : ParserTests
{
    private readonly RpcParser _parser;

    public RpcParserTests()
    {
        _parser = new RpcParser();
    }

    [Fact]
    public void Parse_NoStreams()
    {
        // arrange
        const string content = "rpc Search(SearchRequest) returns (SearchResponse);";
        var tokens = Tokenize(content);
        
        // act
        var block = (RpcBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Equal("Search", block.Name);
        Assert.Equal("SearchRequest", block.RequestType);
        Assert.Equal("SearchResponse", block.ResponseType);
        Assert.False(block.RequestIsStream);
        Assert.False(block.ResponseIsStream);
    }

    [Fact]
    public void Parse_RequestIsStream()
    {
        // arrange
        const string content = "rpc Search(stream SearchRequest) returns (SearchResponse);";
        var tokens = Tokenize(content);
        
        // act
        var block = (RpcBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Equal("Search", block.Name);
        Assert.Equal("SearchRequest", block.RequestType);
        Assert.Equal("SearchResponse", block.ResponseType);
        Assert.True(block.RequestIsStream);
        Assert.False(block.ResponseIsStream);
    }
    
    [Fact]
    public void Parse_ResponseIsStream()
    {
        // arrange
        const string content = "rpc Search(SearchRequest) returns (stream SearchResponse);";
        var tokens = Tokenize(content);
        
        // act
        var block = (RpcBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Equal("Search", block.Name);
        Assert.Equal("SearchRequest", block.RequestType);
        Assert.Equal("SearchResponse", block.ResponseType);
        Assert.False(block.RequestIsStream);
        Assert.True(block.ResponseIsStream);
    }
    
    [Fact]
    public void Parse_TwoWayStream()
    {
        // arrange
        const string content = "rpc Search(stream SearchRequest) returns (stream SearchResponse);";
        var tokens = Tokenize(content);
        
        // act
        var block = (RpcBlock) _parser.Parse(tokens);
        
        // assert
        Assert.Equal("Search", block.Name);
        Assert.Equal("SearchRequest", block.RequestType);
        Assert.Equal("SearchResponse", block.ResponseType);
        Assert.True(block.RequestIsStream);
        Assert.True(block.ResponseIsStream);
    }
}