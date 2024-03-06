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
public class EnumsModelGeneratorTests
{
    enum A : sbyte
    { A, B, C }

    enum B : byte
    { A, B, C }

    enum C : short
    { A, B, C }

    enum D : ushort
    { A, B, C }

    enum E : int
    { A, B, C }

    enum F : uint
    { A, B, C }

    enum G : long
    { A, B, C }

    enum H : ulong
    { A, B, C }

    [TestMethod()]
    public void GenerateModelTest()
    {
        var generator = new EnumsModelsGenerator();
        
        var aModel = generator.GenerateModel<A>(BinarySerializer.SerializeContext, new());

        var bModel = generator.GenerateModel<B>(BinarySerializer.SerializeContext, new());

        var cModel = generator.GenerateModel<C>(BinarySerializer.SerializeContext, new());

        var dModel = generator.GenerateModel<D>(BinarySerializer.SerializeContext, new());

        var eModel = generator.GenerateModel<E>(BinarySerializer.SerializeContext, new());

        var fModel = generator.GenerateModel<F>(BinarySerializer.SerializeContext, new());

        var gModel = generator.GenerateModel<G>(BinarySerializer.SerializeContext, new());

        var hModel = generator.GenerateModel<H>(BinarySerializer.SerializeContext, new());

        var buf = new byte[64];

        var aValue = A.B;
        aModel.Serialize(aValue, (ArrayWriter)buf);
        var aResult = aModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(aValue, aResult);

        var bValue = B.B;
        bModel.Serialize(bValue, (ArrayWriter)buf);
        var bResult = bModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(bValue, bResult);

        var cValue = C.B;
        cModel.Serialize(cValue, (ArrayWriter)buf);
        var cResult = cModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(cValue, cResult);

        var dValue = D.B;
        dModel.Serialize(dValue, (ArrayWriter)buf);
        var dResult = dModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(dValue, dResult);

        var eValue = E.B;
        eModel.Serialize(eValue, (ArrayWriter)buf);
        var eResult = eModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(eValue, eResult);

        var fValue = F.B;
        fModel.Serialize(fValue, (ArrayWriter)buf);
        var fResult = fModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(fValue, fResult);

        var gValue = G.B;
        gModel.Serialize(gValue, (ArrayWriter)buf);
        var gResult = gModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(gValue, gResult);

        var hValue = H.B;
        hModel.Serialize(hValue, (ArrayWriter)buf);
        var hResult = hModel.Deserialize((ArrayReader)buf);
        Assert.AreEqual(hValue, hResult);

    }
}