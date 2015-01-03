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
        private static readonly List<MethodInfo> _allFactories = new List<MethodInfo>();

        static GeometryProviderWindow()
        {
            _allFactories.AddRange(MethodsWithAttribute<CustomFactory>());
        }

        private readonly List<string> _builderNames = new List<string>();
        private readonly List<IGeometryBuilder> _builders = new List<IGeometryBuilder>();
        private readonly List<IGeometryBuilderRenderer> _renderers = new List<IGeometryBuilderRenderer>();
        private readonly List<MethodInfo> _factories = new List<MethodInfo>();

        private int _selectedBuilderIndex = -1;
        private GeometryBuilderSettings _settings;

        private void OnEnable()
        {
            InitializeBuilders();

            minSize = new Vector2(168, 300);
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();

            var index = EditorGUILayout.Popup(_selectedBuilderIndex, _builderNames.ToArray());

            if (index > 0 && _renderers.Count > index)
            {
                var renderer = _renderers[index];
                var builder = _builders[index];

                if (index != _selectedBuilderIndex)
                {
                    _settings = (GeometryBuilderSettings) _factories[index].Invoke(null, null);
                }

                renderer.Draw(_settings);
            }

            GUILayout.EndVertical();
        }

        private void InitializeBuilders()
        {
            var builderTypes = Implementors<IGeometryBuilder>();
            
            foreach (var builderType in builderTypes)
            {
                try
                {
                    var builder = (IGeometryBuilder) Activator.CreateInstance(builderType);
                    var renderer = Renderer(builder);
                    var factory = Factory(builder);

                    _builderNames.Add(Name(builder));
                    _builders.Add(builder);
                    _renderers.Add(renderer);
                    _factories.Add(factory);
                }
                catch
                {
                    Debug.LogError(string.Format(
                        "Could not instantiate IGeometryBuilder {0}. Does it have a public default constructor?",
                        builderType));

                    continue;
                }
            }
        }

        private static string Name(IGeometryBuilder builder)
        {
            var builderType = builder.GetType();
            var customName = Attribute<System.ComponentModel.DisplayNameAttribute>(builderType);
            if (null != customName)
            {
                return customName.DisplayName;
            }

            return builderType.Name;
        }

        private static IGeometryBuilderRenderer Renderer(IGeometryBuilder builder)
        {
            var builderType = builder.GetType();
            var rendererTypes = Implementors<IGeometryBuilderRenderer>();

            foreach (var rendererType in rendererTypes)
            {
                var customRenderer = Attribute<CustomRenderer>(rendererType);
                if (null == customRenderer)
                {
                    continue;
                }

                if (customRenderer.Type == builderType)
                {
                    try
                    {
                        var customRendererInstance = (IGeometryBuilderRenderer) Activator.CreateInstance(rendererType);
                        return customRendererInstance;
                    }
                    catch
                    {
                        Debug.LogError(string.Format(
                            "Could not instantiate custom IGeometryBuilderRenderer {0}. Does it have a public default constructor?",
                            rendererType));
                        break;
                    }
                }
            }

            return new StandardGeometryBuilderRenderer();
        }

        private static MethodInfo Factory(IGeometryBuilder builder)
        {
            var factory = _allFactories
                .Where(method =>
                {
                    var attribute = Attribute<CustomFactory>(method);
                    if (null != attribute && attribute.Type == builder.GetType())
                    {
                        return true;
                    }

                    return false;
                })
                .FirstOrDefault();
            
            if (null != factory)
            {
                return factory;
            }
            
            return typeof(GeometryProviderWindow).GetMethod("DefaultFactory", BindingFlags.Static);
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

        private static MethodInfo[] MethodsWithAttribute<T>() where T : Attribute
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

        private static T Attribute<T>(Type type) where T : Attribute
        {
            return type
                .GetCustomAttributes(typeof(T), true)
                .OfType<T>()
                .FirstOrDefault<T>();
        }

        private static T Attribute<T>(MethodInfo method) where T : Attribute
        {
            return method
                .GetCustomAttributes(typeof(T), true)
                .OfType<T>()
                .FirstOrDefault<T>();
        }

        private static GeometryBuilderSettings DefaultFactory()
        {
            return new GeometryBuilderSettings();
        }

        [MenuItem("Tools/Geometry Provider %g")]
        private static void Toggle()
        {
            EditorWindow.GetWindow<GeometryProviderWindow>();
        }
    }

}