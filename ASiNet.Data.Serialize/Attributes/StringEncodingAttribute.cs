using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Attributes;
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class StringEncodingAttribute(EncodingType encoding = EncodingType.UTF8) : Attribute
{

    public EncodingType Encoding { get; set; } = encoding;

    internal Type GetEncodingType() => Encoding switch 
    { 
        EncodingType.UTF8 => typeof(UTF8String),
        EncodingType.Unicode => typeof(UnicodeString),
        EncodingType.UTF32 => typeof(UTF32String),
        EncodingType.ASCII => typeof(ASCIIString),
        EncodingType.Latian1 => typeof(Latin1String),
        _ => throw new NotSupportedException(),
    };
}
