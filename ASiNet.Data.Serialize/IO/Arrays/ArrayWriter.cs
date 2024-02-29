using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Arrays;
public class ArrayWriter(byte[] c_src) : ISerializeWriter
{
    public int Length => _src.Length;

    public int AvalibleBytes => _src.Length - _position;

    public int FilledBytes => _position;

    private int _position;
    private byte[] _src = c_src;

    public bool CanWriteSize(int size) =>
        AvalibleBytes >= size;

    public void WriteByte(byte data)
    {
        if (AvalibleBytes >= 1)
        {
            _src[_position] = data;
            _position++;
            return;
        }
        throw new IndexOutOfRangeException();
    }

    public void WriteBytes(Span<byte> data)
    {
        if (AvalibleBytes < data.Length)
            throw new IndexOutOfRangeException();
        data.CopyTo(_src.AsSpan().Slice(_position, data.Length));
        _position += data.Length;
    }

    public byte[] AsArray() => _src;



    public static implicit operator ArrayWriter(byte[] src) => new(src);

    public static implicit operator byte[](ArrayWriter src) => src._src;
}
