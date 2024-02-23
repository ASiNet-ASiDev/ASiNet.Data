using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;

public abstract class BaseSerializeModel<T> : SerializeModel<T>
{
    public override bool ContainsDeserializeDelegate => true;
    public override bool ContainsSerializeDelegate => true;

    /// <summary>
    /// Reads an object from byte data.
    /// </summary>
    /// <param name="reader"> Byte space from where the object is read. </param>
    /// <returns></returns>
    public override abstract T? Deserialize(ISerializeReader reader);


    /// <summary>
    /// Writes an object as byte data.
    /// </summary>
    /// <param name="obj"> The object being recorded. </param>
    /// <param name="writer"> The byte space where the object is written to. </param>
    public override abstract void Serialize(T obj, ISerializeWriter writer);

    public override abstract object? DeserializeToObject(ISerializeReader reader);

    public override abstract void SerializeObject(object? obj, ISerializeWriter writer);
}