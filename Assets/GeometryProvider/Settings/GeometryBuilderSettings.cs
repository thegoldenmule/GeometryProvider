using System;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    [Serializable]
    public class GeometryBuilderTransformSettings
    {
        public Vector3 Translation = Vector3.zero;
        public Quaternion Rotation = Quaternion.identity;
        public Vector3 Scale = Vector3.one;
        public Material CustomDeformation;

        public Matrix4x4 TRS()
        {
            return Matrix4x4.TRS(
                Translation,
                Rotation,
                Scale);
        }
    }

    [Serializable]
    public class GeometryBuilderBufferSettings
    {
        public enum MeshBuffer
        {
            UV,
            UV2,
            Color,
            Normal,
            Tangent
        }

        public MeshBuffer Buffer;
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

        public GeometryBuilderTransformSettings Transform = new GeometryBuilderTransformSettings();
        public GeometryBuilderBufferSettings[] Buffers;
    }
}
