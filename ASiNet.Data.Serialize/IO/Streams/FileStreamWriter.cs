using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Streams;

public class FileStreamWriter(Stream stream) : ISerializeWriter
{
    public int Length => (int)_stream.Length;
    public int AvalibleBytes => (int)(_stream.Length - _stream.Position);
    public int FilledBytes => (int)_stream.Position;

    private Stream _stream = stream;

    public bool CanWriteSize(int size) => _stream.CanWrite && AvalibleBytes >= size;

    public void WriteBytes(Span<byte> data)
    {
        if ((!_stream.CanSeek || !_stream.CanWrite) && AvalibleBytes < data.Length)
            throw new ReaderException(new IndexOutOfRangeException("The length of the stream is less than what you are trying to read"));
        _stream.Write(data);
    }

    public void WriteByte(byte data)
    {
        if ((!_stream.CanSeek || !_stream.CanWrite) && AvalibleBytes < 1)
            throw new ReaderException(new IndexOutOfRangeException("The length of the stream is less than what you are trying to read"));
        _stream.WriteByte(data);
    }

    public static implicit operator FileStreamWriter(FileStream src) => new(src);

    public static implicit operator Stream(FileStreamWriter src) => src._stream;
}