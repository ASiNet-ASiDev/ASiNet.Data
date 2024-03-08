# ASiNet.Data.Serialization

ASiNet.Data.Serialization - простая и понятная в использовании библиотека для бинарной сериализации данных.

Сериализатор способен рекурсивно сериализовать и десериализовать __структуры и классы__ любой сложность, а также запоминать строение вашего типа, для экономии времени и памяти при дальнейшей сериализации.

Сериализатор был оптимизирован с помощью использования _unsafe_ кода для более быстрой сериализации как просто базовых типов, так и больших массивов из них.

При сериализации больших данных аллоцируется минимальное количество памяти, что является большим плюсом и позволяет производить сериализацию и десериализацию неопределенное количество раз.

## Поддерживаемые типы:
* `Byte`, `SByte`, `Int16`, `Int32`, `Int64`, `UInt16`, `UInt32`, `UInt64`, `Single`, `Double`, `Boolean`, `Char`, `String`
* `Arrays`
* `Dicitonary<TKey, TValue>`, `List<T>`
* `DateTime`, `Guid`, `Enum's`
* `Nullable<T>`
* `interfaces`, `abstract classes`
А также реализована автоматическая генерация кода для сериализации и десериализации любых классов и структур, реализованная с помощью _Expression`s_.

# Примеры кода

```cs
using ASiNet.Data.Serialization;

var person = new Person() 
{ 
    Id = 10, 
    FirstName = "Иван", 
    LastName = "Иванов" 
};

var buffer = new byte[256]; // Создаём буфер куда будет записан объект

var size = BinarySerializer.Serialize<Person>(person, buffer); // Данный метод вернёт количество записанных байт.

var result = BinarySerializer.Deserialize<Person>(buffer); // Данный метод прочитает объект из массива байт и в result мы получим нашего персона

public class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
```

# Создание собственных моделей.

##Сериализуемый объект:
```cs
public class Person
{
    public int Id { get; set; }

    public int Age { get; set; }

    public string? Name { get; set; }
}
```
## Модель для этого объекта:
```cs
using ASiNet.Data.Serialization;
using ASiNet.Data.Serialization.Interfaces;
using ASiNet.Data.Serialization.Models;

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
```

## Добавим модель в контекст сериализатора:
```cs
BinarySerializer.SerializeContext.AddModel(new PersonModel());
```

Теперь `BinarySerializer` будет использовать вашу `PersonModel` для сериализации и десериализации объекта `Person`
