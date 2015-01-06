using UnityEngine;
using UnityEditor;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Renders custom controls for Plane.
    /// </summary>
    [CustomRenderer(typeof(PlaneGeometryBuilder))]
    public class PlaneGeometryBuilderRenderer : StandardGeometryBuilderRenderer
    {
        protected override void DrawCustomControls(GeometryBuilderSettings settings)
        {
            PlaneGeometryBuilderSettings planeSettings = (PlaneGeometryBuilderSettings) settings;

            GUILayout.Label("Plane");
            EditorGUI.indentLevel++;
            planeSettings.NumXVerts = Mathf.Max(EditorGUILayout.IntField("Num X Vertices", planeSettings.NumXVerts), 2);
            planeSettings.NumZVerts = Mathf.Max(EditorGUILayout.IntField("Num Z Vertices", planeSettings.NumZVerts), 2);
            EditorGUI.indentLevel--;
        }
    }
}
