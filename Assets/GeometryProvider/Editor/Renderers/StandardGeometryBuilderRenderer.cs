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

        public void Draw(GeometryBuilderSettings settings)
        {
            GUILayout.BeginVertical();

            DrawTransformControls(settings.Transform);
            DrawUVControls(settings.UV);
            DrawBuildControls(settings);

            GUILayout.EndVertical();
        }

        protected virtual void DrawTransformControls(GeometryBuilderTransformSettings settings)
        {
            GUILayout.Label("Transform");

            EditorGUI.indentLevel++;
            settings.Scale = EditorGUILayout.Vector3Field("Scale", settings.Scale);
            settings.Rotation.eulerAngles = EditorGUILayout.Vector3Field("Rotation", settings.Rotation.eulerAngles);
            settings.Material = (Material) EditorGUILayout.ObjectField("Material", settings.Material, typeof(Material), false);
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawUVControls(GeometryBuilderUVSettings settings)
        {
            GUILayout.Label("UV");

            EditorGUI.indentLevel++;
            settings.Rect = EditorGUILayout.RectField("Rect", settings.Rect);
            EditorGUI.indentLevel--;
        }

        protected virtual void DrawBuildControls(GeometryBuilderSettings settings)
        {
            GUILayout.Label("Build");

            EditorGUI.indentLevel++;
            if (GUILayout.Button("Create") && null != OnCreate)
            {
                OnCreate();
            }
            EditorGUI.indentLevel--;
        }
    }
}