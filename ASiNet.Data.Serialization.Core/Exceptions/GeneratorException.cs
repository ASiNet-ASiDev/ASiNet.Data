using System;

namespace ASiNet.Data.Serialization.Exceptions
{
    public class GeneratorException : Exception
    {

        public GeneratorException(Exception inner) : base(inner.Message, inner)
        {
            
        }

    }

}