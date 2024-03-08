using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASiNet.Data.Serialization.Models.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.Data.Serialization.IO.Arrays;

namespace ASiNet.Data.Serialization.Models.Arrays.Tests;

[TestClass()]
public class DateTimeArrayModelTests
{
    [TestMethod()]
    public void SerializeTest()
    {
        byte[] data = new byte[128];
        var model = new DateTimeArrayModel();

        var val = new DateTime[] { DateTime.Now, DateTime.UtcNow };

        model.Serialize(val, (ArrayWriter)data);

        var res = model.Deserialize((ArrayReader)data);

        if (res is null)
            Assert.Fail();

        foreach (var item in val.Zip(res))
        {
            Assert.AreEqual(item, item);
        }
    }
}