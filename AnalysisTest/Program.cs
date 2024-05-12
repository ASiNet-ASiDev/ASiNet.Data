using ASiNet.Data.Serialization.Attributes;
using ASiNet.Data.Serialization.Interfaces;

namespace AnalysisTest;

public class Program
{
    static void Main(string[] args)
    {
        Console.ReadKey();

    }

}


[PreGenerate]
public partial class At : ISerializeModel<At>
{

}


[PreGenerate]
public class At2
{

}