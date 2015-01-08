using System;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds buffer data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderBufferSettings
    {
        /// <summary>
        /// True if enabled.
        /// </summary>
        public bool Enabled = false;
    }

    /// <summary>
    /// Holds buffer data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderBufferSettings<T> : GeometryBuilderBufferSettings
    {
        /// <summary>
        /// Transforms buffer.
        /// </summary>
        /// <param name="buffer"></param>
        public virtual void Transform(ref T[] buffer)
        {
            
        }
    }
}