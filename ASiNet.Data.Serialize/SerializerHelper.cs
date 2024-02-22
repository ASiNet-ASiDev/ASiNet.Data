using System.Linq.Expressions;
using ASiNet.Data.Serialization.Base.Models;

namespace ASiNet.Data.Serialization;
public static class SerializerHelper
{
    public static TEnum ToEnum<TType, TEnum>(TType x) where TType : struct where TEnum : Enum
    {
        return (TEnum)(object)x;
    }

    public static Enum ToEnum<TType>(TType x, Type type) where TType : struct
    {
        var result = Activator.CreateInstance(type);
        result = x;
        return (Enum)result;
    }

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
