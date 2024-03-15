using System.Text;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Generators;
public class StringModelsGenerator : IModelsGenerator
{
    public bool CanGenerateModelForType(Type type) =>
        type == typeof(string);

    public bool CanGenerateModelForType<T>() => CanGenerateModelForType(typeof(T));

    public SerializeModel<T> GenerateModel<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        SerializeModel<T>? result = typeof(T).Name switch
        {
            nameof(String) => Cast(new StringModel(settings.DefaultEncoding)),
            _ => null,
        };

        static SerializeModel<T>? Cast(ISerializeModel model) =>
            model as SerializeModel<T>;

        return result is null ? throw new GeneratorException(new NotImplementedException()) : result;
    }


    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        throw new NotImplementedException();
    }

    public GetObjectSizeDelegate<T> GenerateGetSizeDelegate<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        throw new NotImplementedException();
    }

    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        throw new NotImplementedException();
    }
}
