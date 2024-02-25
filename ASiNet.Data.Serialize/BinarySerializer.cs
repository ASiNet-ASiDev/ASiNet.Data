using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization;
public static class BinarySerializer
{
    public static SerializerContext SharedSerializeContext => _sharedSerializeContext.Value;
    private static Lazy<SerializerContext> _sharedSerializeContext = new(() => 
    {  
        var serializerContext = new SerializerContext();
        SerializerHelper.AddUnsafeArraysTypes(serializerContext);
        SerializerHelper.AddUnmanagedTypes(serializerContext);
        return serializerContext;
    });

    public static SerializerSettings Settings { get; set; } = new();

    public static int Serialize<T>(T obj, ISerializeWriter writer)
    {
        var model = SharedSerializeContext.GetOrGenerate<T>();
        model.Serialize(obj, writer);
        return writer.FilledAreaSize;
    }

    public static T? Deserialize<T>(ISerializeReader reader)
    {
        var model = SharedSerializeContext.GetOrGenerate<T>();
        return model.Deserialize(reader);
    }

    public static int Serialize<T>(T obj, byte[] buffer)
        => Serialize<T>(obj, (ISerializeWriter)(ArrayWriter)buffer);

    public static T? Deserialize<T>(byte[] buffer)
        => Deserialize<T>((ISerializeReader)(ArrayReader)buffer);
}
