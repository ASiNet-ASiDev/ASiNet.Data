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
public class DoubleModelTests
{
    [TestMethod()]
    public void SerializeDoubleTest()
    {
        var model = new DoubleModel();

        var test = 1.1D;

        var arr = new byte[sizeof(double)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToDouble(arr) == test);
    }

    [TestMethod()]
    public void DeserializeDoubleTest()
    {
        var model = new DoubleModel();

        var test = 1.1D;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }

    [TestMethod()]
    public void SerializeSingleTest()
    {
        var model = new SingleModel();

        var test = 1.1F;

        var arr = new byte[sizeof(double)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToSingle(arr) == test);
    }

    [TestMethod()]
    public void DeserializeSingleTest()
    {
        var model = new SingleModel();

        var test = 1.1F;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }
}