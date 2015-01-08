using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Constructs a platonic solid! The Dodecahedron.
    /// </summary>
    [DisplayName("Dodecahedron")]
    [Description("Builds a regular polyhedron with 12 faces.")]
    public class DodecahedronGeometryBuilder : StandardGeometryBuilder
    {
        private static readonly float A = 1f / Mathf.Sqrt(3);
        private static readonly float B = Mathf.Sqrt((3 - Mathf.Sqrt(5)) / 6);
        private static readonly float C = Mathf.Sqrt((3 + Mathf.Sqrt(5)) / 6);

        public static readonly Vector3[] Vertices =
        {
            new Vector3(A, A, A),
            new Vector3(A, A, -A),
            new Vector3(A, -A, A),
            new Vector3(A, -A, -A),
            
            new Vector3(-A, A, A),
            new Vector3(-A, A, -A),
            new Vector3(-A, -A, A),
            new Vector3(-A, -A, -A),

            new Vector3(B, C, 0),
            new Vector3(-B, C, 0),
            new Vector3(B, -C, 0),
            new Vector3(-B, -C, 0),

            new Vector3(C, 0, B),
            new Vector3(C, 0, -B),
            new Vector3(-C, 0, B),
            new Vector3(-C, 0, -B),

            new Vector3(0, B, C),
            new Vector3(0, -B, C),
            new Vector3(0, B, -C),
            new Vector3(0, -B, -C)
        };

        public static readonly int[] Triangles =
        {
            0, 8, 9, 0, 9, 4, 0, 4, 16,
            0, 16, 17, 0, 17, 2, 0, 2, 12,
            12, 2, 10, 12, 10, 3, 12, 3, 13,
            9, 5, 15, 9, 15, 14, 9, 14, 4,
            3, 19, 18, 3, 18, 1, 3, 1, 13,
            7, 11, 6, 7, 6, 14, 7, 14, 15,

            0, 12, 13, 0, 13, 1, 0, 1, 8,
            8, 1, 18, 8, 18, 5, 8, 5, 9,
            16, 4, 14, 16, 14, 6, 16, 6, 17,
            6, 11, 10, 6, 10, 2, 6, 2, 17,
            7, 15, 5, 7, 5, 18, 7, 18, 19,
            7, 19, 3, 7, 3, 10, 7, 10, 11
        };

        public override void Layout(out Vector3[] vertices, out int[] triangles)
        {
            vertices = Vertices;
            triangles = Triangles;
        }
    }
}