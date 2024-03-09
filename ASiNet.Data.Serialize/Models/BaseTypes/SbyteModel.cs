using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class SByteModel : SerializeModelBase<sbyte>
{
    public override sbyte Deserialize(in ISerializeReader reader)
    {
        if (reader.CanReadSize(1))
        {
            return (sbyte)reader.ReadByte();
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader)
    {
        if (reader.CanReadSize(1))
        {
            return reader.ReadByte(); ;
        }
        throw new Exception();
    }

    public override void Serialize(sbyte obj, in ISerializeWriter writer)
    {
        writer.WriteByte((byte)obj);
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer)
    {
        if (obj is sbyte value)
        {
            writer.WriteByte((byte)value);
            return;
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(sbyte obj) => sizeof(sbyte);

}