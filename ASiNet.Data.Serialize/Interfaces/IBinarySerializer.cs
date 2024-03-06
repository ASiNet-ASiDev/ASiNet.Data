namespace ASiNet.Data.Serialization.Interfaces;
public interface IBinarySerializer
{
    public ISerializerContext Context { get; }

    /// <summary>
    /// Get the size of the object in bytes before it is serialized. It may be useful if you are not sure about choosing the buffer size.
    /// <para/>
    /// Attention! This method may attempt to generate models if they have not been generated yet!
    /// </summary>
    /// <returns> Bytes size. </returns>
    public int GetSize<T>(T? obj);

    /// <summary>
    /// Serializes an object into bytes
    /// </summary>
    /// <param name="obj"> Serialized object </param>
    /// <param name="buffer"> The buffer where the object will be serialized. Make sure that its size is sufficient to fit the entire object! </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.SerializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> The number of bytes written. </returns>
    public int Serialize<T>(T obj, byte[] buffer);

    /// <summary>
    /// Serializes an object into bytes
    /// </summary>
    /// <param name="obj"> Serialized object </param>
    /// <param name="buffer"> The buffer where the object will be serialized. Make sure that its size is sufficient to fit the entire object! </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.SerializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> The number of bytes written. </returns>
    public int Serialize<T>(T obj, ISerializeWriter writer);

    /// <summary>
    /// Deserializes an object from the buffer
    /// </summary>
    /// <param name="buffer"> Buffer from where the data will be read </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.DeserializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> Deserialized object </returns>
    public T? Deserialize<T>(byte[] buffer);

    /// <summary>
    /// Deserializes an object from the buffer
    /// </summary>
    /// <param name="reader"> Buffer from where the data will be read </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.DeserializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> Deserialized object </returns>
    public T? Deserialize<T>(ISerializeReader reader);
}
