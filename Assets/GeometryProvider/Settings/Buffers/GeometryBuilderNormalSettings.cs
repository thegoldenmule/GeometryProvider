using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds normal data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderNormalSettings : GeometryBuilderBufferSettings
    {
        /// <summary>
        /// If true, generates normals.
        /// </summary>
        public bool Generate = true;

        /// <summary>
        /// If true, flips normals. This is useful if you are *inside* a
        /// primitive.
        /// </summary>
        public bool Invert = true;

        public GeometryBuilderNormalSettings()
        {
            Buffer = Buffer.Normal;
        }

        public override void ApplyDefault(Mesh mesh)
        {
            base.ApplyDefault(mesh);
            
            if (Enabled)
            {
                // TODO: this won't work if we chose a different buffer!
                mesh.RecalculateNormals();
            }
            else
            {
                mesh.normals = null;
            }
        }
    }
}