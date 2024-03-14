using System.Reflection;
using ASiNet.Data.Serialization.Attributes;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialization;
internal static class Helper
{
    public static void AddUnmanagedTypes(ISerializerContext context)
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
        context.AddModel(new DateTimeModel());
        context.AddModel(new TimeSpanModel());
    }

    public static void AddUnsafeArraysTypes(ISerializerContext context)
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

    public static IEnumerable<Type> EnumiratePreGenerateModels()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes().Where(x => x.GetCustomAttribute<PreGenerateAttribute>() is not null))
            {
                yield return type;
            }
        }
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


    public static void WriteTypeHash(ISerializeWriter writer, ISerializeModel model)
    {
        writer.WriteBytes(model.TypeHashBytes);
    }

    public static long ReadTypeHash(ISerializeReader reader)
    {
        var buff = (stackalloc byte[sizeof(long)]);
        reader.ReadBytes(buff);
        var hashString = BitConverter.ToInt64(buff);
        return hashString;
    }

    public static IEnumerable<PropertyInfo> EnumerateProperties(Type type)
    {
        // GET ALL PROPERTIES.
        var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty)
            .OrderBy(x => x.PropertyType.Name)
            .ThenBy(x => x.Name)
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
            .OrderBy(x => x.FieldType.Name)
            .ThenBy(x => x.Name)
            .Where(x => x.GetCustomAttribute<IgnoreFieldAttribute>() is null);
        foreach (var item in fields)
        {
            yield return item;
        }
        yield break;
    }
}
