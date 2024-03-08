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

public class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}

var person = new Person() 
{ 
    Id = 10, 
    FirstName = "Иван", 
    LastName = "Иванов" 
};

var buffer = new byte[256]; // Создаём буфер куда будет записан объект

var size = BinarySerializer.Serialize<Person>(person, buffer); // Данный метод вернёт количество записанных байт.

var result = BinarySerializer.Deserialize<Person>(buffer); // Данный метод прочитает объект из массива байт и в result мы получим нашего персона

```
