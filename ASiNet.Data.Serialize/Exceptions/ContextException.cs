namespace ASiNet.Data.Serialization.Exceptions;
public class ContextException(Exception inner) : Exception(inner.Message, inner)
{
}
