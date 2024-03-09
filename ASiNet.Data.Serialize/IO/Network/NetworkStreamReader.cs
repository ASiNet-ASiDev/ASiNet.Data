using System.Net.Sockets;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.IO.Network;
public class NetworkStreamReader(NetworkStream stream) : ISerializeReader
{
    private NetworkStream _stream = stream;

    public int MaxAttemptsCount { get; set; } = 15;

    public int DelayBetweenAttempts { get; set; } = 100;

    public int Length => _stream.CanRead ? int.MaxValue : 0;

    public int AvalibleBytes => _stream.CanRead ? _stream.Socket.Available : 0;

    public int ReadedBytes => _readedBytes;

    private int _readedBytes;

    public bool CanReadSize(int size)
    {
        if (!_stream.CanRead)
            return false;
        var attemptsCount = 0;
        while (attemptsCount < MaxAttemptsCount)
        {
            if (_stream.Socket.Available < size)
            {
                Task.Delay(DelayBetweenAttempts).Wait();
                attemptsCount++;
                continue;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public byte ReadByte()
    {
        var attemptsCount = 0;
        while (attemptsCount < MaxAttemptsCount)
        {
            if (_stream.Socket.Available < 1)
            {
                Task.Delay(DelayBetweenAttempts).Wait();
                attemptsCount++;
                continue;
            }
            else
            {
                _readedBytes++;
                return (byte)_stream.ReadByte();
            }
        }
        throw new ReaderException(new Exception($"It was not possible to read[0] the entire object[1] in [{attemptsCount}] attempts!"));
    }

    public void ReadBytes(Span<byte> data)
    {
        var attemptsCount = 0;
        while (attemptsCount < MaxAttemptsCount)
        {
            if (_stream.Socket.Available < data.Length)
            {
                Task.Delay(DelayBetweenAttempts).Wait();
                attemptsCount++;
                continue;
            }
            else
            {
                _stream.Read(data);
                _readedBytes += data.Length;
                return;
            }
        }
        throw new ReaderException(new Exception($"It was not possible to read[{_stream.Socket.Available}] the entire object[{data.Length}] in [{attemptsCount}] attempts!"));
    }


    public static implicit operator NetworkStreamReader(NetworkStream src) => new(src);

    public static implicit operator NetworkStream(NetworkStreamReader src) => src._stream;
}
