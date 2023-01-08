namespace ProtoDocs.Parser.Parsing;

public class MessageBlock : ParsedBlock
{
    public string Name { get; }
    
    public IEnumerable<NormalFieldBlock> Fields => Get<NormalFieldBlock>(BlockType.Field);
    public IEnumerable<OptionBlock> Options => Get<OptionBlock>(BlockType.Option);
    public IEnumerable<EnumBlock> Enums => Get<EnumBlock>(BlockType.Enum);
    public IEnumerable<MessageBlock> Messages => Get<MessageBlock>(BlockType.Message);
    public IEnumerable<ReservedBlock> Reserved => Get<ReservedBlock>(BlockType.Reserved);

    public MessageBlock(string name) 
        : base(BlockType.Message)
    {
        Name = name;
    }

    public override void AddChild(ParsedBlock block)
    {
        if (block is not NormalFieldBlock
            and not EnumBlock
            and not MessageBlock
            and not OptionBlock
            and not OneOfFieldBlock
            and not MapFieldBlock
            and not ReservedBlock)
        {
            throw new ArgumentException($"Invalid child block '{block.GetType().Name}'", nameof(block));
        }
        
        base.AddChild(block);
    }
}