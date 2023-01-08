namespace ProtoDocs.Parser.Parsing;

public class ServiceBlock : ParsedBlock
{
    private readonly List<RpcBlock> _rpcs = new();
    private readonly List<OptionBlock> _options = new();
    
    public string Name { get; }

    public IReadOnlyCollection<RpcBlock> Rpcs => _rpcs;

    public IReadOnlyCollection<OptionBlock> Options => _options;

    public ServiceBlock(string name) 
        : base(BlockType.Service)
    {
        Name = name;
    }

    public void AddRpc(RpcBlock rpc)
        => _rpcs.Add(rpc);

    public void AddOption(OptionBlock option) 
        => _options.Add(option);
}