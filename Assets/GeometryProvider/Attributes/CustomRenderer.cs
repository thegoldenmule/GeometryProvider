using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGoldenMule.Geo.Editor
{
    public class CustomRenderer : Attribute
    {
        public Type Type;

        public CustomRenderer(Type type)
        {
            Type = type;
        }
    }
}
