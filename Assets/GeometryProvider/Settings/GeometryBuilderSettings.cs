using System;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    public enum Buffer
    {
        Vertex,
        UV,
        UV2,
        Color,
        Normal,
        Tangent
    }

    [Serializable]
    public class GeometryBuilderVertexSettings
    {
        public Vector3 Translation = Vector3.zero;
        public Quaternion Rotation = Quaternion.identity;
        public Vector3 Scale = Vector3.one;

        public virtual void ApplyDefault(
            Mesh mesh,
            ref Vector3[] vertices,
            ref int[] triangles)
        {
            var transformation = Matrix4x4.TRS(
                Translation,
                Rotation,
                Scale);
            for (int i = 0, len = vertices.Length; i < len; i++)
            {
                vertices[i] = transformation.MultiplyPoint(vertices[i]);
            }

            mesh.Apply(ref vertices, ref triangles);
        }
    }

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
        }
    }

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
        }
    }

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
        }
    }

    [Serializable]
    public class GeometryBuilderSettings
    {
        /// <summary>
        /// Descriptive name. This is used by the editor and will be null at
        /// runtime.
        /// </summary>
        [NonSerialized]
        public string Name;

        /// <summary>
        /// Description of builder. This is used by the editor and will be null
        /// at runtime.
        /// </summary>
        [NonSerialized]
        public string Description;

        /// <summary>
        /// If true, allows verts to be shared between triangles. If false,
        /// each triangle will have its own verts.
        /// </summary>
        public bool ShareVerts = true;

        /// <summary>
        /// Settings for vertices.
        /// </summary>
        public GeometryBuilderVertexSettings Vertex = new GeometryBuilderVertexSettings();

        /// <summary>
        /// Settings for UVs.
        /// </summary>
        public GeometryBuilderUVSettings UV = new GeometryBuilderUVSettings();

        /// <summary>
        /// Settings for UV2s.
        /// </summary>
        public GeometryBuilderUVSettings UV2 = new GeometryBuilderUVSettings
        {
            Buffer = Buffer.UV2
        };

        /// <summary>
        /// Settings for Color.
        /// </summary>
        public GeometryBuilderColorSettings Color = new GeometryBuilderColorSettings();

        /// <summary>
        /// Settings for Normals.
        /// </summary>
        public GeometryBuilderNormalSettings Normals = new GeometryBuilderNormalSettings();

        /// <summary>
        /// Settings for Tangents.
        /// </summary>
        public GeometryBuilderTangentSettings Tangents = new GeometryBuilderTangentSettings();
    }
}
