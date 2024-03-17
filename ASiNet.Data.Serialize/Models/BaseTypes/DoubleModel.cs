using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class DoubleModel : SerializeModelBase<double>
{
    public override double Deserialize(in ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(double)))
        {
            var buffer = (stackalloc byte[sizeof(double)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToDouble(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(in ISerializeReader reader) =>
        Deserialize(reader);

    public override void Serialize(double obj, in ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }

        throw new Exception();
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer) =>
        Serialize((double)obj!, writer);


    public override int ObjectSerializedSize(double obj) => sizeof(double);

    public override int ObjectSerializedSize(object obj) => sizeof(double);
}
