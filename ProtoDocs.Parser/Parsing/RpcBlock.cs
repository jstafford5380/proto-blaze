namespace ProtoDocs.Parser.Parsing;

public class RpcBlock : ParsedBlock
{
    public string Name { get; }
    
    public string RequestType { get; }
    
    public string ResponseType { get; }

    public bool RequestIsStream { get; }

    public bool ResponseIsStream { get; }

    public RpcBlock(string name, string requestType, bool requestIsStream, string responseType, bool responseIsStream) 
        : base(BlockType.Rpc)
    {
        Name = name;
        RequestType = requestType;
        RequestIsStream = requestIsStream;
        ResponseType = responseType;
        ResponseIsStream = responseIsStream;
    }
}