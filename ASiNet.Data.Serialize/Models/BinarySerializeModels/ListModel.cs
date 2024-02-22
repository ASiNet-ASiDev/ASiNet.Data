using System.Collections;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels;

public class ListModel<TList> : BaseSerializeModel<TList>
    where TList : class, IList, new()
{
    private Lazy<ISerializeModel> _arrayElementSerializeModel = new(() =>
        BinarySerializer.SharedSerializeContext.GetOrGenerate(typeof(TList).GenericTypeArguments[0] ?? throw new Exception()) 
        ?? throw new Exception($"Invalid array element type."));
    
    public override void Serialize(TList obj, ISerializeWriter writer)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, obj.Count);
        writer.WriteBytes(buffer); // Writing an array length

        var model = _arrayElementSerializeModel.Value;

        foreach (var element in obj)
            model.SerializeObject(element, writer);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        var array = obj as TList
                    ?? throw new Exception("Invalid array element type.");

        Serialize(array, writer);
    }

    public override TList? Deserialize(ISerializeReader reader)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        reader.ReadBytes(buffer); // Read length

        int arrayLength = BitConverter.ToInt32(buffer);

        if (reader.AvalibleAreaSize % arrayLength != 0)
            throw new Exception("Invalid data to deserealize in array.");

        var model = _arrayElementSerializeModel.Value;

        var arrResult = new TList();
        
        for (int i = 0; i < arrayLength; i++)
            arrResult.Add(model.DeserializeToObject(reader));

        return arrResult;
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }
}