namespace ProtoDocs.Parser.Parsing.Grammar;

internal class OptionBlockParser : OptionParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("option", keyword.Value);
        
        var parsed = base.Parse(tokens);
        
        return parsed;
    }
}