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
public class AllIntModelTests
{
    [TestMethod()]
    public void SerializeInt16Test()
    {
        var model = new Int16Model();

        var test = (short)45;

        var arr = new byte[sizeof(short)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToInt16(arr) == test);
    }

    [TestMethod()]
    public void DeserializeInt16Test()
    {
        var model = new Int16Model();

        var test = (short)45;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }

    [TestMethod()]
    public void SerializeUInt16Test()
    {
        var model = new UInt16Model();

        var test = (ushort)45;

        var arr = new byte[sizeof(short)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToUInt16(arr) == test);
    }

    [TestMethod()]
    public void DeserializeUInt16Test()
    {
        var model = new UInt16Model();

        var test = (ushort)45;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }

    [TestMethod()]
    public void SerializeInt32Test()
    {
        var model = new Int32Model();

        var test = 45;

        var arr = new byte[sizeof(int)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToInt32(arr) == test);
    }

    [TestMethod()]
    public void DeserializeInt32Test()
    {
        var model = new Int32Model();

        var test = 45;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }

    [TestMethod()]
    public void SerializeUInt32Test()
    {
        var model = new UInt32Model();

        uint test = 45;

        var arr = new byte[sizeof(int)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToUInt32(arr) == test);
    }

    [TestMethod()]
    public void DeserializeUInt32Test()
    {
        var model = new UInt32Model();

        uint test = 45;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }

    [TestMethod()]
    public void SerializeInt64Test()
    {
        var model = new Int64Model();

        long test = 45;

        var arr = new byte[sizeof(long)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToInt64(arr) == test);
    }

    [TestMethod()]
    public void DeserializeInt64Test()
    {
        var model = new Int64Model();

        long test = 45;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }

    [TestMethod()]
    public void SerializeUInt64Test()
    {
        var model = new UInt64Model();

        ulong test = 45;

        var arr = new byte[sizeof(long)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToUInt64(arr) == test);
    }

    [TestMethod()]
    public void DeserializeUInt64Test()
    {
        var model = new UInt64Model();

        ulong test = 45;

        var arr = BitConverter.GetBytes(test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }
}