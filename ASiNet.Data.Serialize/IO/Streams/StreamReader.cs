using System.Net.Sockets;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Streams;

public class StreamReader(Stream c_stream) : ISerializeReader
{
    public int Length
    {
        get
        {
            if (_stream is FileStream fs) return (int)fs.Length;
            if (_stream is NetworkStream ns) return int.MaxValue;
            if (_stream is MemoryStream ms) return (int)ms.Length;

            throw new NotImplementedException();
        }
    }
    public int AvalibleBytes
    {
        get
        {
            if (_stream is FileStream fs) return (int)(fs.Length - fs.Position);
            if (_stream is NetworkStream ns) return ns.Socket.Available;
            if (_stream is MemoryStream ms) return (int)(ms.Length - ms.Position);

            throw new NotImplementedException();
        }
    }
    public int ReadedBytes
    {
        get
        {
            if (_stream is FileStream fs) return (int)fs.Position;
            if (_stream is NetworkStream ns) return 0;
            if (_stream is MemoryStream ms) return (int)ms.Position;

            throw new NotImplementedException();
        }
    }

    private Stream _stream = c_stream;

    public bool CanReadSize(int size)
    {
        if (_stream is FileStream fs) return fs.Length - fs.Position >= size;
        if (_stream is NetworkStream ns) return ns.Socket.Available >= size;
        if (_stream is MemoryStream ms) return ms.Length - ms.Position >= size;

        throw new NotImplementedException();
    }

    public void ReadBytes(Span<byte> data)
    {
        int bytes = _stream.Read(data);
        if (bytes < data.Length)
            throw new Exception("Invalid data.");
    }

    public byte ReadByte()
    {
        int res = _stream.ReadByte();
        if (res == -1)
            throw new Exception("Out of range.");
        return (byte)res;
    }
}