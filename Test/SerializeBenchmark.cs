using System.Drawing;
using System.Text;
using System.Text.Json;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Attributes;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;
using BenchmarkDotNet.Attributes;
using MemoryPack;

namespace Test;

[MemoryDiagnoser(true)]
public class SerializeBenchmark
{
    private byte[] _buffer = new byte[40000];
    private byte[] _buffer2 = new byte[40000];

    private byte[] _mempBuf;

    
    private TestClass _instance = new();

    private IBinarySerializer _default;
    private IBinarySerializer _readonly;

    public SerializeBenchmark()
    {
        _default = BinarySerializer.NewDefaultSerializer();
        _readonly = BinarySerializer.NewReadonlySerializer();

        _instance.Ulong = (ulong)Random.Shared.Next();
        var buf = new byte[sizeof(char) * 1000];
        _instance.String = Encoding.UTF8.GetString(buf);

        int arrsize = 5000;
        _instance.Array = new int[arrsize];
        for (int i = 0; i < arrsize; i++) _instance.Array[i] = Random.Shared.Next();
        
        _instance.TestClassik = null;

        _default.Serialize(_instance, _buffer2);
        
        _mempBuf = MemoryPackSerializer.Serialize(_instance);
    }
    
    [Benchmark]
    public void DiSi_Default_SerializerTest()
    {
        _default.Serialize(_instance, _buffer);
    }

    [Benchmark]
    public void DiSi_Readonly_SerializerTest()
    {
        _readonly.Serialize(_instance, _buffer);
    }

    [Benchmark]
    public void MemoryPackSerializerTest()
    {
        MemoryPackSerializer.Serialize(_instance);
    }
    
    [Benchmark]
    public void DiSi_Default_DeserializerTest()
    {
        _default.Deserialize<TestClass>(_buffer2);
    }

    [Benchmark]
    public void DiSi_Readonly_DeserializerTest()
    {
        _readonly.Deserialize<TestClass>(_buffer2);
    }

    [Benchmark]
    public void MemoryPackDeserializerTest()
    {
        MemoryPackSerializer.Deserialize<TestClass>(_mempBuf);
    }
}

[PreGenerate]
[MemoryPackable]
public partial class TestClass
{
    public ulong Ulong { get; set; }
    public string? String { get; set; }
    public int[] Array { get; set; }
    public TestClass? TestClassik { get; set; }
}