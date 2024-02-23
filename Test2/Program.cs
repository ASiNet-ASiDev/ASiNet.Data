using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;

var buf = new byte[64];

var count = BinarySerializer.Serialize<T1>(new T1() { A = 10, B = 20, C = 30, E = [4, 5, 6, 7, 8, 9, 10], D = new() { A = 11, B = 12, C = 13, } }, (ArrayWriter)buf);

var res = BinarySerializer.Deserialize<T1>((ArrayReader)buf);

Console.ReadLine();

class T1
{
    public int B { get; set; }
    public int A { get; set; }

    public int C { get; set; }

    public T1 D { get; set; }

    public int[] E { get; set; }
}