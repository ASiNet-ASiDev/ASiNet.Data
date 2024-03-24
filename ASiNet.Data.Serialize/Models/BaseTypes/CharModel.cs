using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
public class CharModel : SerializeModelBase<char>
{
    public override char Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(char)))
        {
            var buffer = (stackalloc byte[sizeof(char)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToChar(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(char)))
        {
            var buffer = (stackalloc byte[sizeof(char)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToChar(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(char obj, in ISerializeWriter writer, ISerializerContext context)
    {
        var buffer = (stackalloc byte[sizeof(char)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        if (obj is char value)
        {
            var buffer = (stackalloc byte[sizeof(char)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }

    public override int ObjectSerializedSize(char obj, ISerializerContext context) => sizeof(char);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(char);
}
