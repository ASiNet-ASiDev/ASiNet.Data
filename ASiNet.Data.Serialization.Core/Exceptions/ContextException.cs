using System;

namespace ASiNet.Data.Serialization.Exceptions
{
    public class ContextException : Exception
    {
        public ContextException(Exception inner) : base(inner.Message, inner)
        {
        }
    }

}