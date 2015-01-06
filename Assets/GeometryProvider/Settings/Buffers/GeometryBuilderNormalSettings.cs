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
        public bool Generate = true;

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