using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Base.Models;

namespace ASiNet.Data.Serialize;
public static class SerializerHelper
{
    public static Expression ObjectModelGetMethodCall(Expression om, Expression inst) =>
        Expression.Call(om, nameof(ObjectModel<byte>.GetValues), [inst.Type], inst);
    
    public static object? InvokeGenerickMethod(object inst, string methodName, Type[] genericParameters, object?[] parameters)
    {
        var method = inst
            .GetType()
            .GetMethods()
            .Where(x => x.Name == methodName)
            .Where(x => x.GetGenericArguments().Length == genericParameters.Length)
            .First();

        return method
            .MakeGenericMethod(genericParameters)
            .Invoke(inst, parameters);
    }
}
