using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;
using Lexicon = ProtoDocs.Parser.Lexing.Lexicon;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class PackageParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("package", keyword.Value);

        var packageName = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, packageName.Value, "Expected package identifier after package keyword");

        var terminator = tokens.Dequeue();
        AssertValue(";", terminator.Value, "Expected terminator after package identifier");

        return new PackageBlock(packageName.Value);
    }
}