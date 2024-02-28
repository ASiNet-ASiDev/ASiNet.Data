using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization.Generators.Tests;

[TestClass()]
public class ListModelsGeneratorTests
{
    [TestMethod()]
    public void GenerateModelTest()
    {
        var model = BinarySerializer.SharedSerializeContext.GetOrGenerate<List<int>>();
        
        var val = new List<int>() { 1, 2, 3, 4 };
        
        var buff = new byte[64];

        model.Serialize(val, (ArrayWriter)buff);

        var res = model.Deserialize((ArrayReader)buff);
        
        Assert.IsNotNull(res);
        Assert.AreEqual(val.Count, res!.Count);

        for (int i = 0; i < val.Count; i++)
        {
            Assert.AreEqual(val[i], res[i]);
        }
    }

    [TestMethod()]
    public void GenerateNullModelTest()
    {
        var model = BinarySerializer.SharedSerializeContext.GetOrGenerate<List<int>>();

        List<int> val = null;

        var buff = new byte[64];

        model.Serialize(val, (ArrayWriter)buff);

        var res = model.Deserialize((ArrayReader)buff);

        Assert.IsNull(res);
    }
}