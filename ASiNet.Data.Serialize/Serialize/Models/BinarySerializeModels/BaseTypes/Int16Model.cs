using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class Int16Model : BaseSerializeModel<short>
{
    public override short Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(short)))
        {
            var buffer = (stackalloc byte[sizeof(short)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToInt16(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(short)))
        {
            var buffer = (stackalloc byte[sizeof(short)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToInt16(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(short obj, ISerializerWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(short)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializerWriter writer)
    {
        if (obj is short value)
        {
            var buffer = (stackalloc byte[sizeof(short)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }
}