using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class UInt64Model : SerializeModelBase<ulong>
{
    public override ulong Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(ulong)))
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt64(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(ulong)))
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt64(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(ulong obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is ulong value)
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(ulong obj) => sizeof(ulong);

}