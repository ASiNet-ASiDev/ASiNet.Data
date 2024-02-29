namespace ASiNet.Data.Serialization.Interfaces;

/// <summary>
/// Common recording interface for the serializer
/// </summary>
public interface ISerializeWriter
{
    /// <summary>
    /// The total length available for recording
    /// </summary>
    public int Length { get; }
    /// <summary>
    /// The number of bytes available for writing
    /// </summary>
    public int AvalibleBytes { get; }
    /// <summary>
    /// The number of bytes already written
    /// </summary>
    public int FilledBytes { get; }

    /// <summary>
    /// Is it possible to count bytes
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public bool CanWriteSize(int size);

    /// <summary>
    /// write bytes
    /// </summary>
    /// <param name="data"></param>
    public void WriteBytes(Span<byte> data);

    /// <summary>
    /// write byte
    /// </summary>
    /// <param name="data"></param>
    public void WriteByte(byte data);

}
