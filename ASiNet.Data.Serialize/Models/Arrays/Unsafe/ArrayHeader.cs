using System.Runtime.InteropServices;

namespace ASiNet.Data.Serialization.Models.Arrays.Unsafe;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct ArrayHeader
{
    public UIntPtr type;
    public UIntPtr length;
}