namespace ProtoDocs.Parser.Parsing;

public abstract class ParsedBlock : IParsedBlock
{
    private readonly List<ParsedBlock> _children = new();
    public IReadOnlyCollection<ParsedBlock> Children => _children;

    public BlockType Type { get; }
    
    protected ParsedBlock(BlockType type)
    {
        Type = type;
    }

    public virtual void AddChild(ParsedBlock block) => _children.Add(block);

    protected IEnumerable<T> Get<T>(BlockType type) where T : ParsedBlock
        => _children
            .Where(c => c.Type == type)
            .Cast<T>();
}