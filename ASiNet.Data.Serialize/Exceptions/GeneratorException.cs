namespace ASiNet.Data.Serialization.Exceptions;
public class GeneratorException(Exception inner) : Exception(inner.Message, inner)
{
}
