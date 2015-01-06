namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Custom settings object for cylinders.
    /// </summary>
    public class CylinderGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// Number of sides.
        /// </summary>
        public int NumSides = 3;

        /// <summary>
        /// True if the ends should be capped.
        /// </summary>
        public bool Endcaps = true;
    }
}