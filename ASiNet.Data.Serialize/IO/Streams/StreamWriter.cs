using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Streams;

public class StreamWriter : ISerializeWriter
{
    public int Length { get; }
    public int AvalibleBytes { get; }
    public int FilledBytes { get; }
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