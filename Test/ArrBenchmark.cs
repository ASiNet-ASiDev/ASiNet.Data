using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models;
using BenchmarkDotNet.Attributes;

namespace Test;

[MemoryDiagnoser(true)]
public class ArrBenchmark
{
    public ArrBenchmark()
    {
        BinarySerializer.Settings.UseDefaultUnsafeArraysModels = false;
        AutogenModel = BinarySerializer.SerializeContext.GetOrGenerate<int[]>();

        Model = new();
    }

    public SerializeModel<int[]> AutogenModel { get; set; }

    public ArrayModel<int[]> Model { get; set; }

    public byte[] Buff = new byte[ushort.MaxValue];

    public int[] Test = [1];


    [Benchmark]
    public int[]? Default_Array_SD()
    {
        Model.Serialize(Test, (ArrayWriter)Buff);
        return Model.Deserialize((ArrayReader)Buff);
    }

    [Benchmark]
    public int[]? Autogen_Array_SD()
    {
        AutogenModel.Serialize(Test, (ArrayWriter)Buff);
        return AutogenModel.Deserialize((ArrayReader)Buff);
    }
}