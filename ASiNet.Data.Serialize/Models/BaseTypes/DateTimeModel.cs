using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class DateTimeModel : SerializeModelBase<DateTime>
{
    private Lazy<SerializeModel<long>> _longSerializeModel =
        new(() => BinarySerializer.SerializeContext.GetOrGenerate<long>()!);

    public override DateTime Deserialize(in ISerializeReader reader)
    {
        return DateTime.FromBinary(_longSerializeModel.Value.Deserialize(reader));
    }

    public override void Serialize(DateTime obj, in ISerializeWriter writer)
    {
        _longSerializeModel.Value.SerializeObject(obj.ToBinary(), writer);
    }

    public override object? DeserializeToObject(in ISerializeReader reader)
    {
        return Deserialize(reader);
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer)
    {
        Serialize((DateTime)obj!, writer);
    }

    public override int ObjectSerializedSize(DateTime obj) => sizeof(long);

    public override int ObjectSerializedSize(object obj) => sizeof(long);
}

public class TimeSpanModel : SerializeModelBase<TimeSpan>
{
    private Lazy<SerializeModel<long>> _longSerializeModel =
        new(() => BinarySerializer.SerializeContext.GetOrGenerate<long>()!);

    public override TimeSpan Deserialize(in ISerializeReader reader)
    {
        return TimeSpan.FromTicks(_longSerializeModel.Value.Deserialize(reader));
    }

    public override void Serialize(TimeSpan obj, in ISerializeWriter writer)
    {
        _longSerializeModel.Value.SerializeObject(obj.Ticks, writer);
    }

    public override object? DeserializeToObject(in ISerializeReader reader)
    {
        return Deserialize(reader);
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer)
    {
        Serialize((TimeSpan)obj!, writer);
    }

    public override int ObjectSerializedSize(TimeSpan obj) => sizeof(long);

    public override int ObjectSerializedSize(object obj) => sizeof(long);
}