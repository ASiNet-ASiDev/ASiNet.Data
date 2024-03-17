using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class BooleanModel : SerializeModelBase<bool>
{
    public override bool Deserialize(in ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(bool)))
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToBoolean(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(bool)))
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToBoolean(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(bool obj, in ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(bool)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer)
    {
        if (obj is bool value)
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(bool obj) => sizeof(bool);

    public override int ObjectSerializedSize(object obj) => sizeof(bool);
}
