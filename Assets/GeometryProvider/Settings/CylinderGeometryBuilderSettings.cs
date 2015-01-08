using System;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Custom settings object for cylinders.
    /// </summary>
    [Serializable]
    public class CylinderGeometryBuilderSettings : CircleGeometryBuilderSettings
    {
        /// <summary>
        /// True if the ends should be capped.
        /// </summary>
        public bool Endcaps = true;

        /// <summary>
        /// Height of the cylinder.
        /// </summary>
        public float Height = 1;
    }
}