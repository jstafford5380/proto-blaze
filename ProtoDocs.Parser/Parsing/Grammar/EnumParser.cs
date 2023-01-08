using ProtoDocs.Parser.LexicalAnalysis;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class EnumParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("enum", keyword.Value);

        var name = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, name.Value);

        var openBrace = tokens.Dequeue();
        AssertSymbol('{', openBrace);

        var block = new EnumBlock(name.Value);
        
        var nextSymbol = tokens.Peek();
        while (nextSymbol.Value != "}")
        {
            var parser = nextSymbol.Type is TokenType.Identifier 
                ? new EnumFieldParser()
                : BlockParserFactory.Get(nextSymbol.Type);
            
            var childBlock = parser.Parse(tokens);
            block.AddChild(childBlock);
            nextSymbol = tokens.Peek();
        }

        var closingBrace = tokens.Dequeue();
        AssertSymbol('}', closingBrace);
        
        return block;
    }
}