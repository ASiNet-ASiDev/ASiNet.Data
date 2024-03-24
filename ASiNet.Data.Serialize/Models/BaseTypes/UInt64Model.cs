using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class UInt64Model : SerializeModelBase<ulong>
{
    public override ulong Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(ulong)))
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt64(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(ulong)))
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt64(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(ulong obj, in ISerializeWriter writer, ISerializerContext context)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context)
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

    public override int ObjectSerializedSize(ulong obj, ISerializerContext context) => sizeof(ulong);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(ulong);

}