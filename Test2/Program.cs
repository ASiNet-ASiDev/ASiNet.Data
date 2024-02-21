
using ASiNet.Data.Base.Serialization.Models;

var context = new ObjectModelsContext();

context.GetOrGenerate<C1>();

Console.ReadLine();


class C1
{
    public C1 V1 { get; set; }
    public C2 V2 { get; set; }
}

class C2
{
    public C3 V1 { get; set; }
}

class C3
{

}