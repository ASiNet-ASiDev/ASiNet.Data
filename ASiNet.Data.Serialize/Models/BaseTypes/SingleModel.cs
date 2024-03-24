using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class SingleModel : SerializeModelBase<float>
{
    public override float Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(float)))
        {
            var buffer = (stackalloc byte[sizeof(float)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToSingle(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(float)))
        {
            var buffer = (stackalloc byte[sizeof(float)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToSingle(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(float obj, in ISerializeWriter writer, ISerializerContext context)
    {
        var buffer = (stackalloc byte[sizeof(float)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (obj is float value)
        {
            var buffer = (stackalloc byte[sizeof(float)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }


    public override int ObjectSerializedSize(float obj, ISerializerContext context) => sizeof(float);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(float);
}