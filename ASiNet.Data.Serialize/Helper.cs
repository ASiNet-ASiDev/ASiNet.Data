using System.Linq.Expressions;
using ASiNet.Data.Serialization.Attributes;
using System.Reflection;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialization;
internal static class Helper
{
    public static TEnum ToEnum<TType, TEnum>(TType x)
    {
        return (TEnum)(object)x!;
    }


    public static void AddUnmanagedTypes(SerializerContext context)
    {
        context.AddModel(new ByteModel());
        context.AddModel(new SByteModel());

        context.AddModel(new Int16Model());
        context.AddModel(new Int32Model());
        context.AddModel(new Int64Model());

        context.AddModel(new UInt16Model());
        context.AddModel(new UInt32Model());
        context.AddModel(new UInt64Model());

        context.AddModel(new SingleModel());
        context.AddModel(new DoubleModel());

        context.AddModel(new CharModel());
        context.AddModel(new StringModel());

        context.AddModel(new BooleanModel());

        context.AddModel(new GuidModel());
    }

    public static void AddUnsafeArraysTypes(SerializerContext context)
    {
        context.AddModel(new BooleanArrayModel());

        context.AddModel(new Int16ArrayModel());
        context.AddModel(new UInt16ArrayModel());

        context.AddModel(new Int32ArrayModel());
        context.AddModel(new UInt32ArrayModel());

        context.AddModel(new Int64ArrayModel());
        context.AddModel(new UInt64ArrayModel());

        context.AddModel(new SingleArrayModel());
        context.AddModel(new DoubleArrayModel());

        context.AddModel(new ByteArrayModel());
        context.AddModel(new SByteArrayModel());

        context.AddModel(new GuidArrayModel());
        context.AddModel(new DateTimeArrayModel());
    }

    public static Enum ToEnum<TType>(TType x, Type type) where TType : struct
    {
        return (Enum)(object)x;
    }

    public static IEnumerable<Type> EnumiratePreGenerateModels()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes().Where(x => x.GetCustomAttribute<PreGenerateModelAttribute>() is not null))
            {
                yield return type;
            }
        }
    }

    static bool IsNullable<T>(T obj)
    {
        if (obj is null) 
            return true; 
        Type type = typeof(T);
        if (!type.IsValueType) 
            return true;
        if (Nullable.GetUnderlyingType(type) != null) 
            return true;
        return false;
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


    public static IEnumerable<PropertyInfo> EnumerateProperties(Type type)
    {
        // GET ALL PROPERTIES.
        var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty)
            .OrderBy(x => x.Name)
            .Where(x => x.GetIndexParameters().Length == 0 && x.GetCustomAttribute<IgnorePropertyAttribute>() is null);
        foreach (var item in props)
        {
            yield return item;
        }
        yield break;
    }

    public static IEnumerable<FieldInfo> EnumerateFields(Type type)
    {
        // GET ALL FIELDS.
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField)
            .OrderBy(prop => prop.Name)
            .Where(x => x.GetCustomAttribute<IgnoreFieldAttribute>() is null);
        foreach (var item in fields)
        {
            yield return item;
        }
        yield break;
    }
}
