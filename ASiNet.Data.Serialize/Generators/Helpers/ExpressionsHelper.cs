using System.Collections;
using System.Linq.Expressions;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

namespace ASiNet.Data.Serialization.Generators.Helpers;
internal static class ExpressionsHelper
{
    public static Expression WriteNullableByteGenerateTime(Expression writer, byte value) =>
        Expression.Call(writer, nameof(ISerializeWriter.WriteByte), null, Expression.Constant(value));

    public static Expression WriteNullableByteGenerateTime(Expression writer, Expression value) =>
        Expression.Call(writer, nameof(ISerializeWriter.WriteByte), null, value);

    public static Expression ReadNullableByteGenerateTime(Expression reader) =>
        Expression.Call(reader, nameof(ISerializeReader.ReadByte), null);

    public static Expression GetOrGenerateModelGenerateTime(Type type, ISerializerContext serializeContext) =>
        Expression.Constant(
            Helper.InvokeGenerickMethod(
                serializeContext,
                nameof(ISerializerContext.GetOrGenerate),
                [type],
                [])!,
            typeof(SerializeModel<>).MakeGenericType(type));

    public static Expression GetOrGenerateModelSerializeTime(Expression type, Expression serializeContext) =>
        Expression.Call(
            serializeContext, 
            nameof(ISerializerContext.GetOrGenerate), 
            null,
            type
            );

    public static Expression CallGetType(Expression value) =>
        Expression.Call(
                Expression.Convert(value, typeof(object)),
                nameof(object.GetType),
                null
                );

    public static Expression GetOrGenerateModelByHash(Expression reader, Expression serializeContext) =>
        Expression.Call(
            serializeContext, 
            nameof(ISerializerContext.GetOrGenerateByHash), 
            null,
            Expression.Call(
                typeof(Helper), 
                nameof(Helper.ReadTypeHash), 
                null, 
                reader)
            );

    public static Expression SerializeTypeHash(Expression writer, Expression model) =>
        Expression.Call(typeof(Helper), nameof(Helper.WriteTypeHash), null, writer, model);

    public static Expression CallDeserialize(Expression serializeModel, Expression reader) =>
        Expression.Call(serializeModel, nameof(SerializeModel<byte>.Deserialize), null, reader);

    public static Expression CallSerialize(Expression serializeModel, Expression value, Expression writer) =>
        Expression.Call(serializeModel, nameof(SerializeModel<byte>.Serialize), null, value, writer);


    public static Expression CallDeserializeObject(Expression serializeModel, Expression reader) =>
        Expression.Call(serializeModel, nameof(ISerializeModel.DeserializeToObject), null, reader);

    public static Expression CallSerializeObject(Expression serializeModel, Expression value, Expression writer) =>
        Expression.Call(serializeModel, nameof(ISerializeModel.SerializeObject), null, value, writer);

    public static Expression CallGetSizeGenerateTime(Expression serializeModel, Expression inst) =>
        Expression.Call(serializeModel, serializeModel.Type.GetMethod(nameof(ISerializeModel.ObjectSerializedSize), [inst.Type])!, inst);

    public static Expression CallEnumiratorMoveNextGenerateTime(Expression enumirator) =>
        Expression.Call(Expression.Convert(enumirator, typeof(IEnumerator)), nameof(IEnumerator<byte>.MoveNext), null);

    public static Expression CallEnumiratorCurrentGenerateTime(Expression enumirator) =>
        Expression.Property(enumirator, nameof(IEnumerator<byte>.Current), null);

    public static Expression CallGetEnumiratorGenerateTime(Expression collection, Type valueType)
    {
        var t = collection.Type;
        var mi = t.GetMethod(nameof(ICollection.GetEnumerator))!;
        return Expression.Convert(Expression.Call(collection, mi), typeof(IEnumerator<>).MakeGenericType(valueType));
    }
}
