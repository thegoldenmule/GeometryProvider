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
        /// <summary>
        /// Translates vertices before baking.
        /// </summary>
        public Vector3 Translation = Vector3.zero;

        /// <summary>
        /// Rotates vertices before baking.
        /// </summary>
        public Quaternion Rotation = Quaternion.identity;

        /// <summary>
        /// Scales vertices before baking.
        /// </summary>
        public Vector3 Scale = Vector3.one;

        /// <summary>
        /// If true, reverses triangle winding. Ignored if DoubleSided is set
        /// to true.
        /// </summary>
        public bool ReverseWinding = false;

        /// <summary>
        /// If true, duplicates geometry so that each triangle is double sided.
        /// </summary>
        public bool DoubleSided = false;

        /// <summary>
        /// Transforms and applies verts.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public virtual void Transform(
            ref Vector3[] vertices,
            ref int[] triangles)
        {
            // apply vertex transforms
            TransformVertices(ref vertices);

            // apply triangle transforms
            TransformTriangles(ref triangles);
        }

        /// <summary>
        /// Performs vertex transformations.
        /// </summary>
        /// <param name="vertices"></param>
        protected virtual void TransformVertices(ref Vector3[] vertices)
        {
            if (null != vertices)
            {
                var transformation = Matrix4x4.TRS(
                    Translation,
                    Rotation,
                    Scale);
                for (int i = 0, len = vertices.Length; i < len; i++)
                {
                    vertices[i] = transformation.MultiplyPoint(vertices[i]);
                }
            }
        }

        /// <summary>
        /// Performs triangle transformations.
        /// </summary>
        /// <param name="triangles"></param>
        protected virtual void TransformTriangles(ref int[] triangles)
        {
            if (null != triangles)
            {
                if (DoubleSided)
                {
                    int[] newTriangleBuffer;
                    TransformDoubleSided(ref triangles, out newTriangleBuffer);
                    triangles = newTriangleBuffer;
                }
                else if (ReverseWinding)
                {
                    TransformReverseWinding(ref triangles);
                }
            }
        }

        /// <summary>
        /// Duplicates every triangle, reversing the winding on the second set.
        /// This makes every triangle double sided.
        /// </summary>
        /// <param name="triangles"></param>
        /// <param name="doubleSidedTriangles"></param>
        protected virtual void TransformDoubleSided(
            ref int[] triangles,
            out int[] doubleSidedTriangles)
        {
            var len = triangles.Length;
            doubleSidedTriangles = new int[2 * len];

            // copy first triangles
            Array.Copy(triangles, doubleSidedTriangles, len);

            // reverse winding on second set of triangles
            for (var i = 0; i < len; i += 3)
            {
                doubleSidedTriangles[i + len] = triangles[i];
                doubleSidedTriangles[i + len + 1] = triangles[i + 2];
                doubleSidedTriangles[i + len + 2] = triangles[i + 1];
            }
        }

        /// <summary>
        /// Reverses triangle winding.
        /// </summary>
        /// <param name="triangles"></param>
        protected virtual void TransformReverseWinding(ref int[] triangles)
        {
            var len = triangles.Length;
            
            for (var i = 0; i < len; i += 3)
            {
                var temp = triangles[i + 2];

                triangles[i + 2] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
        }
    }
}