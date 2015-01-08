using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds tangent data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderTangentSettings : GeometryBuilderBufferSettings<Vector3>
    {
        /// <summary>
        /// If true, inverts normals.
        /// </summary>
        public bool Invert = false;

        /// <summary>
        /// Transforms vectors.
        /// </summary>
        /// <param name="buffer"></param>
        public override void Transform(ref Vector3[] buffer)
        {
            if (Invert)
            {
                for (int i = 0, len = buffer.Length; i < len; i++)
                {
                    buffer[i] = -buffer[i];
                }
            }
        }
    }
}