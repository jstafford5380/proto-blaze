using ProtoDocs.Parser.LexicalAnalysis;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class NormalFieldParser : FieldBlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var token = tokens.Dequeue();
        AssertTrue(
            token.Type is TokenType.Type or TokenType.Repeated or TokenType.Identifier,
            "Fields must start with 'repeated', a type, or an identifier or else they require further parsing (map or oneof)");

        var repeated = token.Type is TokenType.Repeated;
        if (repeated)
            token = tokens.Dequeue();
        
        AssertTrue(token.Type is TokenType.Type or TokenType.Identifier, $"Expected type or identifier but got '{token.Type}:{token.Value}'");
        var fieldType = token.Value;

        var nameToken = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, nameToken.Value);

        var equals = tokens.Dequeue();
        AssertSymbol('=', equals);

        var positionToken = tokens.Dequeue();
        AssertType(TokenType.Number, positionToken);

        var fieldBlock = new NormalFieldBlock(nameToken.Value, fieldType, repeated, int.Parse(positionToken.Value));

        TryAddOptions(fieldBlock, tokens);

        return fieldBlock;
    }
}