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
public class CharModelTests
{
    [TestMethod()]
    public void SerializeTest()
    {
        var model = new CharModel();

        var arr = new byte[2];
        model.Serialize('A', (ArrayWriter)arr);

        Assert.IsTrue(BitConverter.ToChar(arr) == 'A');
    }

    [TestMethod()]
    public void DeserializeTest()
    {
        var model = new CharModel();

        var arr = new byte[2];

        BitConverter.TryWriteBytes(arr, 'A');

        var result = model.Deserialize((ArrayReader)arr);


        Assert.IsTrue(result == 'A');
    }
}