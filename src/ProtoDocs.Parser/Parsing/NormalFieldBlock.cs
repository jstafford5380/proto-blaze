namespace ProtoDocs.Parser.Parsing;

public class NormalFieldBlock : FieldBlock
{
    public IEnumerable<OptionBlock> Options => Children
        .Where(c => c.Type == BlockType.Option)
        .Cast<OptionBlock>();

    public NormalFieldBlock(string name, string? type, bool isRepeated, int position)
        : base(name, type, isRepeated, position)
    {
    }

    public override void AddChild(ParsedBlock block)
    {
        if (block.Type is not BlockType.Option)
            throw new ArgumentException("Only options are supported as children of fields");
        
        base.AddChild(block);
    }
}