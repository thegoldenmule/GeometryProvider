﻿using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// For building a 20 sided polyhedron.
    /// </summary>
    [DisplayName("Icosahedron")]
    [Description("Builds a regular polyhedron with 20 faces.")]
    public class IcosahedronGeometryBuilder : StandardGeometryBuilder
    {
        public static readonly int[] Indices =
		{
			1,4,0,
			4,9,0,
			4,5,9,
			8,5,4,
			1,8,4,
			1,10,8,
			10,3,8,
			8,3,5,
			3,2,5,
			3,7,2,
			3,10,7,
			10,6,7,
			6,11,7,
			6,0,11,
			6,1,0,
			10,1,6,
			11,0,9,
			2,11,9,
			5,2,9,
			11,2,7 
		};

        public static readonly Vector3[] Vertices =
        {
            new Vector3(-0.525731112119133606f, 0f, 0.850650808352039932f),
            new Vector3(0.525731112119133606f, 0f, 0.850650808352039932f),
            new Vector3(-0.525731112119133606f, 0f, -0.850650808352039932f),

            new Vector3(0.525731112119133606f, 0f, -0.850650808352039932f),
            new Vector3(0f, 0.850650808352039932f, 0.525731112119133606f),
            new Vector3(0f, 0.850650808352039932f, -0.525731112119133606f),

            new Vector3(0f, -0.850650808352039932f, 0.525731112119133606f),
            new Vector3(0f, -0.850650808352039932f, -0.525731112119133606f),
            new Vector3(0.850650808352039932f, 0.525731112119133606f, 0f),

            new Vector3(-0.850650808352039932f, 0.525731112119133606f, 0f),
            new Vector3(0.850650808352039932f, -0.525731112119133606f, 0f),
            new Vector3(-0.850650808352039932f, -0.525731112119133606f, 0f)
		};

        /// <summary>
        /// Builds an icosahedron.
        /// </summary>
        public override void Build(Mesh mesh, GeometryBuilderSettings settings)
        {
            var verts = Vertices;
            var triangles = Indices;

            Transform(ref verts, settings);

            mesh.Apply(ref verts, ref triangles);
        }
    }
}
