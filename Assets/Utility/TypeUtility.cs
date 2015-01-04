using System;
using System.Reflection;
using System.Linq;

namespace TheGoldenMule
{
    public static class TypeUtility
    {
        public static Type[] Implementors<T>()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface)
                .ToArray();
        }

        public static MethodInfo[] MethodsWithAttribute<T>() where T : Attribute
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(type => type.GetMethods())
                .Where(method => method
                    .GetCustomAttributes(typeof(T), true)
                    .Any())
                .ToArray();
        }

        public static T Attribute<T>(Type type) where T : Attribute
        {
            return type
                .GetCustomAttributes(typeof(T), true)
                .OfType<T>()
                .FirstOrDefault<T>();
        }

        public static T Attribute<T>(MethodInfo method) where T : Attribute
        {
            return method
                .GetCustomAttributes(typeof(T), true)
                .OfType<T>()
                .FirstOrDefault<T>();
        }
    }
}