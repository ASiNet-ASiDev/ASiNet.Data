using System.Collections.Generic;
using ASiNet.Data.Serialization.Contexts;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization;
public static class BinarySerializer
{
    /// <summary>
    /// The serializer context stores models and model generators.
    /// </summary>
    public static ISerializerContext SerializeContext => _sharedSerializeContext.Value;

    private static Lazy<ISerializerContext> _sharedSerializeContext = new(() => InitDefaultContext());

    /// <summary>
    /// Get the size of the object in bytes before it is serialized. It may be useful if you are not sure about choosing the buffer size.
    /// <para/>
    /// Attention! This method may attempt to generate models if they have not been generated yet!
    /// </summary>
    /// <returns> Bytes size. </returns>
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
    /// If the context is initialized, it will return its type.
    /// </summary>
    /// <returns> <see cref="SerializeContext"/> type or <see cref="null"/></returns>
    public static Type? InitedContextType() =>
        _sharedSerializeContext.IsValueCreated ? _sharedSerializeContext.Value.GetType() : null;

    /// <summary>
    /// Checks whether the context has been created
    /// </summary>
    /// <returns> True if <see cref="SerializeContext"/> is created, other false</returns>
    public static bool IsInit() =>
        _sharedSerializeContext.IsValueCreated;

    /// <summary>
    /// Use the default context.
    /// </summary>
    /// <param name="settings"> Generators and Context settings. </param>
    /// <returns><see cref="DefaultSerializerContext"/></returns>
    public static ISerializerContext InitDefaultContext(GeneratorsSettings? settings = null) => 
        (_sharedSerializeContext = new Lazy<ISerializerContext>(() => new DefaultSerializerContext(settings ?? new()))).Value;

    /// <summary>
    /// Cannot generate models during execution. 
    /// Use the <see cref="Attributes.PreGenerateAttribute"/> attribute to mark all types that should be used by the serializer.
    /// <para/>
    /// Uses a <see cref="System.Collections.Frozen.FrozenDictionary{TKey, TValue}"/> to speed up work.
    /// </summary>
    /// <param name="settings"> Generators and Context settings. </param>
    /// <returns><see cref="ReadonlySerializerContext"/></returns>
    public static ISerializerContext InitReadonlyContext(GeneratorsSettings? settings = null) =>
        (_sharedSerializeContext = new Lazy<ISerializerContext>(() => new ReadonlySerializerContext(settings ?? new()))).Value;

    /// <summary>
    /// Use your own context.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static ISerializerContext InitCustomContext(Func<ISerializerContext> factory) =>
        (_sharedSerializeContext = new Lazy<ISerializerContext>(factory)).Value;
}
