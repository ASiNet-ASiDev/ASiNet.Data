using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

public class DateTimeModel : SerializeModelBase<DateTime>
{
    private Lazy<SerializeModel<long>> _longSerializeModel =
        new(() => BinarySerializer.SerializeContext.GetOrGenerate<long>()!);

    public override DateTime Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        return DateTime.FromBinary(_longSerializeModel.Value.Deserialize(reader, context));
    }

    public override void Serialize(DateTime obj, in ISerializeWriter writer, ISerializerContext context)
    {
        _longSerializeModel.Value.SerializeObject(obj.ToBinary(), writer, context);
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context)
    {
        return Deserialize(reader, context);
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        Serialize((DateTime)obj!, writer, context);
    }

    public override int ObjectSerializedSize(DateTime obj, ISerializerContext context) => sizeof(long);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(long);
}

public class TimeSpanModel : SerializeModelBase<TimeSpan>
{
    private Lazy<SerializeModel<long>> _longSerializeModel =
        new(() => BinarySerializer.SerializeContext.GetOrGenerate<long>()!);

    public override TimeSpan Deserialize(in ISerializeReader reader, ISerializerContext context)
    {
        return TimeSpan.FromTicks(_longSerializeModel.Value.Deserialize(reader, context));
    }

    public override void Serialize(TimeSpan obj, in ISerializeWriter writer, ISerializerContext context)
    {
        _longSerializeModel.Value.SerializeObject(obj.Ticks, writer, context);
    }

    public override object? DeserializeToObject(in ISerializeReader reader, ISerializerContext context)
    {
        return Deserialize(reader, context);
    }

    public override void SerializeObject(object? obj, in ISerializeWriter writer, ISerializerContext context)
    {
        Serialize((TimeSpan)obj!, writer, context);
    }

    public override int ObjectSerializedSize(TimeSpan obj, ISerializerContext context) => sizeof(long);

    public override int ObjectSerializedSize(object obj, ISerializerContext context) => sizeof(long);
}