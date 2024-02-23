using System.Data.SqlTypes;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;
public class NullableTypesModel<T> : BaseSerializeModel<T>
{

    private Lazy<Type> _underlyingType = new(() => Nullable.GetUnderlyingType(typeof(T))!);

    private Lazy<ISerializeModel> _underlyingSerializeModel = 
        new(() => BinarySerializer.SharedSerializeContext.GetOrGenerate(Nullable.GetUnderlyingType(typeof(T))!));

    public override void Serialize(T obj, ISerializeWriter writer)
    {
        if(obj is null)
        {

        }
        throw new NotImplementedException();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        throw new NotImplementedException();
    }

    public override T? Deserialize(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }
}
