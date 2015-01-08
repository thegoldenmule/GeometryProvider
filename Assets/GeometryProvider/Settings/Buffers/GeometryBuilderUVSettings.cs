using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Defines a rotation in UV space. You can do cool stuff with this!
    /// </summary>
    [Serializable]
    public class UVRotation
    {
        /// <summary>
        /// The point in UV space to rotate about.
        /// </summary>
        public Vector2 Origin = new Vector2(0.5f, 0.5f);

        /// <summary>
        /// The radian measure of the rotation about UV.
        /// </summary>
        public float Theta = 0f;

        /// <summary>
        /// Returns true when the rotation will not affect the UVs.
        /// </summary>
        public bool IsDefault()
        {
            return Math.Abs(Theta % (2 * Mathf.PI)) < Mathf.Epsilon;
        }
    }

    /// <summary>
    /// A settings object that holds UV transformations.
    /// </summary>
    [Serializable]
    public class GeometryBuilderUVSettings : GeometryBuilderBufferSettings
    {
        /// <summary>
        /// UV space is [0, 1] x [0, 1]. With this Rect, you can specify a new
        /// space to map to. Essentially, this gives control over UV
        /// translation and scale.
        /// 
        /// More precisely, there is a transformation function f such that:
        /// 
        /// f(0, 0) -> (Rect.minx, Rect.miny)
        /// f(1, 1) -> (Rect.maxx, Rect.maxy)
        /// 
        /// </summary>
        public Rect Rect = new Rect(0, 0, 1, 1);

        /// <summary>
        /// Specifies a rotation in UV space. This is handy for effects, but
        /// also for flipping u and v.
        /// 
        /// More precisely, every point in UV space is rotated about Origin by
        /// Theta.
        /// </summary>
        public UVRotation Rotation = new UVRotation();

        /// <summary>
        /// Creates a new settings object for UVs.
        /// </summary>
        public GeometryBuilderUVSettings()
        {
            Buffer = Buffer.UV;
        }

        /// <summary>
        /// Transforms UVs.
        /// </summary>
        public virtual void TransformAndApply(Mesh mesh, ref Vector2[] uvs)
        {
            if (Enabled)
            {
                TransformWithRect(ref uvs);
                TransformWithRotation(ref uvs);

                var uvObject = (object) uvs;
                mesh.SetBuffer(Buffer, ref uvObject);
            }
        }

        /// <summary>
        /// Transforms UVs by scaling them by a Rect.
        /// </summary>
        /// <param name="uvs"></param>
        protected virtual void TransformWithRect(ref Vector2[] uvs)
        {
            const float epsilon = Mathf.Epsilon;

            var minx = Rect.x;
            var miny = Rect.y;
            var diffx = Rect.width;
            var diffy = Rect.height;

            // default values?
            if (Math.Abs(minx) < epsilon
                && Math.Abs(miny) < epsilon
                && Math.Abs(diffx - 1) < epsilon
                && Math.Abs(diffy - 1) < epsilon)
            {
                return;
            }

            // perform transformation
            for (int i = 0, len = uvs.Length; i < len; i++)
            {
                var uv = uvs[i];

                uvs[i] = new Vector2(
                    minx + uv.x * diffx,
                    miny + uv.y * diffy);
            }
        }

        /// <summary>
        /// Transforms UVs by rotating them about an angle.
        /// </summary>
        protected virtual void TransformWithRotation(ref Vector2[] uvs)
        {
            if (Rotation.IsDefault())
            {
                return;
            }

            var sine = Mathf.Sin(Rotation.Theta);
            var cosine = Mathf.Cos(Rotation.Theta);

            var origin = Rotation.Origin;

            // perform transformation
            for (int i = 0, len = uvs.Length; i < len; i++)
            {
                var uv = uvs[i];

                // translate
                uv -= origin;

                // rotate
                uv = new Vector2(
                    uv.x * cosine - uv.y * sine,
                    uv.x * sine + uv.y * cosine);

                // translate
                uv += origin;

                uvs[i] = uv;
            }
        }
    }
}