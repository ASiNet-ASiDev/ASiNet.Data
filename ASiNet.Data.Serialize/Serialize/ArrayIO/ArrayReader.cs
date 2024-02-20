using System.Runtime.Serialization;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Writers;

public class ArrayReader(byte[] c_src) : ISerializeReader
{
    public int TotalAreaSize { get; }
    public int AvalibleAreaSize { get; }
    public int FilledAreaSize { get; }

    private int _position;
    private byte[] _src = c_src;
    
    public bool CanReadSize(int size)
        => FilledAreaSize-_position >= size;

    public void ReadBytes(Span<byte> data)
    {
        if(!CanReadSize(data.Length))
            throw new InvalidDataException();
        
        _src.AsSpan().Slice(_position, data.Length).CopyTo(data);
    }

    public byte ReadByte()
    {
        if(!CanReadSize(1))
            throw new InvalidDataException();
        
        return _src[_position++];
    }
}