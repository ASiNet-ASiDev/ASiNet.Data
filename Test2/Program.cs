using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.Arrays;

var buf = new byte[64];

BinarySerializer.Serialize(new T1(), buf);

Console.ReadLine();

class T1
{
    public int B { get; set; }
    public int A { get; set; }

    public int C { get; set; }

    public T1 D { get; set; }

    public int[] E { get; set; }
}