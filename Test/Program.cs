using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


var buf = new byte[64];

int? val = null;

var model = new NullableTypesModel<int?>();

model.Serialize(val, (ArrayWriter)buf);

var res = model.Deserialize((ArrayReader)buf);

Console.ReadLine();