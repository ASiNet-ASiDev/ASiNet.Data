using System.Collections;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;

public class DictionaryModel<TDictionary> : BaseSerializeModel<TDictionary>
    where TDictionary : class, IDictionary, new()
{
    private Lazy<Type> _arrayElementType = new(() =>
        typeof(TDictionary).GenericTypeArguments[0] ?? throw new Exception());

    private Lazy<ISerializeModel> _keyElementSerializeModel = new(() =>
        BinarySerializer.SharedSerializeContext.GetOrGenerate(
            typeof(TDictionary).GenericTypeArguments[0]
            ?? throw new Exception())
        ?? throw new Exception($"Invalid array element type."));

    private Lazy<ISerializeModel> _valueElementSerializeModel = new(() =>
        BinarySerializer.SharedSerializeContext.GetOrGenerate(
            typeof(TDictionary).GenericTypeArguments[1]
            ?? throw new Exception())
        ?? throw new Exception($"Invalid array element type."));

    public override void Serialize(TDictionary obj, ISerializeWriter writer)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, obj.Count);
        writer.WriteBytes(buffer); // Writing an array length

        var keyModel = _keyElementSerializeModel.Value;
        var valueModel = _valueElementSerializeModel.Value;

        foreach (var element in obj.Keys)
            keyModel.SerializeObject(element, writer);

        foreach (var element in obj.Values)
            valueModel.SerializeObject(element, writer);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        var array = obj as TDictionary
                    ?? throw new Exception("Invalid array element type.");

        Serialize(array, writer);
    }

    public override TDictionary? Deserialize(ISerializeReader reader)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        reader.ReadBytes(buffer); // Read length

        int arrayLength = BitConverter.ToInt32(buffer);

        if (reader.AvalibleAreaSize % arrayLength != 0)
            throw new Exception("Invalid data to deserealize in array.");

        var keyModel = _keyElementSerializeModel.Value;
        var valueModel = _valueElementSerializeModel.Value;

        var arrResult = new TDictionary();

        var keys = new object?[arrayLength];
        var values = new object?[arrayLength];

        for (int i = 0; i < arrayLength; i++)
            keys[i] = keyModel.DeserializeToObject(reader);

        for (int i = 0; i < arrayLength; i++)
            values[i] = valueModel.DeserializeToObject(reader);

        for (int i = 0; i < arrayLength; i++)
            arrResult.Add(keys[i], values[i]);

        return arrResult;
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }
}