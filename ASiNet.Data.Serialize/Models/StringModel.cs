using System.Text;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;

public class StringModel(Encoding encoding) : SerializeModelBase<string>, ISerializeStringModel
{
    private readonly Encoding _encoding = encoding;
    public override string? Deserialize(in ISerializeReader reader) =>
        Deserialize(reader, _encoding);

    public override object? DeserializeToObject(in ISerializeReader reader) =>
        Deserialize(reader);

    public override void SerializeObject(object? obj, in ISerializeWriter writer) =>
        Serialize((string?)obj, writer);

    public override void Serialize(string? obj, in ISerializeWriter writer) =>
        Serialize(obj, writer, _encoding);

    public void Serialize(string? obj, in ISerializeWriter writer, Encoding encoding)
    {
        if (obj is null)
        {
            writer.WriteByte(0);
            return;
        }
        else
            writer.WriteByte(1);

        var strBytesSize = encoding.GetByteCount(obj);
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

    public string? Deserialize(in ISerializeReader reader, Encoding encoding)
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

                return encoding.GetString(buffer);
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(string? obj)
    {
        if (obj is null)
            return 1;
        return 1 + 4 + _encoding.GetByteCount(obj);
    }

    public int ObjectSerializedSize(string? obj, Encoding encoding)
    {
        if (obj is null)
            return 1;
        return 1 + 4 + encoding.GetByteCount(obj);
    }
}

