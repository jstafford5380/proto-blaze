namespace ProtoDocs.Parser.Parsing;

public class ImportBlock : ParsedBlock
{
    public string Value { get; }

    public ImportBlock(string value)
        : base(BlockType.Import)
    {
        Value = value;
    }
}