using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class UInt64Model : BaseSerializeModel<ulong>
{
    public override ulong Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(ulong)))
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt64(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(ulong)))
        {
            var buffer = (stackalloc byte[sizeof(ulong)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt64(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(ulong obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(ulong)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
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