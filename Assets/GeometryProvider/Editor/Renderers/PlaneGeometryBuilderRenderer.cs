using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEditor;

namespace TheGoldenMule.Geo.Editor
{
    public class PlaneGeometryBuilderRenderer : StandardGeometryBuilderRenderer
    {
        protected override void DrawTransformControls(GeometryBuilderSettings settings)
        {
            base.DrawTransformControls(settings);

            PlaneGeometryBuilderSettings planeSettings = (PlaneGeometryBuilderSettings) settings;

            GUILayout.Label("Plane");
            EditorGUI.indentLevel++;
            EditorGUILayout.IntSlider("Num X Vertices", planeSettings.NumXVerts, 2, 1000);
            EditorGUILayout.IntSlider("Num Z Vertices", planeSettings.NumZVerts, 2, 1000);
            EditorGUI.indentLevel--;
        }
    }
}
