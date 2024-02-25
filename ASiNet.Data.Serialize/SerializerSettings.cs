using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Data.Serialization;
public struct SerializerSettings
{
    public bool IgnoreFields { get; set; }

    public bool IgnoreProperties { get; set; }
}
