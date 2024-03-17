using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;

public abstract class SerializeModelBase<T> : SerializeModel<T>
{
    /// <summary>
    /// Reads an object from byte data.
    /// </summary>
    /// <param name="reader"> Byte space from where the object is read. </param>
    /// <returns></returns>
    public override abstract T Deserialize(in ISerializeReader reader);


    /// <summary>
    /// Writes an object as byte data.
    /// </summary>
    /// <param name="obj"> The object being recorded. </param>
    /// <param name="writer"> The byte space where the object is written to. </param>
    public override abstract void Serialize(T obj, in ISerializeWriter writer);

    public override abstract object? DeserializeToObject(in ISerializeReader reader);

    public override abstract void SerializeObject(object? obj, in ISerializeWriter writer);

    public override abstract int ObjectSerializedSize(T obj);
}