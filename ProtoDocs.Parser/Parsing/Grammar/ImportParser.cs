using ProtoDocs.Parser.LexicalAnalysis;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class ImportParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("import", keyword.Value);

        var path = tokens.Dequeue();
        AssertValue(Lexicon.QuotedPath, $"\"{path.Value}\"", "Expected import path after import keyword");

        var terminator = tokens.Dequeue();
        AssertValue(";", terminator.Value, "Expected terminator after closing quote");

        return new ImportBlock(path.Value);
    }
}