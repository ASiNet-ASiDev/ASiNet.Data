using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class DateTimeModel : BaseSerializeModel<DateTime>
{
    private Lazy<SerializeModel<long>> _longSerializeModel =
        new(() => BinarySerializer.SerializeContext.GetOrGenerate<long>()!);

    public override DateTime Deserialize(ISerializeReader reader)
    {
        return DateTime.FromBinary(_longSerializeModel.Value.Deserialize(reader));
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
        Serialize((DateTime)obj!, writer);
    }

    public override int ObjectSerializedSize(DateTime obj) => sizeof(long);
}

public class TimeSpanModel : BaseSerializeModel<TimeSpan>
{
    private Lazy<SerializeModel<long>> _longSerializeModel =
        new(() => BinarySerializer.SerializeContext.GetOrGenerate<long>()!);

    public override TimeSpan Deserialize(ISerializeReader reader)
    {
        return TimeSpan.FromTicks(_longSerializeModel.Value.Deserialize(reader));
    }

    public override void Serialize(TimeSpan obj, ISerializeWriter writer)
    {
        _longSerializeModel.Value.SerializeObject(obj.Ticks, writer);
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        Serialize((TimeSpan)obj!, writer);
    }

    public override int ObjectSerializedSize(TimeSpan obj) => sizeof(long);
}