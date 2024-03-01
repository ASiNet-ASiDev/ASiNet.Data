namespace ASiNet.Data.Serialization.Interfaces;

public delegate void SerializeObjectDelegate<T>(T? obj, ISerializeWriter writer);

public delegate T? DeserializeObjectDelegate<T>(ISerializeReader reader);

public delegate int GetObjectSizeDelegate<T>(T? obj);
/// <summary>
/// The model generator is used to create models of various types.
/// </summary>
public interface IModelsGenerator
{
    /// <summary>
    /// Create a new model
    /// </summary>
    /// <typeparam name="T"> The type of model being created </typeparam>
    /// <param name="serializeContext"> Context where will the available models come from, if that's what the generator is trembling about </param>
    /// <param name="settings"> Model Generation Settings </param>
    /// <returns> New serialize model </returns>
    public SerializeModel<T> GenerateModel<T>(SerializerContext serializeContext, in GeneratorsSettings settings);

    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings);

    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings);

    public GetObjectSizeDelegate<T> GenerateGetSizeDelegate<T>(SerializerContext serializeContext, in GeneratorsSettings settings);
}
