using System.Collections;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels;

public class ArrayModel<TElement> : BaseSerializeModel<TElement?[]>
{
    private Lazy<Type> _arrayElementType = new(
        () => typeof(TElement).GetElementType()
            ?? throw new Exception("Invalid array element type."));

    public override void Serialize(TElement?[] obj, ISerializeWriter writer)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, obj.Length);
        writer.WriteBytes(buffer); // Writing an array length

        var model = BinarySerializer.SharedSerializeContext.GetModel(_arrayElementType.Value)
                   ?? throw new Exception("Invalid array element type.");

        foreach (var element in obj)
            model.SerializeObject(element, writer);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        var array = obj as TElement[]
                    ?? throw new Exception("Invalid array element type.");

        Serialize(array, writer);
    }

    public override TElement?[]? Deserialize(ISerializeReader reader)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        reader.ReadBytes(buffer); // Read length

        int arrayLength = BitConverter.ToInt32(buffer);

        if (reader.AvalibleAreaSize % arrayLength != 0)
            throw new Exception("Invalid data to deserealize in array.");

        var model = BinarySerializer.SharedSerializeContext.GetModel(_arrayElementType.Value)
                    ?? throw new Exception("Invalid array element type.");

        var arrResult = Array.CreateInstance(_arrayElementType.Value, arrayLength) as TElement?[];
        
        for (int i = 0; i < arrayLength; i++)
            arrResult![i] = (TElement?)model.DeserializeToObject(reader);

        return arrResult;
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }
}