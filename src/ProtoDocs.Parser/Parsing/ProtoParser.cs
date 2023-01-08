using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;

namespace ProtoDocs.Parser.Parsing;

public class ProtoParser
{
    public IEnumerable<ParsedBlock> Parse(IList<Token> tokens)
    {
        var blocks = new List<ParsedBlock>();
        
        var queue = new Queue<Token>(tokens);

        try
        {
            while (queue.Any())
            {
                var nextToken = queue.Peek();
                var parser = BlockParserFactory.Get(nextToken.Type);
                var block = parser.Parse(queue);
            
                blocks.Add(block);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return blocks;
    }
}