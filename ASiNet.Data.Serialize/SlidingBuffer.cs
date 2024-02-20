namespace ASiNet.Data;
public class SlidingBuffer(int size)
{
    public event Action<byte[]>? Overflow;

    public byte[] Buffer { get; init; } = new byte[size];

    public byte[] FillBytes => Buffer[.._position];

    private int _position = 0;

    public int EmptySize => Buffer.Length - _position;

    public void Write(Span<byte> src)
    {
        if(src.Length > EmptySize)
        {
            var w = src[0..EmptySize];
            w.CopyTo(Buffer);
            Overflow?.Invoke(Buffer);
            _position = 0;
            Write(src[w.Length..]);
        }
        else
        {
            src.CopyTo(Buffer.AsSpan()[_position..]);
            _position += src.Length;
        }
    }

}
