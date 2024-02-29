using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class UInt16Model : BaseSerializeModel<ushort>
{
    public override ushort Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(ushort)))
        {
            var buffer = (stackalloc byte[sizeof(ushort)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt16(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(ushort)))
        {
            var buffer = (stackalloc byte[sizeof(ushort)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt16(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(ushort obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(ushort)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is ushort value)
        {
            var buffer = (stackalloc byte[sizeof(ushort)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(ushort obj) => sizeof(ushort);
}