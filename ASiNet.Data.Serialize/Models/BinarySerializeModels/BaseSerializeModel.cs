using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels;
public abstract class BaseSerializeModel<T> : SerializeModel<T>
{
    public override bool ContainsDeserializeDelegate => true;
    public override bool ContainsSerializeDelegate => true;

    public override abstract T? Deserialize(ISerializeReader reader);

    public override abstract object? DeserializeToObject(ISerializeReader reader);

    public override abstract void Serialize(T obj, ISerializeWriter writer);

    public override abstract void SerializeObject(object? obj, ISerializeWriter writer);
}