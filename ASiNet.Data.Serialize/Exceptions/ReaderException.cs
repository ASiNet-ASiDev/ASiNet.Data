namespace ASiNet.Data.Serialization.Exceptions;
public class ReaderException(Exception inner) : Exception(inner.Message, inner)
{
}
