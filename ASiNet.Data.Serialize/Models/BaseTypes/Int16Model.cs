using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class Int16Model : SerializeModelBase<short>
{
    public override short Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(short)))
        {
            var buffer = (stackalloc byte[sizeof(short)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToInt16(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override void Serialize(short obj, in ISerializeWriter writer, ISerializerContext context)
    {
        var buffer = (stackalloc byte[sizeof(short)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((short)obj!, writer, context);

    public override int ObjectSerializedSize(short obj, ISerializerContext context) => sizeof(short);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(short);
}