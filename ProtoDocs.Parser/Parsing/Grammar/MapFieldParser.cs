using System.Text.RegularExpressions;
using ProtoDocs.Parser.LexicalAnalysis;

namespace ProtoDocs.Parser.Parsing.Grammar;

internal partial class MapFieldParser : FieldBlockParser
{
    public override ParsedBlock Parse(Queue<Token> tokens)
    {
        var map = tokens.Dequeue();
        var mapPattern = MapRegex();
        AssertValue(mapPattern, map.Value);
        
        var match = mapPattern.Match(map.Value);
        var keyType = match.Groups["keytype"].Value;
        var valueType = match.Groups["valuetype"].Value;

        var name = tokens.Dequeue();
        AssertType(TokenType.Identifier, name);
        AssertValue(Lexicon.Identifier, name.Value);

        var equals = tokens.Dequeue();
        AssertSymbol('=', equals);

        var position = tokens.Dequeue();
        AssertType(TokenType.Number, position);

        var mapFieldBlock = new MapFieldBlock(
            name.Value, 
            int.Parse(position.Value),
            new ProtobufType(keyType),
            valueType);
        
        TryAddOptions(mapFieldBlock, tokens);

        return mapFieldBlock;
    }

    [GeneratedRegex("^map\\<(?<keytype>(double|float|int32|int64|uint32|uint64|sint32|sint64|fixed32|fixed64|sfixed32|sfixed64|bool|string|bytes))\\s*,\\s*(?<valuetype>[A-Za-z_][A-Za-z0-9_]*)\\>$")]
    private static partial Regex MapRegex();
}