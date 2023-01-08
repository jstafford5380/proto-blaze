namespace ProtoDocs.Parser.Parsing;

public readonly struct PositionRange
{
    public int From { get; }
    
    public int To { get; }

    public PositionRange(int from, int to)
    {
        From = from;
        To = to;
    }

    public int[] Expand()
    {
        var expanded = new List<int>();
        for (var i = From; i <= To; i++)
        {
            expanded.Add(i);
        }

        return expanded.ToArray();
    }
}