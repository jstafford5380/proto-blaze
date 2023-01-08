using ProtoDocs.Parser.Parsing.Grammar;

namespace ProtoDocs.Parser.Parsing;

internal class ParseGrammarError : Exception
{
    public ParseGrammarError(string parserType, string message)
        : base($"{parserType}: {message}")
    {
    }
    
    public ParseGrammarError(IBlockParser parser, string message)
        : this(parser.GetType().Name, message)
    {
    }
}