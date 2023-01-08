namespace ProtoDocs.Parser.Parsing.Grammar;

internal class ParseGrammarError : Exception
{
    public ParseGrammarError(IBlockParser parser, string message)
        : base($"{parser.GetType().Name}: {message}")
    {
    }
}