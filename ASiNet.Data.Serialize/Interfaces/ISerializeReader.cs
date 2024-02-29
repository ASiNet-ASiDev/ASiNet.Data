namespace ASiNet.Data.Serialization.Interfaces;

/// <summary>
/// Common reading interface for the serializer
/// </summary>
public interface ISerializeReader
{
    /// <summary>
    /// The total length available for reading
    /// </summary>
    public int Length { get; }
    /// <summary>
    /// The number of bytes available for reading
    /// </summary>
    public int AvalibleBytes { get; }
    /// <summary>
    /// The number of bytes already read
    /// </summary>
    public int ReadedBytes { get; }

    /// <summary>
    /// Is it possible to count the specified number of bytes
    /// </summary>
    public bool CanReadSize(int size);

    /// <summary>
    /// Read data to the buffer
    /// </summary>
    /// <param name="data"> buffer </param>
    public void ReadBytes(Span<byte> data);

    /// <summary>
    /// Read next byte
    /// </summary>
    /// <returns></returns>
    public byte ReadByte();
}
