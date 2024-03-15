using System.Text;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;
public class StringModel<T>(Encoding encoding) : SerializeModelBase<T> where T : EncodingString, new()
{
    private readonly Encoding _encoding = encoding;
    public override T? Deserialize(in ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        if (reader.CanReadSize(sizeof(int) + 1))
        {
            var sizeBuffer = (stackalloc byte[sizeof(int)]);
            reader.ReadBytes(sizeBuffer);
            var strBytesSize = BitConverter.ToInt32(sizeBuffer);
            if (reader.CanReadSize(strBytesSize))
            {
                var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
                reader.ReadBytes(buffer);
                var res = new T();
                res.Value = _encoding.GetString(buffer);

                return res;
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader) =>
        Deserialize(reader);

    public override void Serialize(T? obj, in ISerializeWriter writer)
    {
        if (obj is null)
        {
            writer.WriteByte(0);
            return;
        }
        else
            writer.WriteByte(1);

        var strBytesSize = _encoding.GetByteCount(obj.Value);
        var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
        if (obj.Value.TryToBytes(buffer))
        {
            var strSzBytes = (stackalloc byte[sizeof(int)]);
            BitConverter.TryWriteBytes(strSzBytes, strBytesSize);
            writer.WriteBytes(strSzBytes);
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer) =>
        Serialize((T?)obj, writer);

    public override int ObjectSerializedSize(T? obj)
    {
        if (obj is null)
            return 1;
        return  1 + 4 + _encoding.GetByteCount(obj.Value);
    }

}

public class DefaultStringModel(Encoding encoding) : SerializeModelBase<string>
{
    private readonly Encoding _encoding = encoding;
    public override string? Deserialize(in ISerializeReader reader)
    {
        if (reader.ReadByte() == 0)
            return null;

        if (reader.CanReadSize(sizeof(int) + 1))
        {
            var sizeBuffer = (stackalloc byte[sizeof(int)]);
            reader.ReadBytes(sizeBuffer);
            var strBytesSize = BitConverter.ToInt32(sizeBuffer);
            if (reader.CanReadSize(strBytesSize))
            {
                var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
                reader.ReadBytes(buffer);

                return _encoding.GetString(buffer);
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader) =>
        Deserialize(reader);

    public override void Serialize(string? obj, in ISerializeWriter writer)
    {
        if (obj is null)
        {
            writer.WriteByte(0);
            return;
        }
        else
            writer.WriteByte(1);

        var strBytesSize = _encoding.GetByteCount(obj);
        var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
        if (obj.TryToBytes(buffer))
        {
            var strSzBytes = (stackalloc byte[sizeof(int)]);
            BitConverter.TryWriteBytes(strSzBytes, strBytesSize);
            writer.WriteBytes(strSzBytes);
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer) =>
        Serialize((string?)obj, writer);

    public override int ObjectSerializedSize(string? obj)
    {
        if (obj is null)
            return 1;
        return 1 + 4 + _encoding.GetByteCount(obj);
    }

}

