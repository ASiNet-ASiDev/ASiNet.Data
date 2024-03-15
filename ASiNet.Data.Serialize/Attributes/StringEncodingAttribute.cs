using System.Text;

namespace ASiNet.Data.Serialization.Attributes;
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class StringEncodingAttribute(EncodingType encoding = EncodingType.UTF8) : Attribute
{
    public EncodingType Encoding { get; set; } = encoding;

    internal Encoding GetEncoding() => Encoding switch
    {
        EncodingType.UTF8 => System.Text.Encoding.UTF8,
        EncodingType.Unicode => System.Text.Encoding.Unicode,
        EncodingType.UTF32 => System.Text.Encoding.UTF32,
        EncodingType.ASCII => System.Text.Encoding.ASCII,
        EncodingType.Latian1 => System.Text.Encoding.Latin1,
        _ => throw new NotSupportedException(),
    };
}


public enum EncodingType : byte
{
    UTF8, ASCII, Unicode, UTF32, Latian1,
}
