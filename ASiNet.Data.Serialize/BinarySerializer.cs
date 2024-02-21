using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization;
public static class BinarySerializer
{
    public static ObjectModelsContext SharedObjectsModelContext => _sharedObjectsModelContext.Value;

    public static SerializerContext SharedSerializeContext => _sharedSerializeContext.Value;

    private static Lazy<ObjectModelsContext> _sharedObjectsModelContext = new();

    private static Lazy<SerializerContext> _sharedSerializeContext = new(() => SerializerContext.FromDefaultModels(SharedObjectsModelContext));

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

    public static int SerializeArray<T>(T obj, byte[] buffer)
        => Serialize(obj, (ArrayWriter)buffer);

    public static T? DeserializeArray<T>(byte[] buffer)
        => Deserialize<T>((ArrayReader)buffer);
}
