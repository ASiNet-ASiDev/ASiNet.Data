using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class ByteModel : SerializeModelBase<byte>
{
    public override byte Deserialize(in ISerializeReader reader)
    {
        if (reader.CanReadSize(1))
        {
            return reader.ReadByte();
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader)
    {
        if (reader.CanReadSize(1))
        {
            return reader.ReadByte();
        }
        throw new Exception();
    }

    public override void Serialize(byte obj, in ISerializeWriter writer)
    {
        writer.WriteByte(obj);
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer)
    {
        if (obj is byte value)
        {
            writer.WriteByte(value);
            return;
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(byte obj) => sizeof(byte);
}