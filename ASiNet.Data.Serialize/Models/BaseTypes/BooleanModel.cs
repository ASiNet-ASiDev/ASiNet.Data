using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class BooleanModel : BaseSerializeModel<bool>
{
    public override bool Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(bool)))
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToBoolean(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(bool)))
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToBoolean(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(bool obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(bool)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
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
}
