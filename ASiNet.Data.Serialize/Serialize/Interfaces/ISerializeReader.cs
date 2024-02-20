using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Data.Serialize.Interfaces;
public interface ISerializeReader
{
    public int TotalAreaSize { get; }
    public int AvalibleAreaSize { get; }
    public int FilledAreaSize { get; }

    public bool CanReadSize(int size);

    public void ReadBytes(Span<byte> data);

    public byte ReadByte();
}
