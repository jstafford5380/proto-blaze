using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class SyntaxParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("syntax", keyword.Value);
        
        var equals = tokens.Dequeue();
        AssertValue("=", equals.Value, "Expected '=' after syntax keyword");

        var syntaxValue = tokens.Dequeue();
        AssertType(TokenType.QuotedString, syntaxValue);
        
        var terminator = tokens.Dequeue();
        AssertValue(";", terminator.Value, "Expected terminator after closing quote.");
        
        return new SyntaxBlock(syntaxValue.Value);
    }
}