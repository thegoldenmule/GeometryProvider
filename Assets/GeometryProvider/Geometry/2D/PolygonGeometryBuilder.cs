using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds polygons!
    /// </summary>
    public class PolygonGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Builds the mesh.
        /// </summary>
        public override void Build(Mesh mesh, GeometryBuilderSettings settings)
        {
            int numVerts, numTriangles;
        }

        /// <summary>
        /// Custom factory for Polygon settings.
        /// </summary>
        [CustomFactory(typeof(PolygonGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new PolygonGeometryBuilderSettings();
        }
    }
}