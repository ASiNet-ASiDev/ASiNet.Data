namespace ASiNet.Data.Serialization.Interfaces;
/// <summary>
/// An interface that provides an API for creating serializer models.
/// <para/>
/// To create your own models, it is better to use <see cref="Models.BaseSerializeModel{T}"/>
/// </summary>
public interface ISerializeModel : IDisposable
{
    /// <summary>
    /// The type of the object of the current model
    /// </summary>
    public Type ObjType { get; }

    public int ObjectSerializedSize(object? obj);

    /// <summary>
    /// For the model generator, the default is always true
    /// </summary>
    public bool ContainsSerializeDelegate { get; }
    /// <summary>
    /// For the model generator, the default is always true
    /// </summary>
    public bool ContainsDeserializeDelegate { get; }

    public bool ContainsGetSizeDelegate { get; }

    /// <summary>
    /// Writes an object as byte data.
    /// </summary>
    /// <param name="obj"> The object being recorded. </param>
    /// <param name="writer"> The byte space where the object is written to. </param>
    public void SerializeObject(object? obj, ISerializeWriter writer);

    /// <summary>
    /// Reads an object from byte data.
    /// </summary>
    /// <param name="reader"> Byte space from where the object is read. </param>
    /// <returns></returns>
    public object? DeserializeToObject(ISerializeReader reader);
}
