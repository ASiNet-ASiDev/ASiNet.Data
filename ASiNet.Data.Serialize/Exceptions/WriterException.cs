namespace ASiNet.Data.Serialization.Exceptions;
internal class WriterException(Exception inner) : Exception(inner.Message, inner)
{
}
