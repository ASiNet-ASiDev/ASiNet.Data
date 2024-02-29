using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization;
public static class BinarySerializer
{
    /// <summary>
    /// The serializer context stores models and model generators.
    /// </summary>
    public static SerializerContext SerializeContext => _sharedSerializeContext.Value;

    private static Lazy<SerializerContext> _sharedSerializeContext = new(InitContextBase);

    /// <summary>
    /// Context Settings and Model Generators0
    /// </summary>
    public static GeneratorsSettings Settings { get; set; } = new();

    public static int GetSize<T>(T? obj) =>
        SerializeContext.GetOrGenerate<T>().ObjectSerializedSize(obj);

    /// <summary>
    /// Serializes an object into bytes
    /// </summary>
    /// <param name="obj"> Serialized object </param>
    /// <param name="buffer"> The buffer where the object will be serialized. Make sure that its size is sufficient to fit the entire object! </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.SerializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> The number of bytes written. </returns>
    public static int Serialize<T>(T obj, byte[] buffer)
    => Serialize<T>(obj, (ISerializeWriter)(ArrayWriter)buffer);

    /// <summary>
    /// Serializes an object into bytes
    /// </summary>
    /// <param name="obj"> Serialized object </param>
    /// <param name="buffer"> The buffer where the object will be serialized. Make sure that its size is sufficient to fit the entire object! </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.SerializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> The number of bytes written. </returns>
    public static int Serialize<T>(T obj, ISerializeWriter writer)
    {
        var model = SerializeContext.GetOrGenerate<T>();
        model.Serialize(obj, writer);
        return writer.FilledBytes;
    }

    /// <summary>
    /// Deserializes an object from the buffer
    /// </summary>
    /// <param name="buffer"> Buffer from where the data will be read </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.DeserializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> Deserialized object </returns>
    public static T? Deserialize<T>(byte[] buffer)
        => Deserialize<T>((ISerializeReader)(ArrayReader)buffer);

    /// <summary>
    /// Deserializes an object from the buffer
    /// </summary>
    /// <param name="reader"> Buffer from where the data will be read </param>
    /// <exception cref="Exceptions.GeneratorException"/>
    /// <exception cref="Exceptions.DeserializeException"/>
    /// <exception cref="Exceptions.TypeNotSupportedException"/>
    /// <exception cref="IndexOutOfRangeException"/>
    /// <returns> Deserialized object </returns>
    public static T? Deserialize<T>(ISerializeReader reader)
    {
        var model = SerializeContext.GetOrGenerate<T>();
        return model.Deserialize(reader);
    }

    /// <summary>
    /// Checks whether the context has been created
    /// </summary>
    /// <returns> True if <see cref="SerializeContext"/> is created, other false</returns>
    public static bool IsInit() =>
        _sharedSerializeContext.IsValueCreated;

    /// <summary>
    /// Forcibly creates a context if it hasn't been created yet
    /// </summary>
    public static SerializerContext Init() =>
        _sharedSerializeContext.Value;

    /// <summary>
    /// Recreates the current context
    /// </summary>
    public static SerializerContext RegenerateContext()
    {
        _sharedSerializeContext = new(InitContextBase);
        return _sharedSerializeContext.Value;
    }

    private static SerializerContext InitContextBase()
    {
        var serializerContext = new SerializerContext();

        if (Settings.UseDefaultBaseTypesModels)
            Helper.AddUnmanagedTypes(serializerContext);
        if (Settings.UseDefaultUnsafeArraysModels)
            Helper.AddUnsafeArraysTypes(serializerContext);


        if (Settings.AllowPreGenerateModelAttribute)
        {
            foreach (var type in Helper.EnumiratePreGenerateModels())
            {
                serializerContext.GenerateModel(type);
            }
        }

        return serializerContext;
    }
}
