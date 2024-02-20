using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Base.Models;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize;

public delegate void SerializeObjectDelegate<T>(T? obj, ISerializerWriter writer);

public delegate T? DeserializeObjectDelegate<T>(ISerializeReader reader);

public class SerializerModelsGenerator
{
    public SerializeModel<T> GenerateModel<T>(ObjectModelsContext modelsContext, SerializerContext serializeContext)
    {
        var om = modelsContext.GetOrGenerate<T>();
        var model = new SerializeModel<T>();

        model.SetSerializeDelegate(GenerateSerializeLambda<T>(om, serializeContext));
        
        return model;
    }


    public SerializeObjectDelegate<T> GenerateSerializeLambda<T>(ObjectModel<T> model, SerializerContext serializeContext)
    {
        var inst = Expression.Parameter(typeof(T));
        var writer = Expression.Parameter(typeof(ISerializerWriter));
        var om = Expression.Parameter(typeof(ObjectModel<T>));

        var propsArr = Expression.Parameter(typeof(object[]));

        var body = new List<Expression> 
        {
            Expression.Assign(om, Expression.Constant(model)),
            Expression.Assign(propsArr, Expression.Call(om, nameof(ObjectModel<T>.GetValues), null, Expression.Convert(inst, typeof(object))))
        };

        body.AddRange(WriteProperties<T>(inst, writer, propsArr, model, serializeContext));

        var block = Expression.Block([om, propsArr], body);

        var lambda = Expression.Lambda<SerializeObjectDelegate<T>>(block, inst, writer);
        return lambda.Compile();
    }

    private IEnumerable<Expression> WriteProperties<T>(Expression inst, Expression writer, Expression props, ObjectModel<T> model, SerializerContext serializeContext)
    {
        var i = 0;
        foreach (var prop in model.EnumirateProps())
        {
            var sm = (ISerializeModel)SerializerHalper.InvokeGenerickMethod(serializeContext, nameof(SerializerContext.GetOrGenerate), [prop.PropertyType], [])!;
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
}
