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
        /// <summary>
        /// Draws custom controls for a Plane.
        /// </summary>
        /// <param name="settings"></param>
        protected override void DrawCustomControls(GeometryBuilderSettings settings)
        {
            base.DrawCustomControls(settings);

            PlaneGeometryBuilderSettings planeSettings = (PlaneGeometryBuilderSettings) settings;

            planeSettings.NumXVerts = Mathf.Max(EditorGUILayout.IntField("Num X Vertices", planeSettings.NumXVerts), 2);
            planeSettings.NumZVerts = Mathf.Max(EditorGUILayout.IntField("Num Z Vertices", planeSettings.NumZVerts), 2);
        }
    }
}
