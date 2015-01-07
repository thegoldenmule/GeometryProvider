namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Custom settings for building polygons.
    /// </summary>
    public class PolygonGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// Governs how "spiky" a polygon is. The higher the value, the further
        /// from center a polygon's vertices may be.
        /// </summary>
        public float Spikiness = 0f;

        /// <summary>
        /// Governs how "irregular" a polygon is. The higher the value, the
        /// more difference in the radial placement of vertices about the
        /// center.
        /// </summary>
        public float Irregularity = 0f;
    }
}