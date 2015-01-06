using System;

using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    public class StandardGeometryBuilderRenderer : IGeometryBuilderRenderer
    {
        public event Action OnCreate;
        public event Action OnUpdate;

        protected bool _bufferFoldout = false;
        protected string _buildTabState;
        
        public virtual bool IsLiveUpdate
        {
            get;
            private set;
        }

        public virtual Transform Selected
        {
            get;
            set;
        }

        public StandardGeometryBuilderRenderer()
        {
            IsLiveUpdate = true;
        }

        public virtual void Draw(GeometryBuilderSettings settings)
        {
            GUILayout.BeginVertical();

            DrawIntro(settings);
            EditorGUILayout.Separator();

            DrawTransformControls(settings);
            EditorGUILayout.Separator();

            _bufferFoldout = EditorGUILayout.Foldout(_bufferFoldout, "Buffers");

            if (_bufferFoldout)
            {
                DrawUVControls(settings, settings.UV);
                EditorGUILayout.Separator();

                DrawUVControls(settings, settings.UV2);
                EditorGUILayout.Separator();

                DrawColorControls(settings);
                EditorGUILayout.Separator();

                DrawNormalControls(settings);
                EditorGUILayout.Separator();

                DrawTangentControls(settings);
                EditorGUILayout.Separator();
            }

            DrawCustomControls(settings);
            EditorGUILayout.Separator();

            DrawBuildControls(settings);

            GUILayout.EndVertical();
        }

        protected virtual void DrawIntro(GeometryBuilderSettings settings)
        {
            EditorGUI.indentLevel++;
            GUILayout.TextArea(settings.Description ?? "");
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawTransformControls(GeometryBuilderSettings settings)
        {
            GUILayout.Label("Transform Vertices");

            EditorGUI.indentLevel++;
            settings.Vertex.Translation = EditorGUILayout.Vector3Field("Translation", settings.Vertex.Translation);
            settings.Vertex.Rotation.eulerAngles = EditorGUILayout.Vector3Field("Rotation", settings.Vertex.Rotation.eulerAngles);
            settings.Vertex.Scale = EditorGUILayout.Vector3Field("Scale", settings.Vertex.Scale);
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawBufferControls(GeometryBuilderBufferSettings bufferSettings)
        {
            bufferSettings.Buffer = (Buffer) EditorGUILayout.EnumPopup("Buffer", bufferSettings.Buffer);
            bufferSettings.Enabled = EditorGUILayout.Toggle("Enabled", bufferSettings.Enabled);
        }

        protected virtual void DrawUVControls(GeometryBuilderSettings settings, GeometryBuilderUVSettings uv)
        {
            GUILayout.Label("UV");

            EditorGUI.indentLevel++;

            DrawBufferControls(uv);
            uv.Rect = EditorGUILayout.RectField("Rect", uv.Rect);

            EditorGUI.indentLevel--;
        }

        protected virtual void DrawColorControls(GeometryBuilderSettings settings)
        {
            GUILayout.Label("Color");

            EditorGUI.indentLevel++;

            DrawBufferControls(settings.Color);
            settings.Color.Tint = EditorGUILayout.ColorField("Color", settings.Color.Tint);

            EditorGUI.indentLevel--;
        }

        protected virtual void DrawNormalControls(GeometryBuilderSettings settings)
        {
            GUILayout.Label("Normals");

            EditorGUI.indentLevel++;

            DrawBufferControls(settings.Normals);
            settings.Normals.Generate = EditorGUILayout.Toggle("Generate", settings.Normals.Generate);
            
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawTangentControls(GeometryBuilderSettings settings)
        {
            GUILayout.Label("Tangents");

            EditorGUI.indentLevel++;

            DrawBufferControls(settings.Tangents);
            settings.Tangents.Generate = EditorGUILayout.Toggle("Generate", settings.Tangents.Generate);

            EditorGUI.indentLevel--;
        }

        protected virtual void DrawCustomControls(GeometryBuilderSettings settings)
        {
            
        }

        protected virtual void DrawBuildControls(GeometryBuilderSettings settings)
        {
            GUILayout.Label("Build");

            _buildTabState = EditorUtility.DrawTabs(
                _buildTabState,
                new Tab("Create", DrawBuildCreate),
                new Tab("Update", DrawBuildUpdate),
                new Tab("Save", DrawBuildSave));
        }

        protected virtual void DrawBuildCreate()
        {
            EditorGUI.indentLevel++;
            
            if (GUILayout.Button("Create New", GUILayout.Height(40)) && null != OnCreate)
            {
                OnCreate();
            }

            EditorGUI.indentLevel--;
        }

        protected virtual void DrawBuildUpdate()
        {
            EditorGUI.indentLevel++;

            Selected = (Transform) EditorGUILayout.ObjectField("Primitive", Selected, typeof (Transform));

            GUI.enabled = null != Selected;
            IsLiveUpdate = EditorGUILayout.Toggle("Live Update", IsLiveUpdate);

            if (IsLiveUpdate && null != OnUpdate)
            {
                OnUpdate();
            }

            EditorGUI.indentLevel--;
            GUI.enabled = true;
        }

        protected virtual void DrawBuildSave()
        {

        }

        protected void CallUpdate()
        {
            if (null != OnUpdate)
            {
                OnUpdate();
            }
        }
    }
}