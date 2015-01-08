using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds quad geometry.
    /// </summary>
    [DisplayName("Quad")]
    [Description("A simple quad.")]
    public class QuadGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Generates the vertex + triangle layout for a quad.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public override void Layout(
            out Vector3[] vertices,
            out int[] triangles)
        {
            vertices = new []
            {
                new Vector3(-0.5f, 0f, -0.5f),
                new Vector3(0.5f, 0f, -0.5f),
                new Vector3(0.5f, 0f, 0.5f),
                new Vector3(-0.5f, 0f, 0.5f)
            };
            
            triangles = new []
            {
                0, 2, 1,
                0, 3, 2
            };
        }

        /// <summary>
        /// Generates UVs for a quad.
        /// </summary>
        /// <param name="uvs"></param>
        /// <param name="settings"></param>
        public override void UV(out Vector2[] uvs, GeometryBuilderUVSettings settings)
        {
            uvs = new[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };
        }

        /// <summary>
        /// Custom factory for quad data.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(QuadGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new GeometryBuilderSettings();
        }
    }
}