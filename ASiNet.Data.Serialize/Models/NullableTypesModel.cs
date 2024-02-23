using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Interfaces;

namespace ASiNet.Data.Serialization.Models;
public class NullableTypesModel<T> : BaseSerializeModel<T>
{
    public override T? Deserialize(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Serialize(T obj, ISerializeWriter writer)
    {
        throw new NotImplementedException();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        throw new NotImplementedException();
    }
}
