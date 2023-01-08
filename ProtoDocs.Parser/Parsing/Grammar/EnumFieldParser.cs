using ProtoDocs.Parser.LexicalAnalysis;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class EnumFieldParser : FieldBlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var name = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, name.Value);

        var equals = tokens.Dequeue();
        AssertSymbol('=', equals);

        var value = tokens.Dequeue();
        AssertType(TokenType.Number, value);

        var block = new EnumFieldBlock(name.Value, int.Parse(value.Value));
        
        TryAddOptions(block, tokens);

        return block;
    }
}