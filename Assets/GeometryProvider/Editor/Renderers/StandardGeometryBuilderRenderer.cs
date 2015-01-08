using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Provides base functionality for all IGeometryBuilderRenderers that so
    /// desire.
    /// </summary>
    public class StandardGeometryBuilderRenderer : IGeometryBuilderRenderer
    {
        /// <summary>
        /// Called when a new primitive should be created.
        /// </summary>
        public event Action OnCreate;

        /// <summary>
        /// Called when the selected primitive should be updated.
        /// </summary>
        public event Action OnUpdate;

        /// <summary>
        /// GUI state variables.
        /// </summary>
        protected Vector2 _scrollPosition;
        protected string _buildTabState;
        protected readonly Dictionary<string, bool> _foldouts = new Dictionary<string, bool>(); 
        
        /// <summary>
        /// Sets the selected Transform for updates.
        /// </summary>
        public virtual Transform Selected
        {
            get;
            set;
        }

        /// <summary>
        /// Draws all controls.
        /// </summary>
        /// <param name="settings"></param>
        public virtual void Draw(GeometryBuilderSettings settings)
        {
            GUILayout.BeginVertical();

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            DrawIntro(settings);
            EditorGUILayout.Separator();

            if (Foldout("Primitive"))
            {
                EditorGUI.indentLevel++;

                DrawCustomControls(settings);
                EditorGUILayout.Separator();

                EditorGUI.indentLevel++;
            }

            DrawTestControls(settings);
            EditorGUILayout.Separator();

            DrawVertexControls(settings);
            EditorGUILayout.Separator();

            DrawTriangleControls(settings);
            EditorGUILayout.Separator();
            
            DrawUVControls(settings, settings.UV, "UV");
            EditorGUILayout.Separator();

            DrawUVControls(settings, settings.UV2, "UV2");
            EditorGUILayout.Separator();

            DrawColorControls(settings);
            EditorGUILayout.Separator();

            DrawNormalControls(settings);
            EditorGUILayout.Separator();

            DrawBuildControls(settings);

            GUILayout.EndScrollView();

            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws the heading.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawIntro(GeometryBuilderSettings settings)
        {
            EditorGUI.indentLevel++;
            GUILayout.TextArea(settings.Description ?? "");
            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Draws controls for testing.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawTestControls(GeometryBuilderSettings settings)
        {
            
        }

        /// <summary>
        /// Draws custom controls for a subclass of this renderer.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawCustomControls(GeometryBuilderSettings settings)
        {
            
        }

        /// <summary>
        /// Draws controls for vertex transformations.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawVertexControls(GeometryBuilderSettings settings)
        {
            if (Foldout("Vertices"))
            {
                EditorGUI.indentLevel++;
                settings.Vertex.Translation = EditorGUILayout.Vector3Field("Translation", settings.Vertex.Translation);
                settings.Vertex.Rotation.eulerAngles = EditorGUILayout.Vector3Field("Rotation", settings.Vertex.Rotation.eulerAngles);
                settings.Vertex.Scale = EditorGUILayout.Vector3Field("Scale", settings.Vertex.Scale);
                EditorGUI.indentLevel--;
            }
        }

        /// <summary>
        /// Draws controls for triangle transformations.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawTriangleControls(GeometryBuilderSettings settings)
        {
            if (Foldout("Triangles"))
            {
                EditorGUI.indentLevel++;
                
                settings.Vertex.DoubleSided = EditorGUILayout.Toggle("Double Sided", settings.Vertex.DoubleSided);

                GUI.enabled = !settings.Vertex.DoubleSided;
                settings.Vertex.ReverseWinding = EditorGUILayout.Toggle("Reverse Winding", settings.Vertex.ReverseWinding);
                GUI.enabled = true;

                EditorGUI.indentLevel--;
            }
        }

        /// <summary>
        /// Draws controls for UVs.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="uv"></param>
        protected virtual void DrawUVControls(GeometryBuilderSettings settings, GeometryBuilderUVSettings uv, string label)
        {
            if (Foldout(label))
            {
                EditorGUI.indentLevel++;

                DrawBufferControls(uv);

                uv.Rect = EditorGUILayout.RectField("Rect", uv.Rect);

                EditorGUILayout.BeginHorizontal();
                uv.Rotation.Origin = EditorGUILayout.Vector2Field("Origin", uv.Rotation.Origin);
                uv.Rotation.Theta = EditorGUILayout.FloatField("Theta", uv.Rotation.Theta);
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
            }
        }

        /// <summary>
        /// Draws controls for Colors.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawColorControls(GeometryBuilderSettings settings)
        {
            if (Foldout("Color"))
            {
                EditorGUI.indentLevel++;

                DrawBufferControls(settings.Color);
                settings.Color.Tint = EditorGUILayout.ColorField("Color", settings.Color.Tint);

                EditorGUI.indentLevel--;
            }
        }

        /// <summary>
        /// Draws controls for normals.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawNormalControls(GeometryBuilderSettings settings)
        {
            if (Foldout("Normals"))
            {
                EditorGUI.indentLevel++;

                DrawBufferControls(settings.Normals);
                settings.Normals.Generate = EditorGUILayout.Toggle("Generate", settings.Normals.Generate);

                EditorGUI.indentLevel--;
            }
        }

        /// <summary>
        /// Draws controls for tangents.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawTangentControls(GeometryBuilderSettings settings)
        {
            if (Foldout("Tangents"))
            {
                EditorGUI.indentLevel++;

                DrawBufferControls(settings.Tangents);
                settings.Tangents.Generate = EditorGUILayout.Toggle("Generate", settings.Tangents.Generate);

                EditorGUI.indentLevel--;
            }
        }

        /// <summary>
        /// Draws build controls.
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void DrawBuildControls(GeometryBuilderSettings settings)
        {
            if (Foldout("Build", true))
            {
                _buildTabState = EditorUtility.DrawTabs(
                    _buildTabState,
                    new Tab("Create", DrawBuildCreate),
                    new Tab("Update", DrawBuildUpdate),
                    new Tab("Save", DrawBuildSave));
            }
        }

        /// <summary>
        /// Draws controls for creation.
        /// </summary>
        protected virtual void DrawBuildCreate()
        {
            EditorGUI.indentLevel++;
            
            if (GUILayout.Button("Create New", GUILayout.Height(40)) && null != OnCreate)
            {
                OnCreate();
            }

            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Draws controls for live update.
        /// </summary>
        protected virtual void DrawBuildUpdate()
        {
            EditorGUI.indentLevel++;

            Selected = (Transform) EditorGUILayout.ObjectField(
                "Primitive",
                Selected,
                typeof (Transform),
                true);

            if (null != OnUpdate)
            {
                OnUpdate();
            }

            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Draws controls for saving out a prefab.
        /// </summary>
        protected virtual void DrawBuildSave()
        {

        }

        /// <summary>
        /// Draws controls for buffer settings.
        /// </summary>
        /// <param name="bufferSettings"></param>
        protected virtual void DrawBufferControls(GeometryBuilderBufferSettings bufferSettings)
        {
            bufferSettings.Enabled = EditorGUILayout.Toggle("Enabled", bufferSettings.Enabled);
            bufferSettings.Buffer = (Buffer)EditorGUILayout.EnumPopup("Buffer", bufferSettings.Buffer);
        }

        /// <summary>
        /// Draws a generic foldout.
        /// </summary>
        protected virtual bool Foldout(string name, bool defaultValue = false)
        {
            if (!_foldouts.ContainsKey(name))
            {
                _foldouts[name] = defaultValue;
            }

            var value = _foldouts[name] = EditorGUILayout.Foldout(_foldouts[name], name);
            return value;
        }

        /// <summary>
        /// Calls OnUpdate. This is needed by subclasses.
        /// </summary>
        protected void CallUpdate()
        {
            if (null != OnUpdate)
            {
                OnUpdate();
            }
        }
    }
}