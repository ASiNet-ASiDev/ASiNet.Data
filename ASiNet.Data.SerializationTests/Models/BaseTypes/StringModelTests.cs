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
public class StringModelTests
{
    [TestMethod()]
    public void SerializeDeserializeTest()
    {
        var model = new StringModel();

        var test = "Hello world";

        var arr = new byte[300];
        model.Serialize(test, (ArrayWriter)arr);

        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result == test);
    }
}