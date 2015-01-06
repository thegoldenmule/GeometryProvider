using System;

using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Renders editor controls for a CircleGeometryBuilder.
    /// </summary>
    [CustomRenderer(typeof(CircleGeometryBuilder))]
    public class CircleGeometryBuilderRenderer : StandardGeometryBuilderRenderer
    {
        /// <summary>
        /// Draws custom controls for Circle.
        /// </summary>
        /// <param name="settings"></param>
        protected override void DrawCustomControls(GeometryBuilderSettings settings)
        {
            var polygonSettings = (CircleGeometryBuilderSettings) settings;

            GUILayout.Label("Circle");

            EditorGUI.indentLevel++;
            polygonSettings.NumSides = Mathf.Max(3, EditorGUILayout.IntField("Number of Sides", polygonSettings.NumSides));
            EditorGUI.indentLevel--;
        }
    }
}