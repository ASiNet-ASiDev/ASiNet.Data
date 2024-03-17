using System;

namespace ASiNet.Data.Serialization.Exceptions
{
    public class WriterException : Exception
    {
        public WriterException(Exception inner) : base(inner.Message, inner)
        {
            
        }

    }
}
