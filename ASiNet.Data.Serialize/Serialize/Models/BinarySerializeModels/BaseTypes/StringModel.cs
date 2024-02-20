using System.Text;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class StringModel : BaseSerializeModel<string>
{
    public override string Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(int)))
        {
            var sizeBuffer = (stackalloc byte[sizeof(int)]);
            reader.ReadBytes(sizeBuffer);
            var strBytesSize = BitConverter.ToInt32(sizeBuffer);
            if (reader.CanReadSize(strBytesSize))
            {
                var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
                reader.ReadBytes(buffer);
                return Encoding.UTF8.GetString(buffer);
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(int)))
        {
            var sizeBuffer = (stackalloc byte[sizeof(int)]);
            reader.ReadBytes(sizeBuffer);
            var strBytesSize = BitConverter.ToInt32(sizeBuffer);
            if (reader.CanReadSize(strBytesSize))
            {
                var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
                reader.ReadBytes(buffer);
                return Encoding.UTF8.GetString(buffer);
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override void Serialize(string obj, ISerializerWriter writer)
    {
        var strBytesSize = Encoding.UTF8.GetByteCount(obj);
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

    public override void SerializeObject(object? obj, ISerializerWriter writer)
    {
        if (obj is string value)
        {
            var strBytesSize = Encoding.UTF8.GetByteCount(value);
            var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
            if (value.TryToBytes(buffer))
            {
                var strSzBytes = (stackalloc byte[sizeof(int)]);
                BitConverter.TryWriteBytes(strSzBytes, strBytesSize);
                writer.WriteBytes(strSzBytes);
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }
}

