using System;

namespace ASiNet.Data.Serialization.Exceptions
{
    public class ReaderException : Exception
    {
        public ReaderException(Exception inner) : base(inner.Message, inner)
        {

        }
    }

}