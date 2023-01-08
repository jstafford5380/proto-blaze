namespace ProtoDocs.Parser.Parsing;

public class EnumBlock : ParsedBlock
{
    public string Name { get; }

    public IEnumerable<OptionBlock> Options => Children
        .Where(c => c.Type is BlockType.Option)
        .Cast<OptionBlock>();

    public IEnumerable<EnumFieldBlock> Fields => Children
        .Where(c => c.Type is BlockType.EnumField)
        .Cast<EnumFieldBlock>();

    public EnumBlock(string name) 
        : base(BlockType.Enum)
    {
        Name = name;
    }
}