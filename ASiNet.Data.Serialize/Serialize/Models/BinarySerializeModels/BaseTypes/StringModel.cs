using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialize.Interfaces;

namespace ASiNet.Data.Serialize.Models.BinarySerializeModels.BaseTypes;
public class StringModel : BaseSerializeModel<string>
{
    public override string Deserealize(ISerializeReader reader)
    {
        if(reader.CanReadSize(sizeof(int)))
        {
            var sizeBuffer = (stackalloc byte[sizeof(int)]);
            reader.ReadBytes(sizeBuffer);
            var strBytesSize = BitConverter.ToInt32(sizeBuffer);
            var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
        }
        throw new Exception();
    }

    public override object? Deserialize(ISerializeReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Serealize(string obj, ISerializerWriter writer)
    {
        var strBytesSize = Encoding.UTF8.GetByteCount(obj);
        var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
        if (obj.TryToBytes(buffer))
        {
            var strSzBytes = (stackalloc byte[sizeof(int)]);
            BitConverter.TryWriteBytes(strSzBytes, strBytesSize);
            writer.WriteBytes(strSzBytes);
            writer.WriteBytes(buffer);
            return;
        }
        throw new Exception();
    }

    public override void Serialize(object? obj, ISerializerWriter writer)
    {
        if (obj is string value)
        {
            var strBytesSize = Encoding.UTF8.GetByteCount(value);
            var buffer = strBytesSize > ushort.MaxValue ? (new byte[strBytesSize]) : (stackalloc byte[strBytesSize]);
            if (value.TryToBytes(buffer))
            {
                var strSzBytes = (stackalloc byte[sizeof(int)]);
                BitConverter.TryWriteBytes(strSzBytes, strBytesSize);
                writer.WriteBytes(strSzBytes);
                writer.WriteBytes(buffer);
                return;
            }
            throw new Exception();
        }
        throw new Exception();
    }
}

