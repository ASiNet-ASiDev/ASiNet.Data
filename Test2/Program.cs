using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Base.Models;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;

var buffer = new byte[100];

int[] src = [20, 20, 30, 40];

var arr = new ArrayModel<int[]>();

arr.Serialize(src, (ArrayWriter)buffer);

var res = arr.Deserialize((ArrayReader)buffer);

Console.ReadLine();
