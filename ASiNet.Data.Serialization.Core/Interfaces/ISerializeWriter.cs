using System;

namespace ASiNet.Data.Serialization.Interfaces
{
    /// <summary>
    /// Common recording interface for the serializer
    /// </summary>
    public interface ISerializeWriter
    {
        /// <summary>
        /// The total length available for recording
        /// </summary>
        int Length { get; }
        /// <summary>
        /// The number of bytes available for writing
        /// </summary>
        int AvalibleBytes { get; }
        /// <summary>
        /// The number of bytes already written
        /// </summary>
        int FilledBytes { get; }

        /// <summary>
        /// Is it possible to count bytes
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        bool CanWriteSize(int size);

        /// <summary>
        /// write bytes
        /// </summary>
        /// <param name="data"></param>
        void WriteBytes(Span<byte> data);

        /// <summary>
        /// write byte
        /// </summary>
        /// <param name="data"></param>
        void WriteByte(byte data);

    }
}
