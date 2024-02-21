namespace ASiNet.Data.Serialize.Interfaces;
public interface ISerializeModel : IDisposable
{
    public Type ObjType { get; }

    public bool ContainsSerializeDelegate { get; }
    public bool ContainsDeserializeDelegate { get; }

    public void SerializeObject(object? obj, ISerializeWriter writer);

    public object? DeserializeToObject(ISerializeReader reader);
}
