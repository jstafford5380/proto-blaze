using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class ReservedParser : FieldBlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("reserved", keyword.Value);

        ReservedBlock block;
        
        var nextToken = tokens.Peek();
        switch (nextToken.Type)
        {
            case TokenType.Number:
                block = ParseRanges(tokens);
                break;
            case TokenType.QuotedString:
                block = ParseReservedFieldNames(tokens);
                break;
            default:
                throw new ParseGrammarError(this, "Reserved fields can only be numbers or quoted strings");
        }

        return block;
    }

    private ReservedBlock ParseRanges(Queue<Token> tokens)
    {
        var block = new ReservedBlock();
        
        var nextToken = tokens.Peek();
        while (nextToken.Value != ";")
        {
            var number = tokens.Dequeue();
            AssertType(TokenType.Number, number);
            var lookahead = tokens.Peek();
                
            switch (lookahead.Value)
            {
                case ",":
                    block.AddPositions(int.Parse(number.Value));
                    tokens.Dequeue();
                    break;
                case "to":
                {
                    tokens.Dequeue();
                    var nextNumber = tokens.Dequeue();
                    AssertType(TokenType.Number, nextNumber);
                    var range = new PositionRange(
                        int.Parse(number.Value),
                        int.Parse(nextNumber.Value));
                    
                    block.AddPositions(range);
                    break;
                }
            }

            if (!tokens.TryPeek(out nextToken))
                throw new ParseGrammarError(this, "Parse queue emptied before reaching terminator");
        }

        tokens.Dequeue(); // terminator
        
        return block;
    }

    private ReservedBlock ParseReservedFieldNames(Queue<Token> tokens)
    {
        var block = new ReservedBlock();
        var nextToken = tokens.Peek();

        while (nextToken.Value != ";")
        {
            if (nextToken.Value == ",")
            {
                tokens.Dequeue();
            }
            else
            {
                var fieldName = tokens.Dequeue();
                AssertType(TokenType.QuotedString, fieldName);
                block.AddFieldName(fieldName.Value);
            }

            if (!tokens.TryPeek(out nextToken))
                throw new ParseGrammarError(this, "Parse queue emptied before reaching terminator");
        }

        var terminator = tokens.Dequeue();
        AssertSymbol(';', terminator);
        
        return block;
    }
}