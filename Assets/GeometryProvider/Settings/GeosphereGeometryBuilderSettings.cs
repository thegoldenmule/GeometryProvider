namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Custom settings for Geospheres.
    /// </summary>
    public class GeosphereGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// How many times to subdivide the mesh.
        /// </summary>
        public int Quality = 2;
    }
}