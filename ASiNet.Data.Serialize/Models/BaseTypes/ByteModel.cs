using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class ByteModel : SerializeModelBase<byte>
{
    public override byte Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(1))
        {
            return reader.ReadByte();
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(1))
        {
            return reader.ReadByte();
        }
        throw new Exception();
    }

    public override void Serialize(byte obj, in ISerializeWriter writer, ISerializerContext context)
    {
        writer.WriteByte(obj);
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (obj is byte value)
        {
            writer.WriteByte(value);
            return;
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(byte obj, ISerializerContext context) => sizeof(byte);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(byte);
}