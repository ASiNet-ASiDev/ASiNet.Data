using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize;
public class SerializeModel<T>(SerializeObjectDelegate<T>? serialize = null, DeserializeObjectDelegate<T>? deserialize = null) : ISerializeModel
{
    public Type ObjType => _objType.Value;

    private Lazy<Type> _objType = new Lazy<Type>(() => typeof(T));

    public virtual bool ContainsSerializeDelegate => _serializeDelegate is not null;
    public virtual bool ContainsDeserializeDelegate => _deserializeDelegate is not null;


    private SerializeObjectDelegate<T>? _serializeDelegate = serialize;
    private DeserializeObjectDelegate<T>? _deserializeDelegate = deserialize;

    public virtual void SerializeObject(object? obj, ISerializerWriter writer)
    {
        if (_serializeDelegate is null)
            throw new NullReferenceException();
        if (obj is T value)
            _serializeDelegate(value, writer);
        throw new Exception();
    }

    public virtual object? DeserializeToObject(ISerializeReader reader)
    {
        if (_deserializeDelegate is null)
            throw new NullReferenceException();
        return _deserializeDelegate(reader);
    }

    public virtual void Serialize(T obj, ISerializerWriter writer)
    {
        if (_serializeDelegate is null)
            throw new NullReferenceException();
        _serializeDelegate(obj, writer);
    }

    public virtual T? Deserialize(ISerializeReader reader)
    {
        if (_deserializeDelegate is null)
            throw new NullReferenceException();
        return _deserializeDelegate(reader);
    }

    internal void SetSerializeDelegate(SerializeObjectDelegate<T>? set) =>
        _serializeDelegate = set;
    internal void SetDeserializeDelegate(DeserializeObjectDelegate<T>? get) =>
        _deserializeDelegate = get;

    public void Dispose()
    {
        _serializeDelegate = null;
        _deserializeDelegate = null;
        GC.SuppressFinalize(this);
    }
}
