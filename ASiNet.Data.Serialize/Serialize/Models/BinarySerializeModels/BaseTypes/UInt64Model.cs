using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class UInt64Model : BaseSerializeModel<ulong>
{
    public override ulong Deserealize(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override object? Deserialize(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Serealize(ulong obj, ISerializerWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void Serialize(object? obj, ISerializerWriter writer)
    {
        if (obj is ulong value)
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
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