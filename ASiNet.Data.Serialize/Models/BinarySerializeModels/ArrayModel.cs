using System.Collections;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels;

public class ArrayModel<T> : BaseSerializeModel<T>
{
    private Lazy<Type> _arrayElementType = new (() => 
        typeof(T).GetElementType() ?? throw new Exception());

    private Lazy<ISerializeModel> _arrayElementSerializeModel = new(() =>
        BinarySerializer.SharedSerializeContext.GetOrGenerate(typeof(T).GetElementType() ?? throw new Exception()) ?? throw new Exception());

    public override void Serialize(T obj, ISerializeWriter writer)
    {
        var arr = obj as Array;

        if(arr is null)
        {
            writer.WriteByte(0);
            return;
        }
        else
            writer.WriteByte(1);

        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BitConverter.TryWriteBytes(buffer, arr.Length);
        writer.WriteBytes(buffer); // Writing an array length

        var model = _arrayElementSerializeModel.Value;

        foreach (var element in arr)
            model.SerializeObject(element, writer);
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        var array = (T)obj!;


        Serialize(array, writer);
    }

    public override T? Deserialize(ISerializeReader reader)
    {
        var isNull = reader.ReadByte();

        if(isNull == 0)
            return default;

        Span<byte> buffer = stackalloc byte[sizeof(int)];
        reader.ReadBytes(buffer); // Read length

        int arrayLength = BitConverter.ToInt32(buffer);

        if (reader.AvalibleAreaSize % arrayLength != 0)
            throw new Exception("Invalid data to deserealize in array.");

        var model = _arrayElementSerializeModel.Value;

        var arrResult = Array.CreateInstance(_arrayElementType.Value, arrayLength);
        
        for (int i = 0; i < arrayLength; i++)
            arrResult.SetValue(model.DeserializeToObject(reader), i);

        return (T)(object)arrResult;
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        return Deserialize(reader);
    }
}