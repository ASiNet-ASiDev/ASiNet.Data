using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization.Generators.Tests;

[TestClass()]
public class NullableTypesGeneratorTests
{
    [TestMethod()]
    public void GenerateSerializeLambdaTest()
    {
        var generator = new NullableTypesGenerator();

        var res = generator.GenerateSerializeLambda<int?>(BinarySerializer.SharedSerializeContext, BinarySerializer.Settings);

    }

    [TestMethod()]
    public void GenerateDeserializeLambdaTest()
    {
        var generator = new NullableTypesGenerator();

        var res = generator.GenerateDeserializeLambda<int?>(BinarySerializer.SharedSerializeContext, BinarySerializer.Settings);
    }

    [TestMethod()]
    public void NullableTest()
    {
        var generator = new NullableTypesGenerator();


        int? val = 10;

        var model = generator.GenerateModel<int?>(BinarySerializer.SharedSerializeContext, BinarySerializer.Settings);

        var buff = new byte[16];
        
        model.Serialize(val, (ArrayWriter)buff);
        var res = model.Deserialize((ArrayReader)buff);

        Assert.AreEqual(val, res);

        int? val2 = null;

        var buff2 = new byte[16];

        model.Serialize(val2, (ArrayWriter)buff2);
        var res2 = model.Deserialize((ArrayReader)buff2);

        Assert.AreEqual(val, res);

    }
}