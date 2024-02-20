using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels;
public abstract class BaseSerializeModel<T> : SerializeModel<T>
{
    public override bool ContainsDeserializeDelegate => true;
    public override bool ContainsSerializeDelegate => true;

    public override abstract T? Deserealize(ISerializeReader reader);

    public override abstract object? Deserialize(ISerializeReader reader);

    public override abstract void Serealize(T obj, ISerializerWriter writer);

    public override abstract void Serialize(object? obj, ISerializerWriter writer);
}