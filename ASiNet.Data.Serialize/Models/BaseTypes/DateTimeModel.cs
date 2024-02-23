using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class DateTimeModel : BaseSerializeModel<DateTime>
{
    private Lazy<ISerializeModel> _longSerializeModel = 
        new(() => BinarySerializer.SharedSerializeContext.GetOrGenerate(typeof(long))!);
    
    public override DateTime Deserialize(ISerializeReader reader)
    {
        return DateTime.FromBinary((long)_longSerializeModel.Value.DeserializeToObject(reader));
    }

    public override void Serialize(DateTime obj, ISerializeWriter writer)
    {
        _longSerializeModel.Value.SerializeObject(obj.ToBinary(), writer);
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        Serialize((DateTime)obj, writer);
    }
}