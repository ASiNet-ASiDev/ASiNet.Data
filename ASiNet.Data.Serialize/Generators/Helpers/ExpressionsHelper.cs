using System.Collections;
using System.Linq.Expressions;
using System.Text;
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

    public static Expression CallDeserialize(Expression serializeModel, Expression reader, Expression context) =>
        Expression.Call(serializeModel, nameof(SerializeModel<byte>.Deserialize), null, reader, context);

    public static Expression CallSerialize(Expression serializeModel, Expression value, Expression writer, Expression context) =>
        Expression.Call(serializeModel, nameof(SerializeModel<byte>.Serialize), null, value, writer, context);

    public static Expression CallSerializeString(Expression serializeModel, Expression value, Expression writer, Expression context, Expression encoding) =>
        Expression.Call(Expression.Convert(serializeModel, typeof(ISerializeStringModel)), nameof(ISerializeStringModel.Serialize), null, value, writer, context, encoding);

    public static Expression CallDeserializeString(Expression serializeModel, Expression reader, Expression context, Expression encoding) =>
        Expression.Call(Expression.Convert(serializeModel, typeof(ISerializeStringModel)), nameof(ISerializeStringModel.Deserialize), null, reader, context, encoding);

    public static Expression CallGetSizeStringGenerateTime(Expression serializeModel, Expression inst, Expression context, Expression encoding) =>
        Expression.Call(
            Expression.Convert(
                serializeModel, 
                typeof(ISerializeStringModel)), 
            nameof(ISerializeStringModel.ObjectSerializedSize), null, inst, context, encoding);

    public static Expression CallDeserializeObject(Expression serializeModel, Expression reader, Expression context) =>
        Expression.Call(serializeModel, nameof(ISerializeModel.DeserializeToObject), null, reader, context);

    public static Expression CallSerializeObject(Expression serializeModel, Expression value, Expression writer, Expression context) =>
        Expression.Call(serializeModel, nameof(ISerializeModel.SerializeObject), null, value, writer, context);

    public static Expression CallGetSizeGenerateTime(Expression serializeModel, Expression inst, Expression context) =>
        Expression.Call(
            serializeModel,
            serializeModel.Type.GetMethod(nameof(ISerializeModel<byte>.ObjectSerializedSize), 
                [inst.Type, typeof(ISerializerContext)])!, 
            inst, 
            context);

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
