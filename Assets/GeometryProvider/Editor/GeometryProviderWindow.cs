using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TheGoldenMule.Geo.Editor
{

    public class GeometryProviderWindow : EditorWindow
    {
        private readonly Dictionary<IGeometryBuilder, IGeometryBuilderRenderer> _renderers = new Dictionary<IGeometryBuilder,IGeometryBuilderRenderer>;

        private void OnEnable()
        {
            var builderTypes = Implementors<IGeometryBuilder>();
            var rendererTypes = Implementors<IGeometryBuilderRenderer>();
            foreach (var builder in builderTypes)
            {

            }

            minSize = new Vector2(168, 300);
        }

        private void OnGUI()
        {

        }

        private static Type[] Implementors<T>()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface)
                .ToArray();
        }

        public static T Attribute<T>(Type type) where T : Attribute
        {
            return type
                .GetCustomAttributes(typeof(T), true)
                .OfType<T>()
                .FirstOrDefault<T>();
        }

        [MenuItem("Tools/Geometry Provider %g")]
        private static void Toggle()
        {
            EditorWindow.GetWindow<GeometryProviderWindow>();
        }
    }

}