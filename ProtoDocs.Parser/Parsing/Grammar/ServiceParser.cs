using ProtoDocs.Parser.LexicalAnalysis;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal class ServiceParser : BlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var keyword = tokens.Dequeue();
        AssertValue("service", keyword.Value);

        var serviceName = tokens.Dequeue();
        AssertValue(Lexicon.Identifier, serviceName.Value, $"Invalid message identifier '{serviceName.Value}'");
        
        var openBrace = tokens.Dequeue();
        AssertValue("{", openBrace.Value);

        var service = new ServiceBlock(serviceName.Value);
        
        var nextToken = tokens.Peek();
        if (nextToken.Type is not TokenType.Rpc and not TokenType.Option)
            throw new ParseGrammarError(this, "Expected rpc or option in service definition");
         
        while(nextToken.Value != "}")
        {
            var parser = BlockParserFactory.Get(nextToken.Type);
            var block = parser.Parse(tokens);
            
            switch (block)
            {
                case RpcBlock rpc:
                    service.AddRpc(rpc);
                    break;
                case OptionBlock option:
                    service.AddOption(option);
                    break;
            }

            nextToken = tokens.Peek();
        }

        var _ = tokens.Dequeue();
        
        return service;
    }
}