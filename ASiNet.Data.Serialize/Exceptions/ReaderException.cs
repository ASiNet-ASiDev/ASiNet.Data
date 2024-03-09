using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Data.Serialization.Exceptions;
public class ReaderException(Exception inner) : Exception(inner.Message, inner)
{
}
