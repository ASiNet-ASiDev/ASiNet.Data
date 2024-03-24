using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class DoubleModel : SerializeModelBase<double>
{
    public override double Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        if (reader.CanReadSize(sizeof(double)))
        {
            var buffer = (stackalloc byte[sizeof(double)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToDouble(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context) =>
        Deserialize(reader, context);

    public override void Serialize(double obj, in ISerializeWriter writer, ISerializerContext context)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }

        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context) =>
        Serialize((double)obj!, writer, context);


    public override int ObjectSerializedSize(double obj, ISerializerContext context) => sizeof(double);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(double);
}
