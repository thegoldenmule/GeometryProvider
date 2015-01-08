using System;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Custom settings for Geospheres.
    /// </summary>
    [Serializable]
    public class GeosphereGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// How many times to subdivide the mesh.
        /// </summary>
        public int Subdivisions = 2;
    }
}