using System.Net.Sockets;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Network;
public class NetworkStreamWriter(NetworkStream stream) : ISerializeWriter
{

    private NetworkStream _stream = stream;

    public int Length => _stream.CanWrite ? int.MaxValue : 0;

    public int AvalibleBytes => _stream.CanWrite ? int.MaxValue : 0;

    public int FilledBytes => _writedBytes;

    private int _writedBytes;

    public bool CanWriteSize(int size) => _stream.CanWrite;

    public void WriteByte(byte data)
    {
        _stream.WriteByte(data);
        _writedBytes++;
    }

    public void WriteBytes(Span<byte> data)
    {
        _stream.Write(data);
        _writedBytes += data.Length;
    }

    public static implicit operator NetworkStreamWriter(NetworkStream src) => new(src);

    public static implicit operator NetworkStream(NetworkStreamWriter src) => src._stream;
}
