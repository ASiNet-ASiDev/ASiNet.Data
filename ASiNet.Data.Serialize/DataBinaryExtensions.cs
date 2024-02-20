using System.Text;

namespace ASiNet.Data;

public static class DataBinaryExtensions
{
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
