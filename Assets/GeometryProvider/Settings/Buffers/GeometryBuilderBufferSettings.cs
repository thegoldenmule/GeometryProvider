using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds buffer data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderBufferSettings
    {
        public bool Enabled = false;
        public Buffer Buffer;

        public virtual void ApplyDefault(Mesh mesh)
        {
            // 
        }
    }
}