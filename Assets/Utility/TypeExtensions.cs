using System;
using System.Reflection;
using System.Linq;

namespace TheGoldenMule
{
    public static class TypeExtensions
    {
        public static Type[] Implementors(this Type @this)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => @this.IsAssignableFrom(type) && !type.IsInterface)
                .ToArray();
        }

        public static T Attribute<T>(this Type @this) where T : Attribute
        {
            return @this
                .GetCustomAttributes(typeof(T), true)
                .OfType<T>()
                .FirstOrDefault<T>();
        }

        public static T Attribute<T>(this MethodInfo @this) where T : Attribute
        {
            return @this
                .GetCustomAttributes(typeof(T), true)
                .OfType<T>()
                .FirstOrDefault<T>();
        }

        public static MethodInfo[] MethodsWithAttribute<T>() where T : Attribute
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .SelectMany(type => type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
                .Where(method => method
                    .GetCustomAttributes(typeof(T), true)
                    .Any())
                .ToArray();
        }
    }
}