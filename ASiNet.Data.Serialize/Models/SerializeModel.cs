using System.Security.Cryptography;
using System.Text;
using ASiNet.Data.Serialization.Generators;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;
public class SerializeModel<T>(
    SerializeObjectDelegate<T>? serialize = null,
    DeserializeObjectDelegate<T>? deserialize = null,
    GetObjectSizeDelegate<T>? getSize = null) : ISerializeModel
{

    public string TypeHash => _typeHash.Value.Hash;
    public byte[] TypeHashBytes => _typeHash.Value.BytesHash;

    public Type ObjType => _objType.Value;

    private readonly Lazy<Type> _objType = new(() => typeof(T));

    private readonly Lazy<(byte[] BytesHash, string Hash)> _typeHash = new(() => 
    { 
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(typeof(T).FullName ?? typeof(T).Name));
        var str = Convert.ToHexString(bytes);
        return (bytes, str);
    });

    public virtual bool ContainsSerializeDelegate => _serializeDelegate is not null;
    public virtual bool ContainsDeserializeDelegate => _deserializeDelegate is not null;
    public virtual bool ContainsGetSizeDelegate => _deserializeDelegate is not null;


    private SerializeObjectDelegate<T>? _serializeDelegate = serialize;
    private DeserializeObjectDelegate<T>? _deserializeDelegate = deserialize;
    private GetObjectSizeDelegate<T>? _getSizeDelegate = getSize;

    public virtual void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (_serializeDelegate is null)
            throw new NullReferenceException();
        if (obj is T value)
            _serializeDelegate(value, writer);
    }

    public virtual object? DeserializeToObject(ISerializeReader reader)
    {
        if (_deserializeDelegate is null)
            throw new NullReferenceException();
        return _deserializeDelegate(reader);
    }

    public virtual void Serialize(T obj, ISerializeWriter writer)
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

    internal void SetGetSizeDelegate(GetObjectSizeDelegate<T>? get) =>
        _getSizeDelegate = get;

    public virtual int ObjectSerializedSize(object? obj) =>
        ObjectSerializedSize((T?)obj);

    public virtual int ObjectSerializedSize(T? obj)
    {
        if (_getSizeDelegate is null)
            throw new NullReferenceException();
        return _getSizeDelegate(obj);
    }

    public void Dispose()
    {
        _serializeDelegate = null;
        _deserializeDelegate = null;
        GC.SuppressFinalize(this);
    }
}
