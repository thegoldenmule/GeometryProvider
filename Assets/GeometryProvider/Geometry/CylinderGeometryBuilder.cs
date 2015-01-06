using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Constructos geometry for a cylinder with a variable number of sides.
    /// </summary>
    [DisplayName("Cylinder")]
    [Description("Builds a cylinder with the desired number of sides.")]
    public class CylinderGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Builds mesh.
        /// </summary>
        public override void Build(Mesh mesh, GeometryBuilderSettings settings)
        {
            var cylinderSettings = (CylinderGeometryBuilderSettings) settings;
            
            var numSides = Mathf.Max(3, cylinderSettings.NumSides);

            // calculate buffer lengths
            int numVerts, numTriangles;
            CalculateBufferLength(numSides, out numVerts, out numTriangles);

            // top and bottom of cylinder, so multiply by two
            int numTotalVerts = 2 * numVerts;
            int numTotalTriangles = 2 * numTriangles;

            // the user may not want the ends filled in
            if (!cylinderSettings.Endcaps)
            {
                numTotalTriangles = 0;
            }

            // a quad for each side
            numTotalTriangles += 2 * numSides;

            var vertices = new Vector3[numTotalVerts];
            var triangles = new int[numTotalTriangles * 3];

            var halfHeight = cylinderSettings.Height / 2f;

            // bottom
            {
                vertices[0] = new Vector3(0f, -halfHeight, 0f);

                var radians = 2f * Mathf.PI / numSides;
                for (var i = 1; i < numVerts; i++)
                {
                    vertices[i] = new Vector3(
                        0.5f * Mathf.Cos(radians * (i - 1)),
                        -halfHeight,
                        0.5f * Mathf.Sin(radians * (i - 1)));
                }

                if (cylinderSettings.Endcaps)
                {
                    for (var i = 0; i < numTriangles * 3; i += 3)
                    {
                        var vertIndex = i / 3 + 1;

                        triangles[i] = vertIndex;
                        triangles[i + 2] = 0;

                        vertIndex = vertIndex + 1;
                        if (vertIndex >= numVerts)
                        {
                            vertIndex = 1;
                        }

                        triangles[i + 1] = vertIndex;
                    }
                }
            }

            // top
            {
                var startVertIndex = numVerts;

                vertices[startVertIndex] = new Vector3(0f, halfHeight, 0f);

                var radians = 2f * Mathf.PI / numSides;
                for (var i = 1; i < numVerts; i++)
                {
                    vertices[startVertIndex + i] = new Vector3(
                        0.5f * Mathf.Cos(radians * (i - 1)),
                        halfHeight,
                        0.5f * Mathf.Sin(radians * (i - 1)));
                }

                if (cylinderSettings.Endcaps)
                {
                    var startTriangleIndex = numTriangles * 3;
                    for (var i = 0; i < numTriangles * 3; i += 3)
                    {
                        var vertIndex = startVertIndex + i / 3 + 1;

                        triangles[startTriangleIndex + i] = vertIndex;
                        triangles[startTriangleIndex + i + 1] = startVertIndex;

                        vertIndex = vertIndex + 1;
                        if (vertIndex >= startVertIndex + numVerts)
                        {
                            vertIndex = startVertIndex + 1;
                        }

                        triangles[startTriangleIndex + i + 2] = vertIndex;
                    }
                }
            }

            // quads about the edges
            {
                var bottomVertStart = 1;
                var topVertStart = numVerts + 1;

                var bottomVertEnd = numVerts - 1;
                var topVertEnd = 2 * numVerts - 1;

                var startTriangleIndex = cylinderSettings.Endcaps
                    ? 2 * numTriangles * 3
                    : 0;

                for (int quad = 0, len = numSides; quad < len; quad++)
                {
                    // get 4 corners
                    var topVertIndex = topVertStart + quad;
                    var bottomVertIndex = bottomVertStart + quad;

                    var topVertNextIndex = topVertIndex + 1;
                    if (topVertNextIndex > topVertEnd)
                    {
                        topVertNextIndex = topVertStart;
                    }

                    var bottomVertNextIndex = bottomVertIndex + 1;
                    if (bottomVertNextIndex > bottomVertEnd)
                    {
                        bottomVertNextIndex = bottomVertStart;
                    }

                    var triangleIndex = startTriangleIndex + 6 * quad;

                    triangles[triangleIndex] = topVertIndex;
                    triangles[triangleIndex + 1] = bottomVertNextIndex;
                    triangles[triangleIndex + 2] = bottomVertIndex;

                    triangles[triangleIndex + 3] = topVertIndex;
                    triangles[triangleIndex + 4] = topVertNextIndex;
                    triangles[triangleIndex + 5] = bottomVertNextIndex;
                }
            }

            // remaining transformation + application
            settings.Vertex.ApplyDefault(mesh, ref vertices, ref triangles);

            ApplyAllDefaults(mesh, settings);
        }

        /// <summary>
        /// Calculates the number of verts and triangles needed.
        /// </summary>
        private static void CalculateBufferLength(
            int numSides,
            out int numVerts,
            out int numTriangles)
        {
            numVerts = numSides + 1;
            numTriangles = numSides;
        }

        /// <summary>
        /// Custom factory for cylinder settings.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(CylinderGeometryBuilder))]
        private static GeometryBuilderSettings Factory()
        {
            return new CylinderGeometryBuilderSettings();
        }
    }
}