using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.ArrayIO;
public class ArrayWriter(byte[] c_src) : ISerializerWriter
{
    public int TotalAreaSize => _src.Length;

    public int AvalibleAreaSize => _src.Length - _position;

    public int FilledAreaSize => _position;

    private int _position;
    private byte[] _src = c_src;

    public bool CanWriteSize(int size) => 
        AvalibleAreaSize >= size;
    
    public void WriteByte(byte data)
    {
        if(AvalibleAreaSize >= 1)
        {
            _src[_position] = data;
            _position++;
            return;
        }
        throw new IndexOutOfRangeException();
    }

    public void WriteBytes(Span<byte> data)
    {
        if(AvalibleAreaSize < data.Length)
            throw new IndexOutOfRangeException();
        data.CopyTo(_src.AsSpan().Slice(_position, data.Length));
    }

    public byte[] AsArray() => _src;



    public static implicit operator ArrayWriter(byte[] src) => new(src);

    public static implicit operator byte[](ArrayWriter src) => src._src;
}
