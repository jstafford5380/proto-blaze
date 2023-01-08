namespace ProtoDocs.Parser.Parsing;

public class EnumFieldBlock : ParsedBlock
{
    public string Name { get; }
    
    public int Value { get; }

    public IEnumerable<OptionBlock> Options => Children
        .Where(c => c.Type is BlockType.Option)
        .Cast<OptionBlock>();
    
    public EnumFieldBlock(string name, int value) 
        : base(BlockType.EnumField)
    {
        Name = name;
        Value = value;
    }
}