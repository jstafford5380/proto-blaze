namespace ProtoDocs.Parser.Parsing;

public class ReservedBlock : ParsedBlock
{
    private List<int> _positions = new();
    private List<string> _fieldNames = new();

    public IReadOnlyCollection<int> Positions => _positions;
    public IReadOnlyCollection<string> FieldNames => _fieldNames;

    public ReservedBlock() 
        : base(BlockType.Reserved)
    {
    }

    public void AddPositions(int pos) => _positions.Add(pos);
    public void AddPositions(PositionRange range) => _positions.AddRange(range.Expand());
    public void AddFieldName(string fieldName) => _fieldNames.Add(fieldName);
}