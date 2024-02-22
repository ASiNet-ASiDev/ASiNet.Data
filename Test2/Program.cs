using System.Collections.Immutable;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;

var buffer = new byte[100];

SortedDictionary<string, int> src = new(){{"H1", 65}, {"H2", 445}};

var arr = new DictionaryModel<SortedDictionary<string, int>>();

arr.Serialize(src, (ArrayWriter)buffer);

var res = arr.Deserialize((ArrayReader)buffer);

Console.WriteLine(string.Join(' ', res));

Console.ReadLine();