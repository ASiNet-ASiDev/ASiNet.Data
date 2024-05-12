using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Streams;

public class FileStreamReader(Stream stream) : ISerializeReader
{
    public int Length => (int)_stream.Length;
    public int AvalibleBytes => (int)(_stream.Length - _stream.Position);
    public int ReadedBytes => (int)_stream.Position;

    private Stream _stream = stream;

    public bool CanReadSize(int size) => AvalibleBytes >= size;

    public void ReadBytes(Span<byte> data)
    {
        _stream.Read(data);
    }

    public byte ReadByte()
    {
        int res = _stream.ReadByte();
        if (res == -1)
            throw new Exception("Out of range.");
        return (byte)res;
    }

    public static implicit operator FileStreamReader(FileStream src) => new(src);

    public static implicit operator Stream(FileStreamReader src) => src._stream;
}