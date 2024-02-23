using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.IO.Arrays;
using ASiNet.Data.Serialization.Models.BinarySerializeModels.BaseTypes;

namespace ASiNet.Data.Serialization.Models.Tests;

[TestClass()]
public class NullableTypesModelTests
{
    [TestMethod()]
    public void SerializeDeserializeTest()
    {
        var model = new NullableTypesModel<int?>();

        int? test = 14;

        var arr = new byte[300];
        model.Serialize(test, (ArrayWriter)arr);

        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result == test);
    }

    [TestMethod()]
    public void SerializeDeserializeNullTest()
    {
        var model = new NullableTypesModel<int?>();

        int? test = 14;

        var arr = new byte[300];
        model.Serialize(test, (ArrayWriter)arr);

        var result = model.Deserialize((ArrayReader)arr);

        Assert.IsTrue(result == test);
    }
}