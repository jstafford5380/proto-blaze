using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;
using Lexicon = ProtoDocs.Parser.Lexing.Lexicon;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class RpcParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("rpc", keyword.Value);

        var rpcName = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, rpcName.Value);
        
        var requestOpenParen = tokens.Dequeue();
        AssertValue("(", requestOpenParen.Value, "Expected open parenthesis after rpc");

        var requestIsStream = tokens.Peek().Type == TokenType.Stream;
        if (requestIsStream)
            tokens.Dequeue();

        var requestType = tokens.Dequeue();
        AssertValue(Lexicon.FullIdentifier, requestType.Value);

        var requestCloseParen = tokens.Dequeue();
        AssertValue(")", requestCloseParen.Value, "Expected closed parenthesis after request type");

        var returns = tokens.Dequeue();
        AssertType(TokenType.Returns, returns);

        var responseOpenParen = tokens.Dequeue();
        AssertValue("(", responseOpenParen.Value, "Expected open parenthesis after returns");

        var responseIsStream = tokens.Peek().Type == TokenType.Stream;
        if (responseIsStream)
            tokens.Dequeue();

        var responseType = tokens.Dequeue();
        AssertValue(Lexicon.FullIdentifier, responseType.Value);

        var responseCloseParen = tokens.Dequeue();
        AssertValue(")", responseCloseParen.Value, "Expected closed parenthesis after response type");

        // TODO: support option body after close paren but before terminator
        /*
         * service EchoService {
         *    rpc Echo(EchoMessage) returns (EchoMessage) {
         *      option (google.api.http) = {
         *        post: "/v1/echo"
         *        body: "*"
         *      };
         *    }
         *  }
         */
        var terminator = tokens.Dequeue();
        AssertTrue(Lexicon.IsTerminator(terminator), "Expected terminator after return type closed parenthesis");

        return new RpcBlock(
            rpcName.Value, 
            requestType.Value, 
            requestIsStream, 
            responseType.Value, 
            responseIsStream);
    }
}