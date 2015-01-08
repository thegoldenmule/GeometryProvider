using System;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Settings for generating a convex or star polygon.
    /// </summary>
    [Serializable]
    public class CircleGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// The number of sides.
        /// </summary>
        public int NumSides = 10;
    }
}
