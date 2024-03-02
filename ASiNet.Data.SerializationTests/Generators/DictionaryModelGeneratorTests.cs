using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Generators;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.SerializationTests.Generators;

[TestClass()]
public class DictionaryModelGeneratorTests
{

    [TestMethod()]
    public void GenerateModelTest()
    {
        var generator = new DictionaryModelsGenerator();

        var model = generator.GenerateModel<Dictionary<int, string>>(BinarySerializer.SerializeContext, BinarySerializer.Settings);

        var test = new Dictionary<int, string>
        {
            { 1, "1" }, { 2, "2" }, { 3, "3" } ,{ 4, "4" }, { 5, "5" }
        };

        var buff = new byte[512];
        var wr = (ArrayWriter)buff;
        var rd = (ArrayReader)buff;

        model.Serialize(test, wr);

        var res = model.Deserialize(rd);

        Assert.AreEqual(model.ObjectSerializedSize(test), wr.FilledBytes);

        Assert.IsNotNull(res);

        Assert.IsTrue(test.Count == res.Count);

        foreach (var item in test.Keys.Zip(res.Keys))
        {
            Assert.AreEqual(item.First, item.Second);
        }

        foreach (var item in test.Values.Zip(res.Values))
        {
            Assert.AreEqual(item.First, item.Second);
        }
    }

    [TestMethod()]
    public void GenerateModelTestNullable()
    {
        var generator = new DictionaryModelsGenerator();

        var model = generator.GenerateModel<Dictionary<int, string>>(BinarySerializer.SerializeContext, BinarySerializer.Settings);

        Dictionary<int, string> test = null;

        var buff = new byte[512];
        var wr = (ArrayWriter)buff;
        var rd = (ArrayReader)buff;

        model.Serialize(test, wr);

        var res = model.Deserialize(rd);

        Assert.AreEqual(model.ObjectSerializedSize(test), wr.FilledBytes);

        Assert.IsNull(res);

        Assert.IsTrue(test.Count == res.Count);

    }
}
