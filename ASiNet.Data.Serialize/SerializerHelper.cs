using System.Linq.Expressions;
using ASiNet.Data.Serialization.Interfaces;

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



    public static Expression WriteNullableByte(Expression writer, byte value) =>
        Expression.Call(writer, nameof(ISerializeWriter.WriteByte), null, Expression.Constant(value));

    public static Expression WriteNullableByte(Expression writer, Expression value) =>
        Expression.Call(writer, nameof(ISerializeWriter.WriteByte), null, value);

    public static Expression ReadNullableByte(Expression reader) =>
        Expression.Call(reader, nameof(ISerializeReader.ReadByte), null);

    public static Expression GetOrGenerateSerializeModelConstant(Type type, SerializerContext serializeContext) =>
        Expression.Constant(
            InvokeGenerickMethod(
                serializeContext,
                nameof(SerializerContext.GetOrGenerate),
                [type],
                [])!,
            typeof(SerializeModel<>).MakeGenericType(type));

    public static Expression CallDeserialize(Expression serializeModel, Expression reader) =>
        Expression.Call(serializeModel, nameof(SerializeModel<byte>.Deserialize), null, reader);

    public static Expression CallSerialize(Expression serializeModel, Expression value, Expression writer) =>
        Expression.Call(serializeModel, nameof(SerializeModel<byte>.Serialize), null, value, writer);

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
