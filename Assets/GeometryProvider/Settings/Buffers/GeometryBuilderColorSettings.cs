using System;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Specifies method of combining colors.
    /// </summary>
    public enum BlendMode
    {
        Additive,
        Subtractive,
        Multiplicative
    }

    /// <summary>
    /// Holds Color data.
    /// </summary>
    [Serializable]
    public class GeometryBuilderColorSettings : GeometryBuilderBufferSettings<Color>
    {
        /// <summary>
        /// Transforms.
        /// </summary>
        private static readonly Func<Color, Color, Color>[] _colorMethods = 
        {
            (a, b) => a + b,
            (a, b) => a - b,
            (a, b) => a * b
        };

        /// <summary>
        /// Tint color.
        /// </summary>
        public Color Tint = Color.white;

        /// <summary>
        /// BlendMode of combining colors.
        /// </summary>
        public BlendMode BlendMode;

        /// <summary>
        /// Transforms colors.
        /// </summary>
        /// <param name="buffer"></param>
        public override void Transform(ref Color[] buffer)
        {
            var func = _colorMethods[(int) BlendMode];
            for (int i = 0, len = buffer.Length; i < len; i++)
            {
                buffer[i] = func(buffer[i], Tint);
            }
        }
    }
}