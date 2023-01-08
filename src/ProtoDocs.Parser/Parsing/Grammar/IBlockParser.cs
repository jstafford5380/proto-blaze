using System.Text.RegularExpressions;
using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal interface IBlockParser
{
    ParsedBlock Parse(Queue<Token> tokens);
}

internal abstract class BlockParser : IBlockParser
{
    public abstract ParsedBlock Parse(Queue<Token> tokens);

    protected void AssertValue(string expected, string actual, string? message = null)
    {
        if (actual != expected)
            throw new ParseGrammarError(this, $"{message ?? $"Expected {expected} but got {actual}"}");
    }

    protected void AssertSymbol(char symbol, Token token, string? message = null)
    {
        if (token.Type is not TokenType.Symbol)
            throw new ParseGrammarError(this, message ?? "Expected a symbol");

        if (token.Value.Length != 1 || token.Value[0] != symbol)
            throw new ParseGrammarError(this, message ?? $"Expected symbol '{symbol}' but got '{token.Value}'");
    }

    protected void AssertValue(Regex pattern, string actual, string? message = null)
    {
        if (!pattern.IsMatch(actual))
            throw new ParseGrammarError(this, $"{message ?? $"Expected match {pattern} but it did not match"}");
    }

    protected void AssertType(TokenType type, Token token)
    {
        if (token.Type != type)
            throw new ParseGrammarError(this, $"Expected token type '{type}' but got '{token.Type}'");
    }

    protected void AssertTrue(bool condition, string message)
    {
        if (!condition)
            throw new ParseGrammarError(this, message);
    }
}