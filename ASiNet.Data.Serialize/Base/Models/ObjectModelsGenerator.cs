using System.Linq.Expressions;
using System.Reflection;

namespace ASiNet.Data.Serialization.Base.Models;

public delegate object?[] GetValuesDelegate<T>(T obj);

public delegate void SetValuesDelegate<T>(T obj, object?[] values);

public class ObjectModelsGenerator
{
    public ObjectModel<T> GenerateModel<T>()
    {
        var model = new ObjectModel<T>();

        model.SetGetValueeDelegate(GenerateGetLambda<T>());
        model.SetSetValueeDelegate(GenerateSetLambda<T>());
        model.SetProps(GetProps(typeof(T)));

        return model;
    }

    public GetValuesDelegate<T> GenerateGetLambda<T>()
    {
        var instParam = Expression.Parameter(typeof(T), "inst");
        var rp = Expression.Parameter(typeof(object[]), "props");
        var body = Expression.Block([rp],
            [
                Expression.Assign(rp, EnumeratePropsGetMethod(typeof(T), instParam)),
                rp,
            ]);

        var lambda = Expression.Lambda<GetValuesDelegate<T>>(body, instParam);
        return lambda.Compile();
    }
    
    private Expression EnumeratePropsGetMethod(Type type, Expression inst)
    {
        var props = GetProps(type);

        var arrProp = Expression.Parameter(typeof(object[]), "arr");

        var body = new List<Expression>
        {
            Expression.Assign(arrProp, Expression.NewArrayBounds(typeof(object), Expression.Constant(props.Count())))
        };

        var i = 0;
        foreach (var prop in props)
        {
            body.Add(Expression.Assign(Expression.ArrayAccess(arrProp, Expression.Constant(i)), GetPropertyModel(prop, inst)));
            i++;
        }

        body.Add(arrProp);

        var arr = Expression.Block([arrProp], body);

        return arr;
    }

    private Expression GetPropertyModel(PropertyInfo pi, Expression inst)
    {
        return Expression.Convert(Expression.Property(inst, pi), typeof(object));
    }

    public SetValuesDelegate<T> GenerateSetLambda<T>()
    {
        var instParam = Expression.Parameter(typeof(T), "inst");
        var valuesParam = Expression.Parameter(typeof(object[]), "props");
        var body = Expression.Block([], EnumeratePropsSetMethod(typeof(T), instParam, valuesParam));

        var lambda = Expression.Lambda<SetValuesDelegate<T>>(body, instParam, valuesParam);
        return lambda.Compile();
    }

    private IEnumerable<Expression> EnumeratePropsSetMethod(Type type, Expression inst, Expression valuesArr)
    {
        var props = GetProps(type);

        yield return Expression.IfThen(
                        Expression.NotEqual(
                            Expression.ArrayLength(valuesArr),
                            Expression.Constant(props.Count())),
                        Expression.Throw(
                            Expression.Constant(new IndexOutOfRangeException($"The number of properties[{props.Length}] to record does not match"))));

        var i = 0;
        foreach (var prop in props)
        {

            yield return Expression.Assign(
                            Expression.Property(inst, prop),
                            Expression.Convert(
                                Expression.ArrayAccess(valuesArr, Expression.Constant(i)),
                                prop.PropertyType));
            i++;
        }
    }

    private PropertyInfo[] GetProps(Type type) =>
        type.GetProperties().OrderBy(x => x.Name).ToArray();
}
