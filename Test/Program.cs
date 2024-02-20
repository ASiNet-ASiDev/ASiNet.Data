
using System.Linq.Expressions;
using ASiNet.Data;
using ASiNet.Data.Base.Models;
using ASiNet.Data.Serialize;
using ASiNet.Data.Serialize.ArrayIO;
using ASiNet.Data.Serialize.Interfaces;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


var buffer = new byte[] {25, 26, 27, 99, 31};
ISerializeReader reader = new ArrayReader(buffer);

Console.WriteLine(reader.CanReadSize(3));
Span<byte> buf = stackalloc byte[3];
reader.ReadBytes(buf);
Console.WriteLine(string.Join(' ', buf.ToArray()));

Console.WriteLine(reader.CanReadSize(3));
Span<byte> buf1 = stackalloc byte[3];
reader.ReadBytes(buf1);
Console.WriteLine(string.Join(' ', buf1.ToArray()));



Console.ReadKey();

return;
var omc = new ObjectModelsContext();

var sec = new SerializerContext(omc);

var model = sec.GetOrGenerate<T1>();

//BenchmarkRunner.Run<BenchmarkTest>();

Console.ReadLine();


[MemoryDiagnoser()]
public class BenchmarkTest
{
    public BenchmarkTest()
    {
        SetValuesDelegate = _generator.GenerateSetLambda<T1>();
        GetValuesDelegate = _generator.GenerateGetLambda<T1>();
    }

    public ObjectModelsGenerator _generator = new();

    public SetValuesDelegate<T1> SetValuesDelegate;
    public GetValuesDelegate<T1> GetValuesDelegate;


    private T1 _test = new();

    [Benchmark]
    public void GetModelPrors()
    {
        _ = GetValuesDelegate.Invoke(_test);
    }


    [Benchmark]
    public void SetModelPrors()
    {
        SetValuesDelegate.Invoke(_test, 1, 2, 3, 4, 5);
    }

    private void Callback(object? value)
    {

    }
}


public class T1
{
    public int I0 { get; set; }
    public int I1 { get; set; }
    public int I2 { get; set; }
    public int I3 { get; set; }
    public int I4 { get; set; }
}