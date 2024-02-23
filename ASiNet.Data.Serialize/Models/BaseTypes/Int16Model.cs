using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
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

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);

    public override void Serialize(short obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(short)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((short)obj!, writer);
}