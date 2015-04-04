using System.Text;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Extensions for UnityEngine.Color.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Component-wise min.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color Min(Color a, Color b)
        {
            return new Color(
                Mathf.Min(a.r, b.r),
                Mathf.Min(a.g, b.g),
                Mathf.Min(a.b, b.b),
                Mathf.Min(a.a, b.a));
        }

    }
}