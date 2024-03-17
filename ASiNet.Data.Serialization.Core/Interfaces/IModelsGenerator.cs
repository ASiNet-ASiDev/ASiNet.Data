using System;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Interfaces
{
    public delegate void SerializeObjectDelegate<T>(T obj, in ISerializeWriter writer);

    public delegate T DeserializeObjectDelegate<T>(in ISerializeReader reader);

    public delegate int GetObjectSizeDelegate<T>(T obj);
    /// <summary>
    /// The model generator is used to create models of various types.
    /// </summary>
    public interface IModelsGenerator
    {
        bool CanGenerateModelForType(Type type);
        bool CanGenerateModelForType<T>();

        /// <summary>
        /// Create a new model
        /// </summary>
        /// <typeparam name="T"> The type of model being created </typeparam>
        /// <param name="serializeContext"> Context where will the available models come from, if that's what the generator is trembling about </param>
        /// <param name="settings"> Model Generation Settings </param>
        /// <returns> New serialize model </returns>
        SerializeModel<T> GenerateModel<T>(ISerializerContext serializeContext, in GeneratorsSettings settings);

        SerializeObjectDelegate<T> GenerateSerializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings);

        DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings);

        GetObjectSizeDelegate<T> GenerateGetSizeDelegate<T>(ISerializerContext serializeContext, in GeneratorsSettings settings);
    }
}
