using System.Linq.Expressions;
using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization.Base.Models;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization;

public delegate void SerializeObjectDelegate<T>(T? obj, ISerializeWriter writer);

public delegate T? DeserializeObjectDelegate<T>(ISerializeReader reader);

public class SerializerModelsGenerator
{
    public SerializeModel<T> GenerateModel<T>(ObjectModelsContext modelsContext, SerializerContext serializeContext)
    {
        var om = modelsContext.GetOrGenerate<T>();
        var model = new SerializeModel<T>();

        model.SetSerializeDelegate(GenerateSerializeLambda(om, serializeContext));
        model.SetDeserializeDelegate(GenerateDeserializeLambda(om, serializeContext));

        return model;
    }


    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(ObjectModel<T> model, SerializerContext serializeContext)
    {
        var inst = Expression.Parameter(typeof(T), "inst");
        var writer = Expression.Parameter(typeof(ISerializeWriter), "writer");
        var om = Expression.Parameter(typeof(ObjectModel<T>), "objectModel");

        var propsArr = Expression.Parameter(typeof(object[]), "props");

        var body = new List<Expression>
        {
            Expression.Assign(om, Expression.Constant(model)),
            Expression.Assign(propsArr, Expression.Call(om, nameof(ObjectModel<T>.GetValues), null, Expression.Convert(inst, typeof(object))))
        };

        body.AddRange(WriteProperties(inst, writer, propsArr, model, serializeContext));

        var block = Expression.Block([om, propsArr], body);

        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(block, inst, writer);
        return lambda.Compile();
    }

    private IEnumerable<Expression> WriteProperties<T>(Expression inst, Expression writer, Expression props, ObjectModel<T> model, SerializerContext serializeContext)
    {
        var i = 0;
        foreach (var prop in model.EnumirateProps())
        {
            var sm = (ISerializeModel)SerializerHelper.InvokeGenerickMethod(serializeContext, nameof(SerializerContext.GetOrGenerate), [prop.PropertyType], [])!;
            yield return Expression.Call(
                Expression.Constant(sm),
                nameof(ISerializeModel.SerializeObject),
                null,
                Expression.ArrayAccess(
                    props,
                    Expression.Constant(i)),
                writer);
            i++;
        }

        yield break;
    }

    public DeserializeObjectDelegate<T> GenerateDeserializeLambda<T>(ObjectModel<T> model, SerializerContext serializeContext)
    {
        var inst = Expression.Parameter(typeof(T), "inst");
        var reader = Expression.Parameter(typeof(ISerializeReader), "reader");
        var om = Expression.Parameter(typeof(ObjectModel<T>), "objectModel");

        var propsArr = Expression.Parameter(typeof(object[]), "props");

        var body = new List<Expression>
        {
            Expression.Assign(om, Expression.Constant(model)),
            Expression.Assign(inst, Expression.New(typeof(T))),
            Expression.Assign(propsArr, Expression.NewArrayBounds(typeof(object), Expression.Constant(model.PropertiesCount))),
        };

        body.AddRange(ReadProperties(inst, reader, propsArr, model, serializeContext));
        body.Add(Expression.Call(om, nameof(ObjectModel<T>.SetValues), null, Expression.Convert(inst, typeof(object)), propsArr));
        body.Add(inst);

        var block = Expression.Block([om, propsArr, inst], body);

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(block, reader);
        return lambda.Compile();
    }

    private IEnumerable<Expression> ReadProperties<T>(Expression inst, Expression reader, Expression props, ObjectModel<T> model, SerializerContext serializeContext)
    {
        var i = 0;
        foreach (var prop in model.EnumirateProps())
        {
            var sm = (ISerializeModel)SerializerHelper.InvokeGenerickMethod(serializeContext, nameof(SerializerContext.GetOrGenerate), [prop.PropertyType], [])!;

            var desResult = Expression.Call(
                Expression.Constant(sm), 
                nameof(ISerializeModel.DeserializeToObject), 
                null, 
                reader);
            yield return 
                Expression.Assign(
                    Expression.ArrayAccess(
                        props, 
                        Expression.Constant(i)),
                    desResult);
            i++;
        }
        yield break;
    }
}
