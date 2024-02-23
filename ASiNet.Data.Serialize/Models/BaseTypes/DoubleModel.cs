using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class DoubleModel : BaseSerializeModel<double>
{
    public override double Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(double)))
        {
            var buffer = (stackalloc byte[sizeof(double)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToDouble(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader) => 
        Deserialize(reader);

    public override void Serialize(double obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }

        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((double)obj!, writer);
}
