using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using System.Reflection;
using System;

namespace TheGoldenMule.Geo.Editor
{

    public class GeometryProviderWindow : EditorWindow
    {
        private static readonly List<MethodInfo> _allFactories = new List<MethodInfo>();

        static GeometryProviderWindow()
        {
            _allFactories.AddRange(TypeUtility.MethodsWithAttribute<CustomFactory>());
        }

        private readonly List<string> _builderNames = new List<string>();
        private readonly List<IGeometryBuilder> _builders = new List<IGeometryBuilder>();
        private readonly List<IGeometryBuilderRenderer> _renderers = new List<IGeometryBuilderRenderer>();
        private readonly List<MethodInfo> _factories = new List<MethodInfo>();

        private int _selectedBuilderIndex = -1;
        private GeometryBuilderSettings _settings;
        private IGeometryBuilder _builder;
        private IGeometryBuilderRenderer _renderer;
        private GameObject _gameObject;
        private Mesh _mesh;

        private void OnEnable()
        {
            InitializeBuilders();

            minSize = new Vector2(168, 300);
            title = "Geo Editor";
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Primitive");
            var index = Mathf.Max(EditorGUILayout.Popup(_selectedBuilderIndex, _builderNames.ToArray()), 0);
            GUILayout.EndHorizontal();

            if (index >= 0 && _renderers.Count > index)
            {
                if (index != _selectedBuilderIndex || null == _renderer)
                {
                    _settings = (GeometryBuilderSettings) _factories[index].Invoke(null, null);

                    _renderer = _renderers[index];
                    _builder = _builders[index];

                    _settings.Name = Name(_builder);
                    _settings.Description = Description(_builder);
                }

                _renderer.Draw(_settings);

                _selectedBuilderIndex = index;
            }

            GUILayout.EndVertical();
        }

        private void OnCreatePrimitive()
        {
            _gameObject = CreateGameObject("Primitive");
            _mesh = new Mesh();

            _gameObject.GetComponent<MeshFilter>().sharedMesh = _mesh;

            // select the new primitive
            EditorUtility.SelectAndFocus(_gameObject.transform);
            if (null != _renderer)
            {
                _renderer.Selected = _gameObject.transform;
            }

            OnUpdatePrimitive();
        }

        private void OnUpdatePrimitive()
        {
            if (null == _gameObject || null == _builder)
            {
                return;
            }

            _builder.Build(_mesh, _settings);
        }

        private void InitializeBuilders()
        {
            var builderTypes = TypeUtility.Implementors<IGeometryBuilder>();
            
            foreach (var builderType in builderTypes)
            {
                if (builderType.IsAbstract)
                {
                    continue;
                }

                try
                {
                    var builder = (IGeometryBuilder) Activator.CreateInstance(builderType);
                    var renderer = Renderer(builder);
                    var factory = Factory(builder);

                    renderer.OnCreate += OnCreatePrimitive;
                    renderer.OnUpdate += OnUpdatePrimitive;

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

        private static GameObject CreateGameObject(string name)
        {
            var gameObject = new GameObject(name);

            gameObject.AddComponent<MeshFilter>();
            var renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

            return gameObject;
        }

        private static string Name(IGeometryBuilder builder)
        {
            var builderType = builder.GetType();
            var customName = TypeUtility.Attribute<System.ComponentModel.DisplayNameAttribute>(builderType);
            if (null != customName)
            {
                return customName.DisplayName;
            }

            return builderType.Name;
        }

        private static string Description(IGeometryBuilder builder)
        {
            var builderType = builder.GetType();
            var customName = TypeUtility.Attribute<System.ComponentModel.DescriptionAttribute>(builderType);
            if (null != customName)
            {
                return customName.Description;
            }

            return "[No description]";
        }

        private static IGeometryBuilderRenderer Renderer(IGeometryBuilder builder)
        {
            var builderType = builder.GetType();
            var rendererTypes = TypeUtility.Implementors<IGeometryBuilderRenderer>();

            foreach (var rendererType in rendererTypes)
            {
                var customRenderer = TypeUtility.Attribute<CustomRenderer>(rendererType);
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
            foreach (var method in _allFactories)
            {
                var attribute = TypeUtility.Attribute<CustomFactory>(method);
                if (null != attribute && attribute.Type == builder.GetType())
                {
                    return method;
                }
            }
            
            return typeof(GeometryProviderWindow).GetMethod(
                "DefaultFactory",
                BindingFlags.Static | BindingFlags.NonPublic);
        }

        private static GeometryBuilderSettings DefaultFactory()
        {
            return new GeometryBuilderSettings();
        }

        [MenuItem("GameObject/Geometry Editor %g")]
        private static void Toggle()
        {
            var window = EditorWindow.FindObjectOfType<GeometryProviderWindow>();
            if (null != window)
            {
                window.Close();
            }
            else
            {
                EditorWindow.GetWindow<GeometryProviderWindow>();
            }
        }
    }
}