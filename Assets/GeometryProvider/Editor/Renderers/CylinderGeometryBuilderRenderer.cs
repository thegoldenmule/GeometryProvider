using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Custom renderer for cylinder settings.
    /// </summary>
    [CustomRenderer(typeof(CylinderGeometryBuilder))]
    public class CylinderGeometryBuilderRenderer : StandardGeometryBuilderRenderer
    {
        /// <summary>
        /// Draws cylinder specific controls.
        /// </summary>
        /// <param name="settings"></param>
        protected override void DrawCustomControls(GeometryBuilderSettings settings)
        {
            var cylinderSettings = (CylinderGeometryBuilderSettings) settings;

            base.DrawCustomControls(settings);

            cylinderSettings.NumSides = Mathf.Max(3, EditorGUILayout.IntField("Number of Sides", cylinderSettings.NumSides));
            cylinderSettings.Height = EditorGUILayout.FloatField("Height", cylinderSettings.Height);
            cylinderSettings.Endcaps = EditorGUILayout.Toggle("Cap Ends", cylinderSettings.Endcaps);
        }
    }
}