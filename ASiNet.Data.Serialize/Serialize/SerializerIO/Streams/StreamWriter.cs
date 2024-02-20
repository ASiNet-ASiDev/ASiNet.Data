using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.SerializerIO.Streams;

public class StreamWriter : ISerializeWriter
{
    public int TotalAreaSize { get; }
    public int AvalibleAreaSize { get; }
    public int FilledAreaSize { get; }
    public bool CanWriteSize(int size)
    {
        throw new NotImplementedException();
    }

    public void WriteBytes(Span<byte> data)
    {
        throw new NotImplementedException();
    }

    public void WriteByte(byte data)
    {
        throw new NotImplementedException();
    }
}