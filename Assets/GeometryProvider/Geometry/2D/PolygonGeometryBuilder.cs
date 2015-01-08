using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds polygons!
    /// </summary>
    public class PolygonGeometryBuilder : StandardGeometryBuilder
    {
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