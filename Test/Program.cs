using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Test;

BenchmarkRunner.Run<SerializeBenchmark>();
    
Console.ReadLine();