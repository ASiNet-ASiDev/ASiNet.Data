using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Base.Models;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

try
{
    var buffer = new byte[30000];

    var guid = Guid.NewGuid();
    Console.WriteLine(guid);
    
    BinarySerializer.Serialize(guid, (ArrayWriter)buffer);

    var res = BinarySerializer.Deserialize<Guid>((ArrayReader)buffer);

    Console.WriteLine(string.Join(' ', res));
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

Console.ReadLine();
