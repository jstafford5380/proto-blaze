namespace ProtoDocs.Parser.Parsing;

public class OneOfFieldBlock : FieldBlock
{
    public IEnumerable<NormalFieldBlock> Fields => Children
        .Where(c => c.Type is BlockType.Field)
        .Cast<NormalFieldBlock>();
    
    public IEnumerable<OptionBlock> Options => Children
        .Where(c => c.Type is BlockType.Option)
        .Cast<OptionBlock>();

    public OneOfFieldBlock(string name)
        : base(name, null, false, -1)
    {
    }

    public override void AddChild(ParsedBlock block)
    {
        if (block.Type is not BlockType.Field and not BlockType.Option)
            throw new ArgumentException("Only fields and options are supported as children of Oneof fields");
        
        base.AddChild(block);
    }
}