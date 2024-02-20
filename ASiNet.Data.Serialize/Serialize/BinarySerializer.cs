using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Base.Models;

namespace ASiNet.Data.Serialize;
public static class BinarySerializer
{
    public static ObjectModelsContext SharedObjectsModelContext => _sharedObjectsModelContext.Value;

    public static SerializerContext SharedSerializeContext => _sharedSerializeContext.Value;

    private static Lazy<ObjectModelsContext> _sharedObjectsModelContext = new();

    private static Lazy<SerializerContext> _sharedSerializeContext = new();

    public static byte[] Serialize<T>(T obj)
    {
        var model = SharedSerializeContext.GetOrGenerate<T>();

        return null;
    }
}
