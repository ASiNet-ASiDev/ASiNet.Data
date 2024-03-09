using System.Linq.Expressions;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Generators.Helpers;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Generators;
public class EnumsModelsGenerator : IModelsGenerator
{
    public bool CanGenerateModelForType(Type type) => type.IsEnum;

    public bool CanGenerateModelForType<T>() => typeof(T).IsEnum;

    public SerializeModel<T> GenerateModel<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        try
        {
            var model = new SerializeModel<T>();

            serializeContext.AddModel(model);

            model.SetSerializeDelegate(GenerateSerializeLambda<T>(serializeContext, settings));
            model.SetDeserializeDelegate(GenerateDeserializeLambda<T>(serializeContext, settings));
            model.SetGetSizeDelegate(GenerateGetSizeDelegate<T>(serializeContext, settings));

            return model;
        }
        catch (Exception ex)
        {
            throw new GeneratorException(ex);
        }
    }

    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var enumUnderlyingType = type.GetEnumUnderlyingType();

        var writer = Expression.Parameter(typeof(ISerializeWriter).MakeByRefType(), "writer");
        var inst = Expression.Parameter(type, "inst");

        var model = ExpressionsHelper.GetOrGenerateModelGenerateTime(enumUnderlyingType, serializeContext);

        var body = ExpressionsHelper.CallSerialize(model, Expression.Convert(inst, enumUnderlyingType), writer);

        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(body, inst, writer);
        return lambda.Compile();
    }

    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var enumUnderlyingType = type.GetEnumUnderlyingType();

        var reader = Expression.Parameter(typeof(ISerializeReader).MakeByRefType(), "reader");

        var model = ExpressionsHelper.GetOrGenerateModelGenerateTime(enumUnderlyingType, serializeContext);

        var body = Expression.Convert(ExpressionsHelper.CallDeserialize(model, reader), type);

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(body, reader);
        return lambda.Compile();
    }

    public GetObjectSizeDelegate<T> GenerateGetSizeDelegate<T>(ISerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var enumUnderlyingType = type.GetEnumUnderlyingType();

        var inst = Expression.Parameter(type, "inst");
        var result = Expression.Parameter(typeof(int), "size");

        var model = ExpressionsHelper.GetOrGenerateModelGenerateTime(enumUnderlyingType, serializeContext);

        var body = Expression.Block([result],
            Expression.Assign(
                result,
                ExpressionsHelper.CallGetSizeGenerateTime(
                    model,
                    Expression.Convert(
                        inst,
                        enumUnderlyingType)
                    )
                ),
            result
            );

        var lambda = Expression.Lambda<GetObjectSizeDelegate<T>>(body, inst);
        return lambda.Compile();
    }
}
