using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes.Tests;

[TestClass()]
public class GuidModelTests
{
    [TestMethod()]
    public void SerializeTest()
    {
        var model = new GuidModel();

        var test = Guid.NewGuid();

        var arr = new byte[sizeof(decimal)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(new Guid(arr) == test);
    }

    [TestMethod()]
    public void DeserializeTest()
    {
        var model = new GuidModel();

        var test = Guid.NewGuid();

        var arr = test.ToByteArray();
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }
}