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
        /// Appends custom circle controls after transform controls.
        /// </summary>
        /// <param name="settings"></param>
        protected override void DrawTransformControls(GeometryBuilderSettings settings)
        {
            base.DrawTransformControls(settings);

            var polygonSettings = (CircleGeometryBuilderSettings) settings;

            GUILayout.Label("Circle");

            EditorGUI.indentLevel++;
            polygonSettings.NumSides = Mathf.Max(3, EditorGUILayout.IntField("Number of Sides", polygonSettings.NumSides));
            EditorGUI.indentLevel--;
        }
    }
}