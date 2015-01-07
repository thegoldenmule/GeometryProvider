using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds Color data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderColorSettings : GeometryBuilderBufferSettings
    {
        public Color Tint = Color.white;

        public GeometryBuilderColorSettings()
        {
            Buffer = Buffer.Color;
        }

        public override void ApplyDefault(Mesh mesh)
        {
            if (Enabled)
            {
                var numVerts = mesh.vertexCount;

                var colors = new Color[numVerts];
                for (int i = 0; i < numVerts; i++)
                {
                    colors[i] = Tint;
                }

                var colorObj = (object) colors;
                mesh.SetBuffer(Buffer, ref colorObj);
            }
            else
            {
                object nullObj = null;
                mesh.SetBuffer(Buffer, ref nullObj);
            }
        }
    }
}