using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class UInt32Model : BaseSerializeModel<uint>
{
    public override uint Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(uint)))
        {
            var buffer = (stackalloc byte[sizeof(uint)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt32(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(uint)))
        {
            var buffer = (stackalloc byte[sizeof(uint)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToUInt32(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(uint obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(uint)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is uint value)
        {
            var buffer = (stackalloc byte[sizeof(uint)]);
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