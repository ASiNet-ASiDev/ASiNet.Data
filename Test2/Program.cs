using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.Arrays;

Console.WriteLine(BinarySerializer.GetSize(new T1() { }));

Console.ReadLine();

// 1
struct T1
{
    
    public A? V { get; set; }
}

enum A : int
{
    A, B, C, D
}