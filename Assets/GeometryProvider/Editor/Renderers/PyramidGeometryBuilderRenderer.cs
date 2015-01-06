using UnityEditor;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Custom renderer for pyramid settings.
    /// </summary>
    [CustomRenderer(typeof(PyramidGeometryBuilder))]
    public class PyramidGeometryBuilderRenderer : StandardGeometryBuilderRenderer
    {
        /// <summary>
        /// Draws pyramid specific controls.
        /// </summary>
        protected override void DrawCustomControls(GeometryBuilderSettings settings)
        {
            var pyramid = (PyramidGeometryBuilderSettings) settings;

            GUILayout.Label("Pyramid");
            EditorGUI.indentLevel++;

            pyramid.NumSides = Mathf.Max(3, EditorGUILayout.IntField("Number of Sides", pyramid.NumSides));
            pyramid.Height = EditorGUILayout.FloatField("Height", pyramid.Height);

            EditorGUI.indentLevel--;
        }
    }
}