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
public class ByteModelTests
{
    [TestMethod()]
    public void SerializeTest()
    {
        var model = new ByteModel();

        var arr = new byte[1];
        model.Serialize(145, (ArrayWriter)arr);

        Assert.IsTrue(arr[0] == 145);
    }

    [TestMethod()]
    public void DeserializeTest()
    {
        var model = new ByteModel();

        var arr = new byte[1] { 145 };
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result == 145);
    }
}