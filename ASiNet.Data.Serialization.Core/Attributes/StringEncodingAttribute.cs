using System;
using System.Text;

namespace ASiNet.Data.Serialization.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class StringEncodingAttribute : Attribute
    {
        public StringEncodingAttribute(EncodingType encoding = EncodingType.UTF8)
        {
            Encoding = encoding;
        }

        public EncodingType Encoding { get; set; }

        public Encoding GetEncoding()
        {
            switch (Encoding)
            {
                case EncodingType.UTF8:
                    return System.Text.Encoding.UTF8;
                case EncodingType.ASCII:
                    return System.Text.Encoding.ASCII;
                case EncodingType.Unicode:
                    return System.Text.Encoding.Unicode;
                case EncodingType.UTF32:
                    return System.Text.Encoding.UTF32;
                case EncodingType.UTF7:
                    return System.Text.Encoding.UTF7;
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public enum EncodingType : byte
    {
        UTF7, UTF8, ASCII, Unicode, UTF32,
    }

}