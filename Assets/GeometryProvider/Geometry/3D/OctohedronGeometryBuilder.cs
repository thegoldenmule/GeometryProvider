using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Constructs a platonic solid! The Octohedron.
    /// </summary>
    [DisplayName("Octohedron")]
    [Description("Builds a polyhedron with 8 faces.")]
    public class OctohedronGeometryBuilder : StandardGeometryBuilder
    {
        public static readonly Vector3[] Vertices =
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1)
        };

        public static readonly int[] Triangles =
        {
            4, 0, 2,
            4, 2, 1,
            4, 1, 3,
            4, 3, 0,
            5, 2, 0,
            5, 1, 2,
            5, 3, 1,
            5, 0, 3
        };

        public override void Layout(out Vector3[] vertices, out int[] triangles)
        {
            vertices = Vertices;
            triangles = Triangles;
        }
    }
}