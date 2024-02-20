
using System.Linq.Expressions;
using ASiNet.Data;
using ASiNet.Data.Base.Models;
using ASiNet.Data.Serialize;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

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