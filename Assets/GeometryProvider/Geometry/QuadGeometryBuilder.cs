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
        /// <param name="mesh"></param>
        /// <param name="settings"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool Build(
            Mesh mesh,
            GeometryBuilderSettings settings,
            out string error)
        {
            error = string.Empty;

            var vertices = new []
            {
                new Vector3(-0.5f, 0f, -0.5f),
                new Vector3(0.5f, 0f, -0.5f),
                new Vector3(0.5f, 0f, 0.5f),
                new Vector3(-0.5f, 0f, 0.5f)
            };
            var indices = new []
            {
                0, 2, 1,
                0, 3, 2
            };

            Transform(ref vertices, settings);

            mesh.Apply(ref vertices, ref indices);

            return true;
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