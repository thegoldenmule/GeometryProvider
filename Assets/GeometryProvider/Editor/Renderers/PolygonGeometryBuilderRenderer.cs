using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    [CustomRenderer(typeof(PolygonGeometryBuilder))]
    public class PolygonGeometryBuilderRenderer : StandardGeometryBuilderRenderer
    {
        public event Action OnCreate;
        public event Action OnUpdate;

        protected override void DrawTransformControls(GeometryBuilderSettings settings)
        {
            base.DrawTransformControls(settings);

            var polygonSettings = (PolygonGeometryBuilderSettings) settings;

            GUILayout.Label("Polygon");

            EditorGUI.indentLevel++;
            polygonSettings.NumSides = Mathf.Max(3, EditorGUILayout.IntField("Number of Sides", polygonSettings.NumSides));
            polygonSettings.Convex = EditorGUILayout.Toggle("Convex", polygonSettings.Convex);
            EditorGUI.indentLevel--;
        }
    }
}
