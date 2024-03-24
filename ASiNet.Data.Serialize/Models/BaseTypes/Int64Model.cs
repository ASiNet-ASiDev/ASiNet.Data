using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class Int64Model : SerializeModelBase<long>
{
    public override long Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(long)))
        {
            var buffer = (stackalloc byte[sizeof(long)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToInt64(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(long)))
        {
            var buffer = (stackalloc byte[sizeof(long)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToInt64(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(long obj, in ISerializeWriter writer, ISerializerContext context)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (obj is long value)
        {
            var buffer = (stackalloc byte[sizeof(long)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(long obj, ISerializerContext context) => sizeof(long);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(long);

}