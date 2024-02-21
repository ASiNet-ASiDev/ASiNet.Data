using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Base.Models;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Console.ReadLine();

enum Test : int
{
    a1 = 2,
}