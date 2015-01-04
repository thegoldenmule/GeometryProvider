using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGoldenMule.Geo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CustomRenderer : Attribute
    {
        public readonly Type Type;

        public CustomRenderer(Type type)
        {
            Type = type;
        }
    }
}
