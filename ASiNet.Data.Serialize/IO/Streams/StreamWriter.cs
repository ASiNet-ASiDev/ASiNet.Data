﻿using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Streams;
public class StreamWriter(Stream stream) : ISerializeWriter
{
    public int Length => (int)_stream.Length;
    public int AvalibleBytes => (int)(_stream.Length - _stream.Position);
    public int FilledBytes => (int)_stream.Position;

    private Stream _stream = stream;

    public bool CanWriteSize(int size) => _stream.CanWrite && AvalibleBytes >= size;

    public void WriteBytes(Span<byte> data)
    {
        _stream.Write(data);
    }

    public void WriteByte(byte data)
    {
        _stream.WriteByte(data);
    }

    public static implicit operator StreamWriter(Stream src) => new(src);

    public static implicit operator Stream(StreamWriter src) => src._stream;
}
