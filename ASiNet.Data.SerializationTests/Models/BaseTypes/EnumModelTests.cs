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
public class EnumModelTests
{
    enum TestEnum { A, B, C, D }

    [TestMethod()]
    public void SerializeTest()
    {
        var model = new EnumModel<TestEnum>();

        var test = TestEnum.C;

        var arr = new byte[sizeof(int)];
        model.Serialize(test, (ArrayWriter)arr);

        Assert.IsTrue((TestEnum)BitConverter.ToInt32(arr) == test);
    }

    [TestMethod()]
    public void DeserializeTest()
    {
        var model = new EnumModel<TestEnum>();

        var test = TestEnum.C;

        var arr = BitConverter.GetBytes((int)test);
        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(test == result);
    }
}