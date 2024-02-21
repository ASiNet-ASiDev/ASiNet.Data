namespace ASiNet.Data.Serialization.Interfaces;
public interface ISerializeWriter
{
    public int TotalAreaSize { get; }
    public int AvalibleAreaSize { get; }
    public int FilledAreaSize { get; }

    public bool CanWriteSize(int size);

    public void WriteBytes(Span<byte> data);

    public void WriteByte(byte data);

}
