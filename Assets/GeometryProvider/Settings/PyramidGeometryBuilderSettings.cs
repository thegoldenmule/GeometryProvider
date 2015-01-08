using System;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds a pyramid.
    /// </summary>
    [Serializable]
    public class PyramidGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// Number of sides to the pyramid.
        /// </summary>
        public int NumSides = 4;

        /// <summary>
        /// Height of the pyramid.
        /// </summary>
        public float Height = 1;
    }
}