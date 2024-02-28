using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Models;
using ASiNet.Data.Serialization.Generators;

namespace Test;

[MemoryDiagnoser(true)]
public class ListBm
{

    public ListBm()
    {
        
        var generator = new ListModelsGenerator();

        _autogenModel = generator.GenerateModel<List<int>>(BinarySerializer.SharedSerializeContext, BinarySerializer.Settings);

        _listModel = new();
    }

    public SerializeModel<List<int>> _autogenModel;

    public ListModel<List<int>> _listModel;

    public List<int> Test = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];

    private byte[] _buffer = new byte[512];

    [Benchmark]
    public List<int>? List_Test()
    {
        _listModel.Serialize(Test, (ArrayWriter)_buffer);

        return _listModel.Deserialize((ArrayReader)_buffer);
    }

    [Benchmark]
    public List<int>? AUTOGEN_List_Test()
    {
        _autogenModel.Serialize(Test, (ArrayWriter)_buffer);

        return _autogenModel.Deserialize((ArrayReader)_buffer);
    }
}
