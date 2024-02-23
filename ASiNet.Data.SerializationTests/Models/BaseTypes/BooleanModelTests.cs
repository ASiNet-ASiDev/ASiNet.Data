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
public class BooleanModelTests
{
    [TestMethod()]
    public void SerializeTest()
    {
        var model = new BooleanModel();

        var arr = new byte[1];
        model.Serialize(true, (ArrayWriter)arr);

        Assert.IsTrue(arr[0] == 1);
    }

    [TestMethod()]
    public void DeserializeTest()
    {
        var model = new BooleanModel();

        var arr = new byte[1] { 1 };
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result);
    }
}