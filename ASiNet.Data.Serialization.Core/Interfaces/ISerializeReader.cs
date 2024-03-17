using System;

namespace ASiNet.Data.Serialization.Interfaces
{
    /// <summary>
    /// Common reading interface for the serializer
    /// </summary>
    public interface ISerializeReader
    {
        /// <summary>
        /// The total length available for reading
        /// </summary>
        int Length { get; }
        /// <summary>
        /// The number of bytes available for reading
        /// </summary>
        int AvalibleBytes { get; }
        /// <summary>
        /// The number of bytes already read
        /// </summary>
        int ReadedBytes { get; }

        /// <summary>
        /// Is it possible to count the specified number of bytes
        /// </summary>
        bool CanReadSize(int size);

        /// <summary>
        /// Read data to the buffer
        /// </summary>
        /// <param name="data"> buffer </param>
        void ReadBytes(Span<byte> data);

        /// <summary>
        /// Read next byte
        /// </summary>
        /// <returns></returns>
        byte ReadByte();
    }
}

