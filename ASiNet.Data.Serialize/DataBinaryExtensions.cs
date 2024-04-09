using System.Net.Sockets;
using System.Text;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Network;
using ASiNet.Data.Serialization.IO.Streams;

namespace ASiNet.Data.Serialization;

public static class DataBinaryExtensions
{

    public static int Serialize<T>(this IBinarySerializer serializer, T obj, FileStream stream)
        => serializer.Serialize<T>(obj, (FileStreamWriter)stream);

    public static T? Deserialize<T>(this IBinarySerializer serializer, FileStream stream)
        => serializer.Deserialize<T>((FileStreamReader)stream);

    public static int Serialize<T>(this IBinarySerializer serializer, T obj, MemoryStream stream)
        => serializer.Serialize<T>(obj, (IO.Streams.StreamWriter)stream);

    public static T? Deserialize<T>(this IBinarySerializer serializer, MemoryStream stream)
        => serializer.Deserialize<T>((IO.Streams.StreamReader)stream);

    public static int Serialize<T>(this IBinarySerializer serializer, T obj, NetworkStream stream)
        => serializer.Serialize<T>(obj, (NetworkStreamWriter)stream);

    public static T? Deserialize<T>(this IBinarySerializer serializer, NetworkStream stream)
        => serializer.Deserialize<T>((NetworkStreamReader)stream);


    public static bool TryToBytes(this sbyte src, Span<byte> buffer)
    {
        if (buffer.Length == 0)
            return false;
        buffer[0] = (byte)src;
        return true;
    }

    public static bool TryToBytes(this byte src, Span<byte> buffer)
    {
        if (buffer.Length == 0)
            return false;
        buffer[0] = src;
        return true;
    }

    public static bool TryToBytes(this string src, Span<byte> buffer) =>
        Encoding.UTF8.TryGetBytes(src, buffer, out var bytesWritten);

    public static bool TryToBytes(this ReadOnlySpan<char> src, Span<byte> buffer) =>
        Encoding.UTF8.TryGetBytes(src, buffer, out var bytesWritten);


    public static bool TryToBytes(this bool src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this char src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this short src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this ushort src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this int src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this uint src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this long src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this ulong src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this float src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);

    public static bool TryToBytes(this double src, Span<byte> buffer) =>
        BitConverter.TryWriteBytes(buffer, src);
}
