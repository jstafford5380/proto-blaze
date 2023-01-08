using System.Text.RegularExpressions;

namespace ProtoDocs.Parser.Parsing;

public partial class ProtobufType : IEquatable<ProtobufType>
{
    private readonly string _value;

    public static ProtobufType Double = new ("double");
    public static ProtobufType Float = new("float");
    public static ProtobufType Int32 = new("int32");
    public static ProtobufType Int64 = new("int64");
    public static ProtobufType UInt32 = new("uint32");
    public static ProtobufType UInt64 = new("uint64");
    public static ProtobufType SInt32 = new("sint32");
    public static ProtobufType SInt64 = new("sint64");
    public static ProtobufType Fixed32 = new("fixed32");
    public static ProtobufType Fixed64 = new("fixed64");
    public static ProtobufType SFixed32 = new("sfixed32");
    public static ProtobufType SFixed64 = new("sfixed64");
    public static ProtobufType Bool = new("bool");
    public static ProtobufType String = new("string");
    public static ProtobufType Bytes = new("bytes");

    public ProtobufType(string value)
    {
        if (!TypeRegex().IsMatch(value))
            throw new ArgumentException("Invalid type value", nameof(value));

        _value = value;
    }

    [GeneratedRegex(
        "^(double|float|int32|int64|uint32|uint64|sint32|sint64|fixed32|fixed64|sfixed32|sfixed64|bool|string|bytes)$")]
    private static partial Regex TypeRegex();

    public bool Equals(ProtobufType? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ProtobufType)obj);
    }

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() => _value;
}