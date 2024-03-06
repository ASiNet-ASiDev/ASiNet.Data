
using ASiNet.Data.Serialization;

var v = new A() { DT = DateTime.Now, TS = TimeSpan.FromSeconds(5)};
var buff = new byte[128];

BinarySerializer.Serialize<A>(v, buff);

var result = BinarySerializer.Deserialize<A>(buff);

Console.WriteLine();


class A
{
    public TimeSpan TS { get; set; }

    public DateTime DT { get; set; }
}