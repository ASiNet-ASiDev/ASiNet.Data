namespace ASiNet.Data.Serialization.Models;

public enum EncodingType : byte
{
    UTF8, ASCII, Unicode, UTF32, Latian1,
}

public abstract class EncodingString
{
    public EncodingString()
    {
        
    }

    public EncodingString(string val)
    {
        Value = val;
    }

    public string Value { get; set; } = null!;

    public abstract EncodingType Type { get; }
}


public class UTF8String : EncodingString
{
    public UTF8String() { }

    public UTF8String(string str) : base(str) { }

    public override EncodingType Type => EncodingType.UTF8;
}

public class ASCIIString : EncodingString
{
    public ASCIIString() { }

    public ASCIIString(string str) : base(str) { }

    public override EncodingType Type => EncodingType.ASCII;
}


public class UnicodeString : EncodingString
{
    public UnicodeString() { }

    public UnicodeString(string str) : base(str) { }

    public override EncodingType Type => EncodingType.Unicode;
}

public class UTF32String : EncodingString
{
    public UTF32String() { }

    public UTF32String(string str) : base(str) { }

    public override EncodingType Type => EncodingType.UTF32;
}

public class Latin1String : EncodingString
{
    public Latin1String() { }

    public Latin1String(string str) : base(str) { }

    public override EncodingType Type => EncodingType.Latian1;
}

