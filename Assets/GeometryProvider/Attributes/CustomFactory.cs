using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGoldenMule.Geo
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class CustomFactory : Attribute
    {
        public readonly Type Type;

        public CustomFactory(Type type)
        {
            Type = type;
        }
    }
}