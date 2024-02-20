using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;

public class DoubleModel : BaseSerializeModel<double>
{
    public override double Deserealize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(double)))
        {
            var buffer = (stackalloc byte[sizeof(double)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToDouble(buffer);
        }
        throw new Exception();
    }

    public override object? Deserialize(ISerializeReader reader)
    {
        if (reader.CanReadSize(sizeof(double)))
        {
            var buffer = (stackalloc byte[sizeof(double)]);
            reader.ReadBytes(buffer);
            return BitConverter.ToDouble(buffer);
        }
        throw new Exception();
    }

    public override void Serealize(double obj, ISerializerWriter writer)
    {
        var buffer = (stackalloc byte[sizeof(double)]);
        if (obj.TryToBytes(buffer))
        {
            writer.WriteBytes(buffer);
            return;
        }

        throw new Exception();
    }

    public override void Serialize(object? obj, ISerializerWriter writer)
    {
        if (obj is double value)
        {
            var buffer = (stackalloc byte[sizeof(double)]);
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
