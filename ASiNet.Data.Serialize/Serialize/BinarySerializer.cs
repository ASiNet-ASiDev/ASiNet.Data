using ASiNet.Data.Base.Models;
using ASiNet.Data.Serialize.ArrayIO;

namespace ASiNet.Data.Serialize;
public static class BinarySerializer
{
    public static ObjectModelsContext SharedObjectsModelContext => _sharedObjectsModelContext.Value;

    public static SerializerContext SharedSerializeContext => _sharedSerializeContext.Value;

    private static Lazy<ObjectModelsContext> _sharedObjectsModelContext = new();

    private static Lazy<SerializerContext> _sharedSerializeContext = new();

    public static int Serialize<T>(T obj, byte[] buffer)
    {
        var model = SharedSerializeContext.GetOrGenerate<T>();

        var writer = (ArrayWriter)buffer;

        model.Serialize(obj, writer);

        return writer.FilledAreaSize;
    }

    public static T? Deserialize<T>(byte[] buffer)
    {
        var model = SharedSerializeContext.GetOrGenerate<T>();

        var reader = (ArrayReader)buffer;

        var result = (T?)model.Deserialize(reader);

        return result;
    }
}
