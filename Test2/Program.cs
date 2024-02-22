using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;

var buf = new byte[64];

BinarySerializer.Serialize<T1>(new T1() { A = 10, B = 20, C = 30, D = new() { A = 30, B = 40, C = 50 } }, (ArrayWriter)buf);

var res = BinarySerializer.Deserialize<T1>((ArrayReader)buf);

Console.ReadLine();

class T1
{
    public int A { get; set; }

    public int B { get; set; }

    public int C { get; set; }

    public T1 D { get; set; }
}