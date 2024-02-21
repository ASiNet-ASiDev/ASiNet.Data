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
        var inst = Expression.Parameter(typeof(T));
        var writer = Expression.Parameter(typeof(ISerializeWriter));
        var om = Expression.Parameter(typeof(ObjectModel<T>));

        var propsArr = Expression.Parameter(typeof(object[]));

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
        var inst = Expression.Parameter(typeof(T));
        var reader = Expression.Parameter(typeof(ISerializeReader));
        var om = Expression.Parameter(typeof(ObjectModel<T>));


        var block = Expression.Block();

        var lambda = Expression.Lambda<DeserializeObjectDelegate<T>>(block, reader);
        return lambda.Compile();
    }
}
