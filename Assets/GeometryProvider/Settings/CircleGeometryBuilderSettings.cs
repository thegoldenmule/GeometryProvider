namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Settings for generating a convex or star polygon.
    /// </summary>
    public class CircleGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// The number of sides.
        /// </summary>
        public int NumSides = 10;
    }
}
