namespace ProtoDocs.Parser.Parsing;

public class SyntaxBlock : ParsedBlock
{
    public string Value { get; }
    
    public SyntaxBlock(string value) 
        : base(BlockType.Syntax)
    {
        Value = value;
    }
}