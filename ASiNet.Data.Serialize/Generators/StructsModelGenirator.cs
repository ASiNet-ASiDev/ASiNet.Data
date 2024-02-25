using System.Linq.Expressions;
using ASiNet.Data.Serialization.Attributes;
using System.Reflection;
using ASiNet.Data.Serialization.Exceptions;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Generators;
public class StructsModelGenirator : IModelsGenerator
{
    public SerializeModel<T> GenerateModel<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        try
        {
            var model = new SerializeModel<T>();

            serializeContext.AddModel(model);

            model.SetSerializeDelegate(GenerateSerializeLambda<T>(serializeContext, settings));
            model.SetDeserializeDelegate(GenerateDeserializeLambda<T>(serializeContext, settings));

            return model;
        }
        catch (Exception ex)
        {
            throw new GeneratorException(ex);
        }
    }


    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var inst = Expression.Parameter(type, "inst");
        var writer = Expression.Parameter(typeof(ISerializeWriter), "writer");

        var body = Expression.Block(SerializeProperties(type, inst, writer, serializeContext, settings));


        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(body, inst, writer);
        return lambda.Compile();
    }

    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(SerializerContext serializeContext, in GeneratorsSettings settings)
    {
        var type = typeof(T);
        var inst = Expression.Parameter(type, "inst");
        var reader = Expression.Parameter(typeof(ISerializeReader), "reader");

        var body = Expression.Block(DeserializeProperties(type, inst, reader, serializeContext, settings));

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(Expression.Block([inst], body, inst), reader);
        return lambda.Compile();
    }

    private IEnumerable<Expression> SerializeProperties(Type type, Expression inst, Expression writer, SerializerContext serializeContext, GeneratorsSettings settings)
    {
        if (!settings.GlobalIgnoreProperties || type.GetCustomAttribute<IgnorePropertiesAttribute>() is not null)
        {
            // WRITE OBJECT PROPERTIES!
            foreach (var pi in SerializerHelper.EnumerateProperties(type))
            {
                var model = SerializerHelper.GetOrGenerateSerializeModelConstant(pi.PropertyType, serializeContext);
                var value = Expression.Property(inst, pi);
                yield return SerializerHelper.CallSerialize(model, value, writer);
            }
        }

        if (!settings.GlobalIgnoreFields || type.GetCustomAttribute<IgnoreFieldsAttribute>() is not null)
        {
            // WRITE OBJECT FIELDS!
            foreach (var fi in SerializerHelper.EnumerateFields(type))
            {
                var model = SerializerHelper.GetOrGenerateSerializeModelConstant(fi.FieldType, serializeContext);
                var value = Expression.Field(inst, fi);
                yield return SerializerHelper.CallSerialize(model, value, writer);
            }
        }
    }

    private IEnumerable<Expression> DeserializeProperties(Type type, Expression inst, Expression reader, SerializerContext serializeContext, GeneratorsSettings settings)
    {
        // CREATE NEW INSTANCE!
        yield return Expression.Assign(inst, Expression.New(type));

        if (!settings.GlobalIgnoreProperties || type.GetCustomAttribute<IgnorePropertiesAttribute>() is not null)
        {
            // READ AND SET PROPERTIES!
            foreach (var pi in SerializerHelper.EnumerateProperties(type))
            {
                var model = SerializerHelper.GetOrGenerateSerializeModelConstant(pi.PropertyType, serializeContext);
                var value = Expression.Property(inst, pi);
                yield return Expression.Assign(value, SerializerHelper.CallDeserialize(model, reader));
            }
        }

        if (!settings.GlobalIgnoreFields || type.GetCustomAttribute<IgnoreFieldsAttribute>() is not null)
        {
            // READ AND SET FIELDS!
            foreach (var fi in SerializerHelper.EnumerateFields(type))
            {
                var model = SerializerHelper.GetOrGenerateSerializeModelConstant(fi.FieldType, serializeContext);
                var value = Expression.Field(inst, fi);
                yield return Expression.Assign(value, SerializerHelper.CallDeserialize(model, reader));
            }
        }
    }
}
