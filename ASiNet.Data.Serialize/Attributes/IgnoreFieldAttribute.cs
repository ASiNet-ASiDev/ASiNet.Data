using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.Data.Serialization.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class IgnoreFieldAttribute : Attribute
{
}
