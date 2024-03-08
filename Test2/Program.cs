using ASiNet.Data.Serialization;

var buffer = new byte[128];

var p = new Person() 
{ 
    Id = 10, 
    Test = new Test()
};

var type = p.Test.GetType(); 

Console.WriteLine();


public class Person
{
    public int Id { get; set; }

    public ITest Test { get; set; }
}

public interface ITest
{

}

public  class Test : ITest
{
    public int A {  get; set; }

    public int B { get; set; }
}