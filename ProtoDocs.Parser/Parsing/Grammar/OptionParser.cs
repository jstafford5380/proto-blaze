using ProtoDocs.Parser.LexicalAnalysis;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class OptionParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        string optionName;
        var optionNameToken = tokens.Dequeue();
        if (optionNameToken.Type is TokenType.Symbol && optionNameToken.Value == "(")
        {
            var innerName = tokens.Dequeue();
            AssertValue(Lexicon.FullIdentifier, innerName.Value);
            optionName = innerName.Value;

            var closeParen = tokens.Dequeue();
            AssertSymbol(')', closeParen);
        }
        else
        {
            AssertValue(Lexicon.Identifier, optionNameToken.Value, "Expected option name identifier after option keyword");
            optionName = optionNameToken.Value;
        }

        var equals = tokens.Dequeue();
        AssertValue("=", equals.Value, "Expected '=' after option name");
        
        var optionValue = tokens.Dequeue();
        if (optionValue.Type is not TokenType.QuotedString and not TokenType.Boolean)
            throw new ParseGrammarError(this, "Option values must be a boolean or quoted string");

        var terminator = tokens.Dequeue();
        AssertSymbol(';', terminator);
        
        return new OptionBlock(optionName, optionValue.Value);
    }
}