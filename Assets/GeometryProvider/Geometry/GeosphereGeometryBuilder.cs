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
        /// Builds Geosphere.
        /// </summary>
        public override void Build(Mesh mesh, GeometryBuilderSettings settings)
        {
            var geosphereSettings = (GeosphereGeometryBuilderSettings) settings;

            var vertices = IcosahedronGeometryBuilder.Vertices;
            var triangles = IcosahedronGeometryBuilder.Triangles;

            for (int i = 0, len = geosphereSettings.Quality; i < len; i++)
            {
                MeshExtensions.Subdivide(ref vertices, ref triangles);
            }

            Normalize(ref vertices);
            Transform(ref vertices, settings);
            mesh.Apply(ref vertices, ref triangles);
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