using ProtoDocs.Parser.LexicalAnalysis;
using ProtoDocs.Parser.Lexing;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal abstract class FieldBlockParser : BlockParser
{
    protected void TryAddOptions(FieldBlock fieldBlock, Queue<Token> tokens)
        => TryAdd(fieldBlock, tokens);

    protected void TryAddOptions(EnumFieldBlock enumField, Queue<Token> tokens)
        => TryAdd(enumField, tokens);

    private void TryAdd(ParsedBlock block, Queue<Token> tokens)
    {
        var optionsOrTerminator = tokens.Dequeue();
        if (optionsOrTerminator.Value == ";")
            return;
        
        AssertSymbol('[', optionsOrTerminator, "Only options may follow a field");
        
        var optionParser = new OptionParser(); 
        var optionCurrent = tokens.Peek();
        while (optionCurrent.Value != "]")
        {
            if (optionCurrent.Value == ",")
            {
                tokens.Dequeue();
                optionCurrent = tokens.Peek();
                continue;
            }
                
            var option = optionParser.Parse(tokens);
            block.AddChild(option);
            optionCurrent = tokens.Peek();
        }

        var closeBrace = tokens.Dequeue();
        AssertSymbol(']', closeBrace);
        
        var terminator = tokens.Dequeue();
        AssertSymbol(';', terminator, "Expected terminator after field options");
    }
}