using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Base.Models;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;

var arr = new byte[1000];

Console.WriteLine(BinarySerializer.Serialize(new T1() { A = 11, B = 22, C = 33, D = new() { A2 = Guid.NewGuid(), A1 = "12345" } }, (ArrayWriter)arr));


var res = BinarySerializer.Deserialize<T1>((ArrayReader)arr);


Console.ReadLine();

class T1
{
    public int? A { get; set; }

    public int B { get; set; }

    public int C { get; set; }

    public T2? D { get; set; }
}

class T2
{
    public string A1 { get; set; }

    public Guid A2 { get; set; }
}