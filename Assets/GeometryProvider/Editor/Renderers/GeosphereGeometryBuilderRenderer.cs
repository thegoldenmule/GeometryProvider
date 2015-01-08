using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Custom renderer for Geosphere settings.
    /// </summary>
    [CustomRenderer(typeof(GeosphereGeometryBuilder))]
    public class GeosphereGeometryBuilderRenderer : StandardGeometryBuilderRenderer
    {
        /// <summary>
        /// Draws Geosphere settings.
        /// </summary>
        /// <param name="settings"></param>
        protected override void DrawCustomControls(GeometryBuilderSettings settings)
        {
            base.DrawCustomControls(settings);
            
            var geosphereSettings = (GeosphereGeometryBuilderSettings) settings;

            geosphereSettings.Quality = Mathf.Max(
                1,
                EditorGUILayout.IntSlider(
                    "Quality",
                    geosphereSettings.Quality,
                    0, 4));
        }

        /// <summary>
        /// Overrides Update panel because GeoSphere's are too expensive to be
        /// built every frame using the subdivision method.
        /// </summary>
        protected override void DrawBuildUpdate()
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.LabelField("Computationally expensive, click to update.");

            Selected = (Transform)EditorGUILayout.ObjectField("Primitive", Selected, typeof(Transform));

            GUI.enabled = null != Selected;
            
            if (GUILayout.Button("Rebuild", GUILayout.Height(40)))
            {
                CallUpdate();
            }

            GUI.enabled = true;

            EditorGUI.indentLevel--;
        }
    }
}