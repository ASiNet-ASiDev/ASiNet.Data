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
public class DateTimeModelTests
{
    [TestMethod()]
    public void SerializeTest()
    {
        var model = new DateTimeModel();

        var test = DateTime.Now;

        var arr = new byte[sizeof(long)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue(DateTime.FromBinary(BitConverter.ToInt64(arr)) == test);
    }

    [TestMethod()]
    public void DeserializeTest()
    {
        var model = new DateTimeModel();

        var test = DateTime.Now;

        var arr = BitConverter.GetBytes(test.ToBinary());
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result == test);
    }
}