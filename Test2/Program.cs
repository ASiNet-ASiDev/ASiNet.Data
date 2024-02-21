using ASiNet.Data.Base.Serialization.Models;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Base.Models;
using ASiNet.Data.Serialization.IO.Arrays;

var gen = new SerializerModelsGenerator();

var omContext = new ObjectModelsContext();
var omGen = new ObjectModelsGenerator();
var model = omGen.GenerateModel<T1>();

var sc = SerializerContext.FromDefaultModels(omContext);

var des = gen.GenerateDeserializeLambda(model, sc);
var ser = gen.GenerateSerializeLambda(model, sc);

var buffer = new byte[32];

ser.Invoke(new T1() { A = 11, B = 22, C = 33 }, (ArrayWriter)buffer);

var result = des.Invoke((ArrayReader)buffer);


Console.ReadLine();

class T1
{
    public int A { get; set; }

    public int B { get; set; }

    public int C { get; set; }
}