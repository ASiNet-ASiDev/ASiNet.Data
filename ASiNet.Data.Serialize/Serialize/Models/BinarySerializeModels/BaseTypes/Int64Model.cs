using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class Int64Model : BaseSerializeModel<long>
{
    public override long Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(long)))
        {
            var buffer = (stackalloc byte[sizeof(long)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToInt64(buffer);
        }
        throw new Exception();
    }

    public override object? DeserializeToObject(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(long)))
        {
            var buffer = (stackalloc byte[sizeof(long)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToInt64(buffer);
        }
        throw new Exception();
    }

    public override void Serialize(long obj, ISerializeWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(long)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer)
    {
        if (obj is long value)
        {
            var buffer = (stackalloc byte[sizeof(long)]);
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