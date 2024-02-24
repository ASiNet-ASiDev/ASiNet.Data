using System.Collections;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;

public class ListModel<TList> : BaseSerializeModel<TList>
{
    private Lazy<ISerializeModel> _arrayElementSerializeModel = new(() =>
        BinarySerializer.SharedSerializeContext.GetOrGenerate(typeof(TList).GenericTypeArguments[0] ?? throw new Exception())
        ?? throw new Exception($"Invalid array element type."));

    public override void Serialize(TList? obj, ISerializeWriter writer)
    {
        if(obj is null)
        {
            writer.WriteByte(0);
            return;
        }
        else
            writer.WriteByte(1);

        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, ((IList)obj).Count);
        writer.WriteBytes(buffer); // Writing an array length

        var model = _arrayElementSerializeModel.Value;

        foreach (var element in (IList)obj!)
            model.SerializeObject(element, writer);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((TList?)obj, writer);
    

    public override TList? Deserialize(ISerializeReader reader)
    {
        if(reader.ReadByte() == 0)
            return default;

        Span<byte> buffer = stackalloc byte[sizeof(int)];
        reader.ReadBytes(buffer); // Read length

        int arrayLength = BitConverter.ToInt32(buffer);

        if (reader.AvalibleAreaSize % arrayLength != 0)
            throw new Exception("Invalid data to deserealize in array.");

        var model = _arrayElementSerializeModel.Value;

        var arrResult = (IList)Activator.CreateInstance(typeof(TList))!;

        for (int i = 0; i < arrayLength; i++)
            arrResult.Add(model.DeserializeToObject(reader));

        return (TList)arrResult;
    }

    public override object? DeserializeToObject(ISerializeReader reader) => 
        Deserialize(reader);
    
}