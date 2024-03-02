using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models;
using BenchmarkDotNet.Attributes;

namespace Test;

[MemoryDiagnoser(true)]
public class DictionaryBenchmark
{
    public DictionaryBenchmark()
    {
        BinarySerializer.Settings.UseDefaultUnsafeArraysModels = false;
        AutogenModel = BinarySerializer.SerializeContext.GetOrGenerate<Dictionary<int, string>>();

        Model = new();
    }

    public SerializeModel<Dictionary<int, string>> AutogenModel { get; set; }

    public DictionaryModel<Dictionary<int, string>> Model { get; set; }

    public byte[] Buff = new byte[ushort.MaxValue];

    public Dictionary<int, string> Test = new() { { 1, "1"}, { 2, "2"}, {  3, "3"}, { 4, "4"},
        { 5, "1"}, { 6, "2"}, {  7, "3"}, { 8, "4"},
        { 12, "1"}, { 11, "2"}, {  10, "3"}, { 9, "4"},
        { 13, "1"}, { 14, "2"}, {  15, "3"}, { 16, "4"}};


    [Benchmark]
    public Dictionary<int, string>? Default_Array_SD()
    {
        Model.Serialize(Test, (ArrayWriter)Buff);
        return Model.Deserialize((ArrayReader)Buff);
    }

    [Benchmark]
    public Dictionary<int, string>? Autogen_Array_SD()
    {
        AutogenModel.Serialize(Test, (ArrayWriter)Buff);
        return AutogenModel.Deserialize((ArrayReader)Buff);
    }
}