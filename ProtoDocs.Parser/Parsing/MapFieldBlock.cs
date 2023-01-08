namespace ProtoDocs.Parser.Parsing;

public class MapFieldBlock : FieldBlock
{
    public ProtobufType KeyType { get; }

    public string ValueType { get; }
    
    public MapFieldBlock(string name, int position, ProtobufType keyType, string valueType) 
        : base(name, null, false, position)
    {
        KeyType = keyType;
        ValueType = valueType;
    }
}