using System.Diagnostics.Metrics;
using System.Text;
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;
using Test;

BinarySerializer.SerializeContext.AddModel(new PersonModel());

Console.ReadLine();


public class Person
{
    public int Id { get; set; }

    public int Age { get; set; }

    public string? Name { get; set; }
}

public class PersonModel : SerializeModelBase<Person>
{

    public override int ObjectSerializedSize(Person? obj)
    {
        if(obj is null)
            return 1; // возвращаем длину в виде 1 Nullable байта.
        var result = 1 + 1 + 4 + 4; // Nullable байт + Name Nullable байт + int Id + int Age

        if (obj.Name is null) // Возвращает размер объекта без попыток посчитать размер строки, если строка null
            return result;

        result += Encoding.UTF8.GetByteCount(obj.Name); // дописывает длину строки к размеру объекта

        return result;
    }

    public override void Serialize(Person? obj, ISerializeWriter writer)
    {
        if(obj is null)
        {
            writer.WriteByte(0); // Пишем Nullable байт указывающий что данный обьект null
            return;
        }
        else
            writer.WriteByte(1); // Пишем Nullable байт указывающий что данный обьект не null

        writer.WriteBytes(BitConverter.GetBytes(obj.Id)); // Записываем Id
        writer.WriteBytes(BitConverter.GetBytes(obj.Age)); // записываем Age

        if(obj.Name != null)
        {
            writer.WriteByte(1); // Пишем Nullable байт указывающий что данная строка не null
            writer.WriteBytes(BitConverter.GetBytes(obj.Name.Length)); // Пишем длину строки
            writer.WriteBytes(Encoding.UTF8.GetBytes(obj.Name)); // Пишем строку
        }
        else
            writer.WriteByte(0); //Пишем Nullable байт указывающий что данная строка null
    }

    public override Person? Deserialize(ISerializeReader reader)
    {
        if(reader.ReadByte() == 0) // Читаем Nullable байт и если 0, то возвращаем null
            return null;
        
        var buff = new byte[4];
        reader.ReadBytes(buff); // читаем id
        var id = BitConverter.ToInt32(buff);

        reader.ReadBytes(buff); // читаем age
        var age = BitConverter.ToInt32(buff);

        if(reader.ReadByte() == 0) // читаем Nullable байт строки
            return new Person() { Id = id, Age = age };
        
        reader.ReadBytes(buff); // читаем длину строки
        var length = BitConverter.ToInt32(buff);

        var strBuff = new byte[length];
        reader.ReadBytes(strBuff); // читаем строку

        var name = Encoding.UTF8.GetString(strBuff);

        return new Person() { Id = id, Name = name, Age = age };
    }

    public override void SerializeObject(object? obj, ISerializeWriter writer) =>
        Serialize((Person?)obj, writer);

    public override object? DeserializeToObject(ISerializeReader reader) =>
        Deserialize(reader);
}
