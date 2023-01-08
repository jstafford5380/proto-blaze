namespace ProtoDocs.Parser.Parsing;

public class PackageBlock : ParsedBlock
{
    public string Value { get; }

    public PackageBlock(string value)
        : base(BlockType.Package)
    {
        Value = value;
    }
}