using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Arrays;

public class ArrayReader(byte[] c_src) : ISerializeReader
{
    public int TotalAreaSize => _src.Length;
    public int AvalibleAreaSize => 0;
    public int FilledAreaSize => _src.Length;

    private int _position;
    private byte[] _src = c_src;

    public bool CanReadSize(int size)
        => FilledAreaSize - _position >= size;

    public void ReadBytes(Span<byte> data)
    {
        if (!CanReadSize(data.Length))
            throw new IndexOutOfRangeException();

        _src.AsSpan().Slice(_position, data.Length).CopyTo(data);
        _position += data.Length;
    }

    public byte ReadByte()
    {
        if (!CanReadSize(1))
            throw new IndexOutOfRangeException();

        return _src[_position++];
    }


    public static implicit operator ArrayReader(byte[] src) => new(src);

    public static implicit operator byte[](ArrayReader src) => src._src;
}