using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds tangent data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderTangentSettings : GeometryBuilderBufferSettings
    {
        public bool Generate = true;

        public GeometryBuilderTangentSettings()
        {
            Buffer = Buffer.Tangent;
        }

        public override void ApplyDefault(Mesh mesh)
        {
            base.ApplyDefault(mesh);

            if (Enabled)
            {
                // TODO: this won't work if we chose a different buffer!
                mesh.RecalculateTangents();
            }
            else
            {
                mesh.tangents = null;
            }
        }
    }
}