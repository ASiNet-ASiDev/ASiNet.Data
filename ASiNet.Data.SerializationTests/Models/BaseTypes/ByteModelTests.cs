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
    public void SerializeByteTest()
    {
        var model = new ByteModel();

        var arr = new byte[1];
        model.Serialize(145, (ArrayWriter)arr);

        Assert.IsTrue(arr[0] == 145);
    }

    [TestMethod()]
    public void DeserializeByteTest()
    {
        var model = new ByteModel();

        var arr = new byte[1] { 145 };
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result == 145);
    }

    [TestMethod()]
    public void SerializeSByteTest()
    {
        var model = new SByteModel();

        var arr = new byte[1];
        model.Serialize(98, (ArrayWriter)arr);

        Assert.IsTrue((sbyte)arr[0] == (sbyte)98);
    }

    [TestMethod()]
    public void DeserializeSByteTest()
    {
        var model = new SByteModel();

        var arr = new byte[1] { ((byte)(sbyte)98) };
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result == 98);
    }
}