namespace ASiNet.Data.Serialization.Exceptions;
public class WriterException(Exception inner) : Exception(inner.Message, inner)
{
}
