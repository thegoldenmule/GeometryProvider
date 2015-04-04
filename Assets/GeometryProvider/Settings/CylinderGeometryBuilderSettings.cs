using System;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// UV settings for cylinder.
    /// </summary>
    [Serializable]
    public class CylinderGeometryUVSettings : GeometryBuilderUVSettings
    {
        /// <summary>
        /// Describes potential mapping methods.
        /// </summary>
        public enum MappingMethod
        {
            Cylindrical,
            Planar,
            Mixed
        }

        /// <summary>
        /// Method of mapping uvs.
        /// </summary>
        public MappingMethod Method;
    }

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