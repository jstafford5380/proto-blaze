namespace ProtoDocs.Parser.Parsing;

public abstract class FieldBlock : ParsedBlock
{
    public string Name { get; }
    
    public int Position { get; }

    public bool IsRepeated { get; }
    
    public string? FieldType { get; }

    protected FieldBlock(string name, string? type, bool isRepeated, int position)
        : base(BlockType.Field)
    {
        Name = name;
        FieldType = type;
        IsRepeated = isRepeated;
        Position = position;
    }
}