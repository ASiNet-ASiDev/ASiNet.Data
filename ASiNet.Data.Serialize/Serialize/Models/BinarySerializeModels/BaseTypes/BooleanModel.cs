using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class BooleanModel : BaseSerializeModel<bool>
{
    public override bool Deserealize(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override object? Deserialize(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Serealize(bool obj, ISerializerWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(bool)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void Serialize(object? obj, ISerializerWriter writer)
    {
        if (obj is bool value)
        {
            var buffer = (stackalloc byte[sizeof(bool)]);
            if (value.TryToBytes(buffer))
            {
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }
}
