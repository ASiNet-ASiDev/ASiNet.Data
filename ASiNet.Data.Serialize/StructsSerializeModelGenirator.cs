using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization;
public class StructsSerializeModelGenirator
{
    public SerializeModel<T> GenerateModel<T>(SerializerContext serializeContext)
    {
        var model = new SerializeModel<T>();

        serializeContext.AddModel(model);

        model.SetSerializeDelegate(GenerateSerializeLambda<T>(serializeContext));
        model.SetDeserializeDelegate(GenerateDeserializeLambda<T>(serializeContext));

        return model;
    }


    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(SerializerContext serializeContext)
    {
        var type = typeof(T);
        var inst = Expression.Parameter(type, "inst");
        var writer = Expression.Parameter(typeof(ISerializeWriter), "writer");

        var body = Expression.Block(SerializeProperties(type, inst, writer, serializeContext));


        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(body, inst, writer);
        return lambda.Compile();
    }

    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(SerializerContext serializeContext)
    {
        var type = typeof(T);
        var inst = Expression.Parameter(type, "inst");
        var reader = Expression.Parameter(typeof(ISerializeReader), "reader");

        var body = Expression.Block(DeserializeProperties(type, inst, reader, serializeContext));

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(Expression.Block([inst], body, inst), reader);
        return lambda.Compile();
    }

    private IEnumerable<Expression> SerializeProperties(Type type, Expression inst, Expression writer, SerializerContext serializeContext)
    {
        // WRITE OBJECT PROPERTIES!
        foreach (var pi in EnumerateProperties(type))
        {
            var model = SerializerHelper.GetOrGenerateSerializeModelConstant(pi.PropertyType, serializeContext);
            var value = Expression.Property(inst, pi);
            yield return SerializerHelper.CallSerialize(model, value, writer);
        }

        // WRITE OBJECT FIELDS!
        foreach (var fi in EnumerateFields(type))
        {
            var model = SerializerHelper.GetOrGenerateSerializeModelConstant(fi.FieldType, serializeContext);
            var value = Expression.Field(inst, fi);
            yield return SerializerHelper.CallSerialize(model, value, writer);
        }
    }

    private IEnumerable<Expression> DeserializeProperties(Type type, Expression inst, Expression reader, SerializerContext serializeContext)
    {
        // CREATE NEW INSTANCE!
        yield return Expression.Assign(inst, Expression.New(type));

        // READ AND SET PROPERTIES!
        foreach (var pi in EnumerateProperties(type))
        {
            var model = SerializerHelper.GetOrGenerateSerializeModelConstant(pi.PropertyType, serializeContext);
            var value = Expression.Property(inst, pi);
            yield return Expression.Assign(value, SerializerHelper.CallDeserialize(model, reader));
        }

        // READ AND SET FIELDS!
        foreach (var fi in EnumerateFields(type))
        {
            var model = SerializerHelper.GetOrGenerateSerializeModelConstant(fi.FieldType, serializeContext);
            var value = Expression.Field(inst, fi);
            yield return Expression.Assign(value, SerializerHelper.CallDeserialize(model, reader));
        }
    }

    private IEnumerable<PropertyInfo> EnumerateProperties(Type type)
    {
        // GET ALL PROPERTIES.
        var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty)
            .OrderBy(prop => prop.Name);
        foreach (var item in props)
        {
            yield return item;
        }
        yield break;
    }

    private IEnumerable<FieldInfo> EnumerateFields(Type type)
    {
        // GET ALL FIELDS.
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField);
        foreach (var item in fields)
        {
            yield return item;
        }
        yield break;
    }
}
