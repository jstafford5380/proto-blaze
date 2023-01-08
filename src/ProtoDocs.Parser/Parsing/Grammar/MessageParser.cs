using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;
using Lexicon = ProtoDocs.Parser.Lexing.Lexicon;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class MessageParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("message", keyword.Value);

        var messageName = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, messageName.Value);
        
        var openBrace = tokens.Dequeue();
        AssertValue("{", openBrace.Value, "Expected opening curly brace after message keyword");
        
        var messageBlock = new MessageBlock(messageName.Value);
        
        var currentToken = tokens.Peek();
        while (currentToken.Value != "}")
        {
            ParsedBlock block;
            switch (currentToken.Type)
            {
                case TokenType.Repeated:
                case TokenType.Type:
                case TokenType.Identifier:
                    var fieldParser = BlockParserFactory.Get(TokenType.NormalField);
                    block = fieldParser.Parse(tokens);
                    break;
                case TokenType.OneOf:
                    var oneOfParser = BlockParserFactory.Get(TokenType.OneOf);
                    block = oneOfParser.Parse(tokens);
                    break;
                case TokenType.Map:
                    var mapParser = BlockParserFactory.Get(TokenType.Map);
                    block = mapParser.Parse(tokens);
                    break;
                case TokenType.Reserved:
                    var reservedParser = BlockParserFactory.Get(TokenType.Reserved);
                    block = reservedParser.Parse(tokens);
                    break;
                default:
                    var parser = BlockParserFactory.Get(currentToken.Type);
                    block = parser.Parse(tokens);
                    messageBlock.AddChild(block);
                    break;
            }


            messageBlock.AddChild(block);
            currentToken = tokens.Peek();
        }
        
        var closeBrace = tokens.Dequeue();
        AssertSymbol('}', closeBrace);

        return messageBlock;
    }
}