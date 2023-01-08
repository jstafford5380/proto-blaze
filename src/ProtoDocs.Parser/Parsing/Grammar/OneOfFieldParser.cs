using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;
using Lexicon = ProtoDocs.Parser.Lexing.Lexicon;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class OneOfFieldParser : FieldBlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("oneof", keyword.Value, "Expected keyword 'oneof'");

        var name = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, name.Value);

        var openBrace = tokens.Dequeue();
        AssertSymbol('{', openBrace);

        var oneOf = new OneOfFieldBlock(name.Value);
        
        var nextToken = tokens.Peek();
        while (nextToken.Value != "}")
        {
            ParsedBlock block;
            switch (nextToken.Type)
            {
                case TokenType.Type:
                case TokenType.Identifier:
                    var fieldParser = BlockParserFactory.Get(TokenType.NormalField);
                    block = fieldParser.Parse(tokens);
                    break;
                case TokenType.Option:
                    var optionParser = BlockParserFactory.Get(TokenType.Option);
                    block = optionParser.Parse(tokens);
                    break;
                default:
                    throw new InvalidOperationException($"{nextToken.Type} is not a valid child of Oneof");
            }
            
            oneOf.AddChild(block);
            nextToken = tokens.Peek();
        }

        var closeBrace = tokens.Dequeue();
        AssertSymbol('}', closeBrace);

        return oneOf;
    }
}