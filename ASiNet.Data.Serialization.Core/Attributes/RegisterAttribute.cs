using System;
using System.Collections.Generic;
using System.Text;

namespace ASiNet.Data.Serialization.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
public class RegisterAttribute : Attribute
{
}
