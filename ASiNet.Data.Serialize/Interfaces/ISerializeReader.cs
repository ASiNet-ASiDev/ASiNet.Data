namespace ASiNet.Data.Serialization.Interfaces;
public interface ISerializeReader
{
    public int TotalAreaSize { get; }
    public int AvalibleAreaSize { get; }
    public int FilledAreaSize { get; }

    public bool CanReadSize(int size);

    public void ReadBytes(Span<byte> data);

    public byte ReadByte();
}
