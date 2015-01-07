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
        /// Builds geo for a quad.
        /// </summary>
        public override void Build(
            Mesh mesh,
            GeometryBuilderSettings settings)
        {
            var vertices = new []
            {
                new Vector3(-0.5f, 0f, -0.5f),
                new Vector3(0.5f, 0f, -0.5f),
                new Vector3(0.5f, 0f, 0.5f),
                new Vector3(-0.5f, 0f, 0.5f)
            };
            
            var triangles = new []
            {
                0, 2, 1,
                0, 3, 2
            };

            settings.Vertex.TransformAndApply(mesh, ref vertices, ref triangles);

            ApplyAllDefaults(mesh, settings);
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