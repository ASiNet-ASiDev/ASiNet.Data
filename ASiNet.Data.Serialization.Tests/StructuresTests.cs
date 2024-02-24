using System.Numerics;
using ASiNet.Data.Serialization.IO.Arrays;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASiNet.Data.Serialization.Tests;

[TestClass]
[TestSubject(typeof(StructuresTests))]
public class StructuresTests
{
    struct A
    {
        public int B { get; set; }
        public int C { get; set; }
    }
    [TestMethod]
    public void NotEqualStruct()
    {
        var a = new A() { B = 26666, C = 1231232 };
        var buffer = new byte[60000];
        BinarySerializer.Serialize(a, (ArrayWriter)buffer);
        var a2 = BinarySerializer.Deserialize<A>((ArrayReader)buffer);
        Assert.IsTrue( a.B == a2.B && a.C == a2.C );
    }
    
    [TestMethod]
    public void NumericVector4truct()
    {
        var a = new Vector4(1231, 532, 12, 412);
        var buffer = new byte[60000];
        BinarySerializer.Serialize(a, (ArrayWriter)buffer);
        var a2 = BinarySerializer.Deserialize<Vector4>((ArrayReader)buffer);
        Assert.IsTrue( a.Y == a2.Y && a.W == a2.W && a.X == a2.X && a.Z == a2.Z);
    }
}