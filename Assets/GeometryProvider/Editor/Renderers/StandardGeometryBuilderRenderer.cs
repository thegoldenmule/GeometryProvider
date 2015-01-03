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
        public void Draw(GeometryBuilderSettings settings)
        {
            GUILayout.BeginVertical();

            DrawTransformControls(settings.Transform);
            DrawUVControls(settings.UV);

            GUILayout.EndVertical();
        }

        private void DrawTransformControls(GeometryBuilderTransformSettings settings)
        {
            GUILayout.Label("Transform");

            EditorGUI.indentLevel++;
            settings.Scale = EditorGUILayout.Vector3Field("Scale", settings.Scale);
            settings.Rotation.eulerAngles = EditorGUILayout.Vector3Field("Rotation", settings.Rotation.eulerAngles);
            EditorGUI.indentLevel--;
        }

        private void DrawUVControls(GeometryBuilderUVSettings settings)
        {
            GUILayout.Label("UV");

            EditorGUI.indentLevel++;
            settings.Rect = EditorGUILayout.RectField("Rect", settings.Rect);
            EditorGUI.indentLevel--;
        }
    }
}