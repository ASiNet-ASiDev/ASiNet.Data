using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class ByteModel : BaseSerializeModel<byte>
{
    public override byte Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(1))
        {
            return reader.ReadByte();
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(1))
        {
            return reader.ReadByte();;
        }
        throw new Exception();
    }

    public override void Serialize(byte obj, ISerializeWriter writer)
    {
        writer.WriteByte(obj);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is byte value)
        {
            writer.WriteByte(value);
            return;
        }
        throw new Exception();
    }
}