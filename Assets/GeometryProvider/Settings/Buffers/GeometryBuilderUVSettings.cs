using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds UV data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderUVSettings : GeometryBuilderBufferSettings
    {
        public Rect Rect = new Rect(0, 0, 1, 1);

        public GeometryBuilderUVSettings()
        {
            Buffer = Buffer.UV;
        }

        public virtual void Transform(ref Vector2[] uvs)
        {
            if (Enabled)
            {
                var minx = Rect.x;
                var miny = Rect.y;
                var diffx = Rect.width;
                var diffy = Rect.height;
                for (int i = 0, len = uvs.Length; i < len; i++)
                {
                    var uv = uvs[i];

                    uvs[i] = new Vector2(
                        minx + uv.x * diffx,
                        miny + uv.y * diffy);
                }
            }
        }
    }
}