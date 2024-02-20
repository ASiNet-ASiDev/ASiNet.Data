using System.Collections;
using ASiNet.Data.Serialize.Interfaces;
using ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.ArrayTypes;

public class ArrayModel<T> : BaseSerializeModel<T>
    where T: class, IList
{
    private Lazy<Type> _arrayElementType = new(
        ()=>typeof(T).GetElementType() 
            ?? throw new Exception("Invalid array element type."));
    
    public override void Serialize(T obj, ISerializeWriter writer)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, obj.Count);
        writer.WriteBytes(buffer); // Writing an array length
        
        var model = BinarySerializer.SharedSerializeContext.GetModel(_arrayElementType.Value)
                   ?? throw new Exception("Invalid array element type.");
        
        foreach (var element in obj)
            model.SerializeObject(element, writer);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        var array = (obj as T)
                    ?? throw new Exception("Invalid array element type.");
        
        Serialize(array, writer);
    }

    public override T? Deserialize(ISerializeReader reader)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        reader.ReadBytes(buffer); // Read length

        int arrayLength = BitConverter.ToInt32(buffer);

        if (reader.AvalibleAreaSize % arrayLength != 0)
            throw new Exception("Invalid data to deserealize in array.");

        var model = BinarySerializer.SharedSerializeContext.GetModel(_arrayElementType.Value)
                    ?? throw new Exception("Invalid array element type.");
        
        var arrResult = Array.CreateInstance(_arrayElementType.Value, arrayLength) as T;
        
        int elementSize = reader.AvalibleAreaSize / arrayLength;
        for (int i = 0; i < arrayLength; i++)
            arrResult![i] = model.DeserializeToObject(reader);
        
        return arrResult;
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }
}