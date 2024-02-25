namespace ASiNet.Data.Serialization.Interfaces;

public delegate void SerializeObjectDelegate<T>(T? obj, ISerializeWriter writer);

public delegate T? DeserializeObjectDelegate<T>(ISerializeReader reader);

public interface IModelsGenerator
{

    public SerializeModel<T> GenerateModel<T>(SerializerContext serializeContext, in GeneratorsSettings settings);

    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings);

    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings);
}
