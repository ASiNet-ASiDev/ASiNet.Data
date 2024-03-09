using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class UInt32Model : SerializeModelBase<uint>
{
    public override uint Deserialize(in ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(uint)))
        {
            var buffer = (stackalloc byte[sizeof(uint)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt32(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(uint)))
        {
            var buffer = (stackalloc byte[sizeof(uint)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt32(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(uint obj, in ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(uint)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer)
    {
        if (obj is uint value)
        {
            var buffer = (stackalloc byte[sizeof(uint)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(uint obj) => sizeof(uint);
}