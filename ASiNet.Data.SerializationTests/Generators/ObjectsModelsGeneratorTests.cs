using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.Contexts;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization.Generators.Tests;

[TestClass()]
public class ObjectsModelsGeneratorTests
{
    [TestMethod()]
    public void Test1()
    {
        //var context = new DefaultSerializerContext(new());
        
        //context.GenerateModel<TestInterfaceInstance>();

        //var model = context.GenerateModel<Test1>();

        //var buff = new byte[128];

        //var val = new Test1()
        //{
        //    //Property1 = new TestInterfaceInstance() { A = 10, B = 15 }
        //};

        //model.Serialize(val, (ArrayWriter)buff);

        //var res = model.Deserialize((ArrayReader)buff);

    }
}

public class Test1
{
    public ITestInterface Property1 { get; set; }
}

public interface ITestInterface
{

}

public class TestInterfaceInstance : ITestInterface
{
    public int A { get; set; }

    public int B { get; set; }
}