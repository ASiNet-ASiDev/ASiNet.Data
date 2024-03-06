using BenchmarkDotNet.Running;
using Test;

BenchmarkRunner.Run<SerializeBenchmark>();

Console.ReadLine();