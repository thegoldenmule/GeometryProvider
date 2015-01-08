using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Creates geospheres.
    /// </summary>
    [DisplayName("Geo-Sphere")]
    [Description("Creates a smoother type of sphere with no wasted geometry.")]
    public class GeosphereGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Settings for a Geosphere.
        /// </summary>
        private GeosphereGeometryBuilderSettings _settings;

        /// <summary>
        /// Starts the build flow.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="settings"></param>
        public override void Start(Mesh mesh, GeometryBuilderSettings settings)
        {
            _settings = (GeosphereGeometryBuilderSettings) settings;
        }

        /// <summary>
        /// Retrieves the geometry layout.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public override void Layout(out Vector3[] vertices, out int[] triangles)
        {
            vertices = IcosahedronGeometryBuilder.Vertices;
            triangles = IcosahedronGeometryBuilder.Triangles;

            for (int i = 0, len = _settings.Subdivisions; i < len; i++)
            {
                MeshExtensions.Subdivide(ref vertices, ref triangles);
            }

            Normalize(ref vertices);
        }

        /// <summary>
        /// Normalizes all verts.
        /// </summary>
        /// <param name="vertices"></param>
        private static void Normalize(ref Vector3[] vertices)
        {
            for (int i = 0, len = vertices.Length; i < len; i++)
            {
                vertices[i] = vertices[i].normalized;
            }
        }

        /// <summary>
        /// Custom factory for Geosphere settings.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(GeosphereGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new GeosphereGeometryBuilderSettings();
        }
    }
}