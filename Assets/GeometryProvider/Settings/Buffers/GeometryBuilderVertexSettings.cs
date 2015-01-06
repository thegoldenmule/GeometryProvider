using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Holds vertex settings.
    /// </summary>
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
}