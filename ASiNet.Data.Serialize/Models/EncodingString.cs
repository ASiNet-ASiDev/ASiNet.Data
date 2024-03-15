namespace ASiNet.Data.Serialization.Models;

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
}


public class UTF8String : EncodingString
{
    public UTF8String() { }

    public UTF8String(string str) : base(str) { }
}

public class ASCIIString : EncodingString
{
    public ASCIIString() { }

    public ASCIIString(string str) : base(str) { }
}


public class UnicodeString : EncodingString
{
    public UnicodeString() { }

    public UnicodeString(string str) : base(str) { }
}

public class UTF32String : EncodingString
{
    public UTF32String() { }

    public UTF32String(string str) : base(str) { }
}

public class Latin1String : EncodingString
{
    public Latin1String() { }

    public Latin1String(string str) : base(str) { }
}

