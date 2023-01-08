namespace ProtoDocs.Parser.Parsing.Grammar;

internal interface IBlockParser
{
    SyntaxBlock Parse(Queue<Token> tokens);
}