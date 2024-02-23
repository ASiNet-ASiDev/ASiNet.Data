using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class SbyteModel : BaseSerializeModel<sbyte>
{
    public override sbyte Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(1))
        {
            return (sbyte)reader.ReadByte();
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

    public override void Serialize(sbyte obj, ISerializeWriter writer)
    {
        writer.WriteByte((byte)obj);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is sbyte value)
        {
            writer.WriteByte((byte)value);
            return;
        }
        throw new Exception();
    }
}