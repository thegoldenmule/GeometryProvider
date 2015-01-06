using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds a pyramid.
    /// </summary>
    [DisplayName("Pyramid")]
    [Description("Builds a pyramid with a single point at the top.")]
    public class PyramidGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Builds a pyramid.
        /// </summary>
        public override bool Build(Mesh mesh, GeometryBuilderSettings settings, out string error)
        {
            error = string.Empty;

            var pyramidSettings = (PyramidGeometryBuilderSettings) settings;

            var numSides = pyramidSettings.NumSides;

            int numVerts, numTriangles;
            CalculateBufferLength(pyramidSettings.NumSides, out numVerts, out numTriangles);

            var vertices = new Vector3[numVerts + 1];
            var triangles = new int[(numTriangles + pyramidSettings.NumSides) * 3];

            BuildCircle(
                numSides,
                ref vertices,
                ref triangles);

            // add last vert
            vertices[numVerts] = new Vector3(0f, pyramidSettings.Height, 0f);

            // construct triangles to new vert
            var startTriangleIndex = numSides * 3;
            for (int i = 0, len = numSides; i < len; i++)
            {
                var triangleIndex = startTriangleIndex + i * 3;
                var vertIndex = i + 1;

                triangles[triangleIndex] = vertIndex;
                triangles[triangleIndex + 1] = numVerts;

                var index = vertIndex + 1;
                if (index >= numVerts)
                {
                    index = 1;
                }

                triangles[triangleIndex + 2] = index;
            }

            Transform(ref vertices, settings);
            mesh.Apply(ref vertices, ref triangles);

            return true;
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
        /// Builds circle geo.
        /// </summary>
        private static void BuildCircle(
            int numSides,
            ref Vector3[] vertices,
            ref int[] triangles)
        {
            int numVerts, numTriangles;
            CalculateBufferLength(numSides, out numVerts, out numTriangles);

            vertices[0] = Vector3.zero;

            var radians = 2f * Mathf.PI / numSides;
            for (var i = 1; i < numVerts; i++)
            {
                vertices[i] = 0.5f * new Vector3(
                        Mathf.Cos(radians * (i - 1)),
                        0f,
                        Mathf.Sin(radians * (i - 1)));
            }

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

        /// <summary>
        /// Custom factory.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(PyramidGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new PyramidGeometryBuilderSettings();
        }
    }
}