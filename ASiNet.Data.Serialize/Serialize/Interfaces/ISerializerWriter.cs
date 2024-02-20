using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Data.Serialize.Interfaces;
public interface ISerializerWriter
{
    public int TotalAreaSize { get; }
    public int AvalibleAreaSize { get; }
    public int FilledAreaSize { get; }

    public bool CanWriteSize(int size);

    public void WriteBytes(Span<byte> data);

    public void WriteByte(byte data);

}
