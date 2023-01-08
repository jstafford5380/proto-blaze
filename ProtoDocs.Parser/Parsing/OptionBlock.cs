namespace ProtoDocs.Parser.Parsing;

public class OptionBlock : ParsedBlock
{
    public string Name { get; }
    
    public string Value { get; }

    public OptionBlock(string name, string value)
        : base(BlockType.Option)
    {
        Name = name;
        Value = value;
    }
}