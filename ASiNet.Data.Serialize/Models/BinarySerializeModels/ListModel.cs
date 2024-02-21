using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels;

public class ListModel<TElement> : BaseSerializeModel<List<TElement?>>
{
    public override void Serialize(List<TElement?> obj, ISerializeWriter writer)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, obj.Count);
        writer.WriteBytes(buffer); // Writing an array length

        var model = BinarySerializer.SharedSerializeContext.GetModel(typeof(TElement))
                    ?? throw new Exception("Invalid array element type.");

        foreach (var element in obj)
            model.SerializeObject(element, writer);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        var array = obj as List<TElement?>
                    ?? throw new Exception("Invalid array element type.");

        Serialize(array, writer);
    }

    public override List<TElement?>? Deserialize(ISerializeReader reader)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        reader.ReadBytes(buffer); // Read length

        int arrayLength = BitConverter.ToInt32(buffer);

        if (reader.AvalibleAreaSize % arrayLength != 0)
            throw new Exception("Invalid data to deserealize in array.");

        var model = BinarySerializer.SharedSerializeContext.GetModel(typeof(TElement))
                    ?? throw new Exception("Invalid array element type.");

        var arrResult = new List<TElement?>();
        
        for (int i = 0; i < arrayLength; i++)
            arrResult.Add((TElement?)model.DeserializeToObject(reader));

        return arrResult;
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }
}