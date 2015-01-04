using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    public class StandardGeometryBuilderRenderer : IGeometryBuilderRenderer
    {
        public event Action OnCreate;
        public event Action OnUpdate;
        public event Action OnAddBuffer;

        protected string _buildTabState;

        public virtual bool IsLiveUpdate
        {
            get;
            private set;
        }

        public virtual void Draw(GeometryBuilderSettings settings)
        {
            GUILayout.BeginVertical();

            DrawIntro(settings);
            DrawTransformControls(settings);
            DrawBufferControls(settings);
            DrawBuildControls(settings);

            GUILayout.EndVertical();
        }

        protected virtual void DrawIntro(GeometryBuilderSettings settings)
        {
            GUILayout.Label(settings.Name ?? "");

            EditorGUI.indentLevel++;
            GUILayout.TextArea(settings.Description ?? "");
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawTransformControls(GeometryBuilderSettings settings)
        {
            GUILayout.Label("Transform");

            EditorGUI.indentLevel++;
            settings.Transform.Translation = EditorGUILayout.Vector3Field("Translation", settings.Transform.Translation);
            settings.Transform.Scale = EditorGUILayout.Vector3Field("Scale", settings.Transform.Scale);
            settings.Transform.Rotation.eulerAngles = EditorGUILayout.Vector3Field("Rotation", settings.Transform.Rotation.eulerAngles);
            settings.Transform.CustomDeformation = (Material) EditorGUILayout.ObjectField("Material", settings.Transform.CustomDeformation, typeof(Material), false);
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawBufferControls(GeometryBuilderSettings settings)
        {
            if (null == settings.Buffers)
            {
                return;
            }

            GUILayout.Label("Buffers");

            EditorGUI.indentLevel++;
            for (int i = 0, len = settings.Buffers.Length; i < len; i++)
            {

            }

            if (GUILayout.Button("Add") && null != OnAddBuffer)
            {
                OnAddBuffer();
            }

            EditorGUI.indentLevel--;
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
            if (GUILayout.Button("Create") && null != OnCreate)
            {
                OnCreate();
            }
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawBuildUpdate()
        {

        }

        protected virtual void DrawBuildSave()
        {

        }
    }
}